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

namespace CANProject
{
    public partial class Form_AddOptional : Form
    {
        public Form_AddOptional()
        {
            InitializeComponent();
            readUdsInstructions(Application.StartupPath+@"\OBD.txt", listOBD);

            listBoxOBD.Items.Clear();
            for (int i = 0; i < listOBD.Count; i++)
                listBoxOBD.Items.Add(listOBD[i].description);
        }
        /*---------------------------------------------------------------------------------------------------*/
        public int protocolType = -1;
        public UDSServiceFormat udsReq = null;
        /*---------------------------------------------------------------------------------------------------*/
        List<UDSServiceFormat> listOBD = new List<UDSServiceFormat>();
        void readUdsInstructions(string path, List<UDSServiceFormat> list)
        {
            list.Clear();

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
                    list.Add(uds);
            }

            sr.Close();
        }

        private void buttonAddOBD_Click(object sender, EventArgs e)
        {
            byte externFlag = 0;
            if (radioButtonOS.Checked)
                externFlag = 0;
            else if (radioButtonOE.Checked)
                externFlag = 1;
            else
            {
                MessageBox.Show("请指定帧格式");
                return;
            }

            if (listBoxOBD.SelectedIndex == -1)
            {
                MessageBox.Show("请指定OBD请求");
                return;
            }

            protocolType = 0;
            udsReq = listOBD[listBoxOBD.SelectedIndex];
            udsReq.format = externFlag;
            this.Close();
        }

        private void buttonAddUDS_Click(object sender, EventArgs e)
        {
            byte externFlag = 0;
            if (radioButtonUS.Checked)
                externFlag = 0;
            else if (radioButtonUE.Checked)
                externFlag = 1;
            else
            {
                MessageBox.Show("请指定帧格式");
                return;
            }

            uint canID = 0;
            try { canID = Convert.ToUInt32(textBoxCANID.Text, 16); }
            catch (Exception) { MessageBox.Show("请输入正确的16进制CAN ID"); return; }

            byte sID = 0;
            try { sID = Convert.ToByte(textBoxSID.Text, 16); }
            catch (Exception) { MessageBox.Show("请输入正确的16进制SID"); return; }

            List<byte> listParam = new List<byte>();
            string sParams = textBoxPARAM.Text;
            for (int index = 0; index < sParams.Length;)
            {
                for (; index < sParams.Length && isHexLetter(sParams[index]) == false; index++) ;
                if (index >= sParams.Length)
                    break;

                int end;
                for (end = index + 1; end < sParams.Length && isHexLetter(sParams[end]); end++) ;

                string sHex = sParams.Substring(index, end-index);
                byte bHex = 0;
                try { bHex = Convert.ToByte(sHex, 16); }
                catch (Exception) { MessageBox.Show("请输入正确的16进制参数"); return; }

                listParam.Add(bHex);
                index = end + 1;
            }

            if (listParam.Count == 0)
            {
                MessageBox.Show("参数不能为空");
                return;
            }

            string desp = textBoxDES.Text;
            desp = desp.Replace(" ", "");
            desp = desp.Replace("\t", "");
            if (desp.Equals("") )
            {
                MessageBox.Show("描述不能为空");
                return;
            }

            UDSServiceFormat uds = new UDSServiceFormat();
            uds.address = canID;
            uds.expectedFrame = 0;
            uds.format = externFlag;
            uds.serviceID = sID;
            uds.description = desp;
            for (int i = 0; i < listParam.Count; i++)
                uds.parameterList.Add(listParam[i]);

            protocolType = 1;
            udsReq = uds;
            this.Close();
        }
        /*---------------------------------------------------------------------------------------------------*/
        bool isHexLetter(char ch)
        {
            if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                return true;
            else
                return false;
        }
    }
}
