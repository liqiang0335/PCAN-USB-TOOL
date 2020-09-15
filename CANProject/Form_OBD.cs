using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    public partial class Form_OBD : Form
    {
        USBCAN usbCAN;
        UDSBASEDCAN udsClass;
        List<UDSServiceFormat> listOBD = new List<UDSServiceFormat>();

        int DELAY = 500;

        public Form_OBD()
        {
            InitializeComponent();

            readUdsInstructions(Application.StartupPath + @"\OBD.txt", listOBD);
            addOBDinfo();

            usbCAN = new USBCAN(obdProcess);
            udsClass = new UDSBASEDCAN(usbCAN);

            int cnt = listOBD.Count / 0x20;
            if ((cnt % 0x20) != 0)
                cnt++;
            arrStrSupported = new string[cnt];

            resetArrStrSupported();

            obdShowIndexs.Clear();

            delFetch = fetchObdValue;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void obdProcess(ref VCI_CAN_OBJ canObj)
        {
            if (canObj.ID >= 0x7E8)
                udsClass.udsProcess(ref canObj);
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void readUdsInstructions(string path, List<UDSServiceFormat> list)
        {
            list.Clear();

            StreamReader sr = new StreamReader(path);

            string line = null;
            while ( (line = sr.ReadLine()) != null)
            {
                UDSServiceFormat uds = new UDSServiceFormat();

                string[] arr = line.Split('\t');
                int len = arr.Length;
                int despSt = -1, despEd = -1;
                if ( (despSt = arr[len-1].IndexOf('"')) != -1)
                {
                    len--;
                    if ((despEd = arr[len].IndexOf('"', despSt + 1)) != -1)
                        uds.description = arr[len].Substring(despSt + 1, despEd - despSt - 1);
                }

                try { uds.address = Convert.ToUInt32(arr[0], 16); }
                catch(Exception) { continue; }

                try { uds.serviceID = Convert.ToByte(arr[1], 16); }
                catch (Exception) { continue; }


                int l;
                for (l = 2; l < len; l++)
                {
                    byte parm = 0;
                    try { parm = Convert.ToByte(arr[l], 16); }
                    catch (Exception) { break; }
                    uds.parameterList.Add(parm);
                }

                if (l >= len)
                    list.Add(uds);
            }

            sr.Close();
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void addOBDinfo()
        {
            for (int i = 0; i < listOBD.Count; i++)
            {
                dataGridViewshow.Rows.Add();
                dataGridViewshow.Rows[i].Cells[1].Value = listOBD[i].description;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        string[] arrStrSupported;

        void resetArrStrSupported()
        {
            for (int i = 0; i < arrStrSupported.Length; i++)
                arrStrSupported[i] = "undefined";
        }
        /*---------------------------------------------------------------------------------------------------*/
        public bool fetchStrSupported(byte externFlag)
        {
            if (usbCAN.startDevice() == false)
                return false;

            for (int i = 0; i < arrStrSupported.Length; i++)
            {
                UDSServiceFormat uds = listOBD[0x20 * i];
                uds.format = externFlag;
                if (uds.format == 0)
                    uds.address = 0x7DF;
                else
                    uds.address = 0x18DB33F1;

                uds.expectedFrame = 0; //单帧

                UDSResponseList retClass = null;

                for (int reTry = 0; reTry < 10; reTry++)
                {
                    DateTime prev = DateTime.Now;
                    while ((DateTime.Now - prev).TotalMilliseconds < DELAY)
                        Application.DoEvents();

                    retClass = udsClass.getResponseList(uds);

                    if ((retClass.errorCode != UDSResponseList.ERRORCODE.SUCCEED) || 
                        (retClass.responseList.Count < 6) || (retClass.responseList[1] != uds.parameterList[0]) )
                        continue;

                    break;
                }

                if ((retClass.errorCode != UDSResponseList.ERRORCODE.SUCCEED) ||
                        (retClass.responseList.Count < 6) || (retClass.responseList[1] != uds.parameterList[0]))
                {
                    MessageBox.Show("初始化超时，请重试\n或者尝试切换帧格式");
                    return false;
                }

                //address赋值
                for (int g = 0x20 * i + 1; g < 0x20 * i + 0x20; g++)
                {
                    if (uds.format == 0)
                        listOBD[g].address = retClass.respAddress - 0x08;
                    else
                    {
                        UInt32 TASA = retClass.respAddress & 0x0000FFFF;
                        UInt32 addr = 0x18DA0000;

                        TASA = (TASA >> 8) | ((TASA << 8) & 0x0000FF00);
                        addr = addr | TASA;
                        listOBD[g].address = addr;
                    }

                    listOBD[g].expectedFrame = 0;
                }

                arrStrSupported[i] = "";
                for (int d = 0; d < 4; d++)
                    arrStrSupported[i] += Convert.ToString(retClass.responseList[d+2], 2).PadLeft(8, '0');

                if (arrStrSupported[i][0x1f] == '0')
                {
                    for (int j = i + 1; j < arrStrSupported.Length; j++)
                        arrStrSupported[j] = "00000000000000000000000000000000";
                    break;
                }
            }

            if (usbCAN.shutDevice() == false)
                return false;

            return true;
        }
        /*---------------------------------------------------------------------------------------------------*/
        List<int> obdShowIndexs = new List<int>();
        Thread threadFetch = null;

        private void buttonGet_Click(object sender, EventArgs e)
        {
            byte externFlag = 0;
            if (radioButtonNotEx.Checked)
                externFlag = 0;
            else if (radioButtonEx.Checked)
                externFlag = 1;
            else
            {
                MessageBox.Show("请选择帧格式");
                return;
            }
            //初始化
            if (arrStrSupported[0].Equals("undefined") )
            {
                if (fetchStrSupported(externFlag) == false)
                {
                    resetArrStrSupported();
                    return;
                }
            }

            //
            if (usbCAN.startDevice() == false)
                return;

            if (checkBoxRepeat.Checked == false)
            {
                fetchObdValue();
                usbCAN.shutDevice();
            }
            else
            {
                if (threadFetch == null)
                {
                    threadFetch = new Thread(fetchThreading);
                    threadFetch.Start();
                }
            }
        }

        delegate void fetchFunc();
        fetchFunc delFetch;
        void fetchThreading()
        {
            while (true)
            {
                fetchObdValue();
            }
        }

        void fetchObdValue()
        {
            for (int i = 0; i < obdShowIndexs.Count; i++)
            {
                int index;

                try { index = obdShowIndexs[i]; }
                catch (Exception) { continue; } 

                if (index % 0x20 == 0)
                {
                    dataGridViewshow.Rows[index].Cells[2].Value = arrStrSupported[index / 0x20];
                    continue;
                }

                int parentIndex = index / 0x20;
                int childIndex = index - (parentIndex * 0x20) - 1;
                if (arrStrSupported[parentIndex][childIndex] == '0')
                {
                    dataGridViewshow.Rows[index].Cells[2].Value = "此车不支持";
                    continue;
                }

                UDSResponseList retClass = null;
                UDSServiceFormat uds = listOBD[index];

                for (int reTry = 0; reTry < 10; reTry++)
                {
                    DateTime prev = DateTime.Now;
                    while ((DateTime.Now - prev).TotalMilliseconds < DELAY)
                        Application.DoEvents();

                    retClass = udsClass.getResponseList(uds);

                    if ((retClass.errorCode != UDSResponseList.ERRORCODE.SUCCEED) || (retClass.responseList[1] != uds.parameterList[0]))
                        continue;

                    break;
                }

                if (retClass.errorCode != UDSResponseList.ERRORCODE.SUCCEED)
                {
                    dataGridViewshow.Invoke(new Action(() => { dataGridViewshow.Rows[index].Cells[2].Value = "超时"; }));
                    continue;
                }
                else if ((retClass.responseList[1] != uds.parameterList[0]))
                {
                    dataGridViewshow.Invoke(new Action(() => { dataGridViewshow.Rows[index].Cells[2].Value = "帧出错，等待重发"; }));
                    continue;
                }

                string obdVal = obdFormula(retClass);

                dataGridViewshow.Invoke(new Action(() => { dataGridViewshow.Rows[index].Cells[2].Value = obdVal; }));
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        string showHex(List<byte> list)
        {
            string sHex = "";
            for (int i = 2; i < list.Count; i++)
                sHex += Convert.ToString(list[i], 16) + " ";

            return sHex;
        }

        string showA(List<byte> list, double coef, double constant, string unit)
        {
            if (list.Count < 3)
                return "帧出错，等待重发";

            double A = list[2];
            A = coef * A + constant;

            return A.ToString("f2") + " " + unit;
        }

        string showB(List<byte> list, double coef, double constant, string unit)
        {
            if (list.Count < 4)
                return "帧出错，等待重发";

            double A = list[3];
            A = coef * A + constant;

            return A.ToString("f2") + " " + unit;
        }

        string showAB(List<byte> list, double coefA, double coefB, double constant, string unit)
        {
            if (list.Count < 4)
                return "帧出错，等待重发";

            double A = list[2];
            double B = list[3];

            A = coefA * A + coefB * B + constant;

            return A.ToString("f2") + " " + unit;
        }

        string showCD(List<byte> list, double coefC, double coefD, double constant, string unit)
        {
            if (list.Count < 6)
                return "帧出错，等待重发";

            double C = list[4];
            double D = list[5];

            C = coefC * C + coefD * D + constant;

            return C.ToString("f2") + " " + unit;
        }

        string obdFormula(UDSResponseList uds)
        {
            List<byte> list = uds.responseList;
            int cnt = list.Count;

            if (cnt < 2)
                return "帧出错，等待重发";

            string sRet = "";
            byte pID = list[1];
            switch (pID)
            {
                case 0x04:
                    sRet = showA(list, 1/2.55, 0, "%");
                    break;

                case 0x05:
                    sRet = showA(list, 1, -40, "°C");
                    break;

                case 0x06:
                case 0x07:
                case 0x08:
                case 0x09:
                    sRet = showA(list, 1/1.28, -100, "°C");
                    break;

                case 0x0a:
                    sRet = showA(list, 3, 0, "kPa");
                    break;

                case 0x0b:
                    sRet = showA(list, 1, 0, "kPa");
                    break;

                case 0x0c:
                    sRet = showAB(list, 256/4.0, 1/4.0, 0, "rpm");
                    break;

                case 0x0d:
                    sRet = showA(list, 1, 0, "km/h");
                    break;

                case 0x0e:
                    sRet = showA(list, 0.5, -64, "° before TDC");
                    break;

                case 0x0f:
                    sRet = showA(list, 1, -40, "°C");
                    break;

                case 0x10:
                    sRet = showAB(list, 256/100.0, 1/100.0, 0, "grams/sec");
                    break;

                case 0x11:
                    sRet = showA(list, 100/255.0, 0, "%");
                    break;

                case 0x14:
                case 0x15:
                case 0x16:
                case 0x17:
                case 0x18:
                case 0x19:
                case 0x1a:
                case 0x1b:
                    sRet = showA(list, 1/200.0, 0, "volts") + ";";
                    sRet += showB(list, 100/128.0, -100, "%");
                    break;

                case 0x1f:
                    sRet = showAB(list, 256, 1, 0, "seconds");
                    break;

                case 0x21:
                    sRet = showAB(list, 256, 1, 0, "km");
                    break;

                case 0x22:
                    sRet = showAB(list, 0.079*256, 0.079, 0, "kPa");
                    break;

                case 0x23:
                    sRet = showAB(list, 10*256, 10, 0, "kPa");
                    break;

                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 0x28:
                case 0x29:
                case 0x2a:
                case 0x2b:
                    sRet = showAB(list, 2.0*256/65536, 2.0/65536, 0, "ratio") + ";";
                    sRet += showCD(list, 8.0*256/65536, 8.0/65536, 0, "V");
                    break;

                case 0x2c:
                case 0x2e:
                case 0x2f:
                    sRet = showA(list, 100/255.0, 0, "%");
                    break;

                case 0x2d:
                    sRet = showA(list, 100/128.0, -100, "%");
                    break;

                case 0x30:
                    sRet = showA(list, 1, 0, "count");
                    break;

                case 0x31:
                    sRet = showAB(list, 256, 1, 0, "km");
                    break;

                case 0x32:
                    sRet = showAB(list, 256/4.0, 1/4.0, 0, "Pa");
                    break;

                case 0x33:
                    sRet = showA(list, 1, 0, "kPa");
                    break;

                case 0x34:
                case 0x35:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                case 0x3a:
                case 0x3b:
                    sRet = showAB(list, 2.0*256/65536, 2.0/65536, 0, "ratio") + ";";
                    sRet += showCD(list, 1, 1.0/256, -128, "mA");
                    break;

                case 0x3c:
                case 0x3d:
                case 0x3e:
                case 0x3f:
                    sRet = showAB(list, 25.6, 0.1, -40, "°C");
                    break;

                case 0x42:
                    sRet = showAB(list, 0.256, 0.001, 0, "V");
                    break;

                case 0x43:
                    sRet = showAB(list, 25600/255.0, 100, 0, "%");
                    break;

                case 0x44:
                    sRet = showAB(list, 2.0*256/65536, 2.0/65536, 0, "ratio");
                    break;

                case 0x45:
                    sRet = showA(list, 100/255.0, 0, "%");
                    break;

                case 0x46:
                case 0x5c:
                    sRet = showA(list, 1, -40, "°C");
                    break;

                case 0x47:
                case 0x48:
                case 0x49:
                case 0x4a:
                case 0x4b:
                case 0x4c:

                case 0x52:
                case 0x5a:
                case 0x5b:
                    sRet = showA(list, 100/255.0, 0, "%");
                    break;

                case 0x4d:
                case 0x4e:
                    sRet = showAB(list, 256, 1, 0, "minutes");
                    break;

                case 0x53:
                    sRet = showAB(list, 256/200.0, 1/200.0, 0, "kPa");
                    break;

                case 0x54:
                    sRet = showAB(list, 256, 1, -32767, "Pa");
                    break;

                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                    sRet = showA(list, 100.0/128, -100, "%") + ";";
                    sRet += showB(list, 100.0 / 128, -100, "%");
                    break;

                case 0x59:
                    sRet = showAB(list, 2560, 10, 0, "kPa");
                    break;

                case 0x5d:
                    sRet = showAB(list, 256/128.0, 1/128.0, -210, "°");
                    break;

                case 0x5e:
                    sRet = showAB(list, 256/20.0, 1/20.0, 0, "L/h");
                    break;

                case 0x61:
                case 0x62:
                    sRet = showA(list, 1, -125, "%");
                    break;

                default:
                    sRet = showHex(list);
                    break;
            }

            return sRet;
        }
        /*---------------------------------------------------------------------------------------------------*/
        private void 设备参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SetCANParam form = new Form_SetCANParam(usbCAN);
            form.ShowDialog();
        }

        private void buttonSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewshow.Rows.Count; i++)
                dataGridViewshow.Rows[i].Cells[0].Value = true;
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewshow.Rows.Count; i++)
            {
                if (dataGridViewshow.Rows[i].Cells[0].Value != null)
                    dataGridViewshow.Rows[i].Cells[0].Value = !(bool)dataGridViewshow.Rows[i].Cells[0].Value;
                else
                    dataGridViewshow.Rows[i].Cells[0].Value = true;
            }
        }

        private void dataGridViewshow_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewshow.IsCurrentCellDirty)
                dataGridViewshow.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewshow_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if ((bool)dataGridViewshow.Rows[e.RowIndex].Cells[0].Value)
                {
                    if (obdShowIndexs.FindIndex(x => x.Equals(e.RowIndex)) == -1)
                        obdShowIndexs.Add(e.RowIndex);
                }
                else
                {
                    int index = obdShowIndexs.FindIndex(x => x.Equals(e.RowIndex));
                    if (index != -1)
                        obdShowIndexs.RemoveAt(index);
                }
            }
        }

        private void checkBoxRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRepeat.Checked == false)
            {
                if (threadFetch != null)
                {
                    threadFetch.Abort();
                    threadFetch = null;
                    usbCAN.shutDevice();
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
    }
}
