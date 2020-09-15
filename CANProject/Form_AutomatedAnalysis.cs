using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CANProject
{
    public partial class Form_AutomatedAnalysis : Form
    {
        USBCAN usbCAN;
        UDSBASEDCAN udsCAN;

        public Form_AutomatedAnalysis()
        {
            InitializeComponent();
            //
            usbCAN = new USBCAN(getRawPacket);
            udsCAN = new UDSBASEDCAN(usbCAN);

            chartCmp.Series.Clear();

            readUdsInstructions(Application.StartupPath+@"\OPTIONAL.txt");
            //test
            ANALYSISRESULT testResult = new ANALYSISRESULT();
            testResult.udsReq.description = "[测试数据]大众途安发动机扭矩";
            testResult.udsReq.format = 0;
            testResult.udsReq.expectedFrame = 0;
            testResult.udsReq.address = 0x7E0;
            testResult.udsReq.serviceID = 0x22;
            testResult.udsReq.parameterList.Add(0x10);
            testResult.udsReq.parameterList.Add(0x10);

            testResult.path = Application.StartupPath + @"\dataPacket\[测试数据]大众途安发动机扭矩.txt";
            analysisResults.Add(testResult);

            listBoxResult.Items.Add(testResult.udsReq.description);
        }
        /*---------------------------------------------------------------------------------------------------*/
        List<CAN_OBJ> rawPacket = new List<CAN_OBJ>();
        bool hasGetUds = false;

        unsafe void getRawPacket(ref VCI_CAN_OBJ canObj)
        {
            if (canObj.ID >= 0x7E8)
                hasGetUds = true;

            CAN_OBJ obj = new CAN_OBJ();
            obj.ID = canObj.ID;
            obj.RemoteFlag = canObj.RemoteFlag;
            obj.ExternFlag = canObj.ExternFlag;
            obj.DataLen = canObj.DataLen;
            for (int i = 0; i < 8; i++)
                obj.Data[i] = canObj.Data[i];

            rawPacket.Add(obj);
        }
        /*---------------------------------------------------------------------------------------------------*/
        class ANALYSISRESULT
        {
            public UDSServiceFormat udsReq = new UDSServiceFormat();
            public string path;
        }

        List<ANALYSISRESULT> analysisResults = new List<ANALYSISRESULT>();

        /*---------------------------------------------------------------------------------------------------*/
        List<CAN_OBJ> dataPacket = new List<CAN_OBJ>();
        void readDataPacket(string path)
        {
            dataPacket.Clear();

            StreamReader sr = new StreamReader(path);
            string line = null;
            while ( (line = sr.ReadLine()) != null)
            {
                string[] arr = line.Split(' ');
                CAN_OBJ can = new CAN_OBJ();
                can.ID = Convert.ToUInt32(arr[0], 16);
                can.DataLen = Convert.ToByte(arr[1]);

                for (int d = 2; d < arr.Length - 1 && d < 10; d++)
                    can.Data[d - 2] = Convert.ToByte(arr[d], 16);

                dataPacket.Add(can);
            }
            sr.Close();
        }

        AUTOMATICFITTING autoFitting = new AUTOMATICFITTING();
        List<AUTOMATICFITTING.CALCULATIONRESULT> calcResults;

        private void listBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxResult.SelectedIndex == -1)
                return;

            readDataPacket(analysisResults[listBoxResult.SelectedIndex].path);

            showFittingResult();
        }

        void showFittingResult()
        {
            int endian = 0;
            if (radioButtonBigEndian.Checked)
                endian = 0;
            else if (radioButtonSmallEndian.Checked)
                endian = 1;
            else
            {
                MessageBox.Show("请选择端序");
                return;
            }

            double digit = 0;
            if (radioButton1Byte.Checked)
                digit = 1;
            else if (radioButton1_5Byte.Checked)
                digit = 1.5;
            else if (radioButton2Byte.Checked)
                digit = 2;
            else if (radioButton0_5Byte.Checked)
                digit = 0.5;
            else
            {
                MessageBox.Show("请选择字节数");
                return;
            }

            calcResults = autoFitting.toGetResult(dataPacket, analysisResults[listBoxResult.SelectedIndex].udsReq, digit, endian);

            listBoxItems.Items.Clear();
            for (int i = 0; i < calcResults.Count; i++)
            {
                string item = string.Format("ID:0x{0};DATA{1};标准差={2}", calcResults[i].id.ToString("X2"), calcResults[i].startIndex, calcResults[i].standardDeviation.ToString("f2"));
                listBoxItems.Items.Add(item);
            }

            if (calcResults.Count > 0)
                listBoxItems.SelectedIndex = 0;
        }

        void drawComparison(int index)
        {
            chartCmp.Series.Clear();
            chartCmp.Series.Dispose();

            double xMin = double.MaxValue, xMax = double.MinValue;

            chartCmp.Titles.Clear();
            string title = string.Format("y={0}x", calcResults[index].coefficient.w.ToString("f2"));
            if (calcResults[index].coefficient.b >= 0)
                title += "+" + calcResults[index].coefficient.b.ToString("f2");
            else
                title += calcResults[index].coefficient.b.ToString("f2");

            chartCmp.Titles.Add(title);

            Series dc = new Series("CAN");
            dc.ChartType = SeriesChartType.FastPoint;
            for (int i = 0; i < calcResults[index].points.Count; i++)
            {
                double x = calcResults[index].points[i].X;
                double y = calcResults[index].points[i].Y;
                dc.Points.AddXY(x, y);
                if (x < xMin)
                    xMin = x;
                if (x > xMax)
                    xMax = x;
            }

            chartCmp.ChartAreas[0].AxisX.Maximum = xMax;
            chartCmp.ChartAreas[0].AxisX.Minimum = xMin;
            chartCmp.Series.Add(dc);

            Series ct = new Series("UDS");
            ct.ChartType = SeriesChartType.FastLine;
            ct.Points.AddXY(xMin, calcResults[index].coefficient.w * xMin + calcResults[index].coefficient.b);
            ct.Points.AddXY(xMax, calcResults[index].coefficient.w * xMax + calcResults[index].coefficient.b);
            ct.BorderWidth = 2;
            chartCmp.Series.Add(ct);
        }
        /*---------------------------------------------------------------------------------------------------*/
        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedIndex == -1)
                return;

            drawComparison(listBoxItems.SelectedIndex);
        }

        private void radioButtonBigEndian_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                showFittingResult();
        }

        private void radioButton1Byte_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                showFittingResult();
        }

        private void radioButtonSmallEndian_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                showFittingResult();
        }

        private void radioButton2Byte_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                showFittingResult();
        }

        private void radioButton0_5Byte_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                showFittingResult();
        }

        private void radioButton1_5Byte_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                showFittingResult();
        }
        /*---------------------------------------------------------------------------------------------------*/
        List<UDSServiceFormat> listOptionals = new List<UDSServiceFormat>();
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form_AddOptional form = new Form_AddOptional();
            form.ShowDialog();
            if (form.udsReq != null)
            {
                listOptionals.Add(form.udsReq);
                string desp;
                if (form.protocolType == 0)
                    desp = "[OBD]";
                else
                    desp = "[UDS]";

                desp += form.udsReq.description;
                listBoxOptional.Items.Add(desp);
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        List<UDSServiceFormat> listRequests = new List<UDSServiceFormat>();
        private void buttonAddReq_Click(object sender, EventArgs e)
        {
            if (listBoxOptional.SelectedIndex != -1)
            {
                listRequests.Add(listOptionals[listBoxOptional.SelectedIndex]);
                listBoxRequest.Items.Add(listBoxOptional.Items[listBoxOptional.SelectedIndex].ToString() );
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        bool wantTerminate = false;
        int DELAY = 500;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int pointCount = 0;
            try { pointCount = Convert.ToInt32(textBoxPointCount.Text); }
            catch (Exception) { MessageBox.Show("请指定收集点个数"); return; }

            int udsInterval = 0;
            try { udsInterval = Convert.ToInt32(textBoxUdsInterval.Text); }
            catch (Exception) { MessageBox.Show("请指定指令发送间隔"); return; }
            //
            richTextBoxLog.Text = "";
            buttonEnd.Enabled = true;
            buttonStart.Enabled = false;
            groupBoxResult.Enabled = false;
            rawPacket.Clear();
            hasGetUds = false;
            DateTime prev;

            for (int reqIndex = 0; reqIndex < listRequests.Count; reqIndex++)
            {
                if (usbCAN.startDevice() == false)
                    goto endProcess;

                udsCAN.createService(listRequests[reqIndex]);
                for (int cnt = pointCount; cnt > 0;)
                {
                    for (int reTry = 0; reTry < 10 && wantTerminate == false; reTry++)
                    {                      
                        prev = DateTime.Now;
                        while ((DateTime.Now - prev).TotalMilliseconds < DELAY)
                            Application.DoEvents();

                        hasGetUds = false;
                        usbCAN.sendFame(1);

                        prev = DateTime.Now;
                        while (hasGetUds == false && (DateTime.Now - prev).TotalMilliseconds < 1200)
                            Application.DoEvents();
                        if (hasGetUds == false)
                            continue;

                        break;
                    }

                    if (wantTerminate)
                        goto endProcess;

                    if (hasGetUds == false)
                        richTextBoxLog.AppendText("超时，请检查设备\n");
                    else
                    {
                        cnt--;
                        richTextBoxLog.AppendText(listRequests[reqIndex].description + "：剩余点个数 " + cnt.ToString() + "\n");
                    }

                    prev = DateTime.Now;
                    while ((DateTime.Now - prev).TotalMilliseconds < udsInterval)
                        Application.DoEvents();
                }

                //保存文件
                if (usbCAN.shutDevice() == false)
                    goto endProcess;

                string path = Application.StartupPath + @"\dataPacket\";
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                path += listRequests[reqIndex].description + Convert.ToUInt64(ts.TotalSeconds).ToString();
                FileStream file = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(file);

                string line;
                for (int i = 0; i < rawPacket.Count; i++)
                {
                    line = rawPacket[i].ID.ToString("X2") + " ";
                    line += rawPacket[i].DataLen.ToString() + " ";
                    for (int d = 0; d < rawPacket[i].DataLen; d++)
                        line += rawPacket[i].Data[d].ToString("X2") + " ";

                    sw.WriteLine(line);
                }

                sw.Close();
                file.Close();

                ANALYSISRESULT result = new ANALYSISRESULT();
                result.udsReq = listRequests[reqIndex];
                result.path = path;
                analysisResults.Add(result);
                listBoxResult.Items.Add(result.udsReq.description);
            }

        endProcess:
            buttonEnd.Enabled = false;
            buttonStart.Enabled = true;
            groupBoxResult.Enabled = true;
            wantTerminate = false;
        }

        private void 设备参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SetCANParam form = new Form_SetCANParam(usbCAN);
            form.ShowDialog();
        }

        private void buttonRemoveReq_Click(object sender, EventArgs e)
        {
            if (listBoxRequest.SelectedIndex != -1)
            {
                listRequests.RemoveAt(listBoxRequest.SelectedIndex);
                listBoxRequest.Items.RemoveAt(listBoxRequest.SelectedIndex);
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            int index = listBoxOptional.SelectedIndex;
            if (index != -1)
            {
                string sName = listBoxOptional.Items[index].ToString() ;
                int pro = 0;
                if (sName.IndexOf("[OBD]") != -1)
                    pro = 0;
                else
                    pro = 1;

                Form_ModifyOptional form = new Form_ModifyOptional(listOptionals[index], pro);
                form.ShowDialog();
                if (form.retResult == DialogResult.OK)
                {
                    if (form.protocol == 0)
                        sName = "[OBD]";
                    else
                        sName = "[UDS]";

                    listBoxOptional.Items[index] = sName + listOptionals[index].description;
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = listBoxOptional.SelectedIndex;
            if (index != -1)
            {
                int reqIndex = -1;
                while ((reqIndex = listBoxRequest.Items.IndexOf(listBoxOptional.Items[index])) != -1)
                {
                    listBoxRequest.Items.RemoveAt(reqIndex);
                    listRequests.RemoveAt(reqIndex);
                }

                listBoxOptional.Items.RemoveAt(index);
                listOptionals.RemoveAt(index);
            }
        }

        private void 清空物理量列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxRequest.Items.Clear();
            listRequests.Clear();

            listBoxOptional.Items.Clear();
            listOptionals.Clear();
        }

        private void 清空待测列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxRequest.Items.Clear();
            listRequests.Clear();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            wantTerminate = true;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void readUdsInstructions(string path)
        {
            listBoxOptional.Items.Clear();
            listOptionals.Clear();

            StreamReader sr = new StreamReader(path);

            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                UDSServiceFormat uds = new UDSServiceFormat();

                string[] arr = line.Split('\t');
                int len = arr.Length;
                int despSt = -1, despEd = -1;
                if ((despSt = arr[len - 1].IndexOf('"')) != -1)
                {
                    len--;
                    if ((despEd = arr[len].IndexOf('"', despSt + 1)) != -1)
                        uds.description = arr[len].Substring(despSt + 1, despEd - despSt - 1);
                }

                try { uds.address = Convert.ToUInt32(arr[0], 16); }
                catch (Exception) { continue; }

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
                {
                    listBoxOptional.Items.Add(uds.description);
                    uds.description = uds.description.Replace("[OBD]", "");
                    uds.description = uds.description.Replace("[UDS]", "");
                    listOptionals.Add(uds);
                }
            }

            sr.Close();
        }

        private void Form_AutomatedAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("是否保存物理量列表？", "", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel)
                e.Cancel = true;
            else if (result == DialogResult.Yes)
            {
                writeUdsInstructions(Application.StartupPath + @"\OPTIONAL.txt");
            }
        }

        void writeUdsInstructions(string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(file);

            string line;
            for (int i = 0; i < listBoxOptional.Items.Count; i++)
            {
                line = listOptionals[i].address.ToString("X2") + "\t";
                line += listOptionals[i].serviceID.ToString("X2") + "\t";
                for (int p = 0; p < listOptionals[i].parameterList.Count; p++)
                    line += listOptionals[i].parameterList[p].ToString("X2") + "\t";
                line += "\"" + listBoxOptional.Items[i].ToString() + "\"";

                sw.WriteLine(line);
            }

            sw.Close();
            file.Close();
        }
    }
}
