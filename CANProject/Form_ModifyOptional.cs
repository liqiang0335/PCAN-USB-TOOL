using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    public partial class Form_ModifyOptional : Form
    {
        public DialogResult retResult = DialogResult.Cancel;
        UDSServiceFormat udsReq;
        public int protocol;

        public Form_ModifyOptional(UDSServiceFormat uds, int pro)
        {
            InitializeComponent();

            udsReq = uds;
            protocol = pro;

            readUdsReq();
        }

        void readUdsReq()
        {
            comboBoxExternFlag.SelectedIndex = udsReq.format;
            textBoxCANID.Text = udsReq.address.ToString("X2");
            textBoxSID.Text = udsReq.serviceID.ToString("X2");

            string param = "";
            for (int i = 0; i < udsReq.parameterList.Count; i++)
                param += udsReq.parameterList[i].ToString("X2") + " ";
            textBoxPARAM.Text = param;

            textBoxDES.Text = udsReq.description;
            comboBoxPro.SelectedIndex = protocol;
        }

        bool isHexLetter(char ch)
        {
            if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                return true;
            else
                return false;
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (comboBoxExternFlag.SelectedIndex == -1)
            {
                MessageBox.Show("请指定帧格式");
                return;
            }
            byte externFlag = (byte)comboBoxExternFlag.SelectedIndex;

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

                string sHex = sParams.Substring(index, end - index);
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
            if (desp.Equals(""))
            {
                MessageBox.Show("描述不能为空");
                return;
            }

            udsReq.address = canID;
            udsReq.expectedFrame = 0;
            udsReq.format = externFlag;
            udsReq.serviceID = sID;
            udsReq.description = desp;
            udsReq.parameterList.Clear();
            for (int i = 0; i < listParam.Count; i++)
                udsReq.parameterList.Add(listParam[i]);

            if (comboBoxPro.SelectedIndex == -1)
            {
                MessageBox.Show("请选择协议类型");
                return;
            }

            protocol = comboBoxPro.SelectedIndex;
            retResult = DialogResult.OK;
            this.Close();
        }
    }
}
