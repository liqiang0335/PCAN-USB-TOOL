using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Peak.Can.Basic;

namespace CANProject
{
    public partial class Form_SetCANParam : Form
    {
        USBCAN usbCAN;
        public static Form_SetCANParam form_setcan;
        public Dictionary<int, USBCAN> USBCAN_dictionary;

        public Form_SetCANParam()
        {
            InitializeComponent();
        }

        public Form_SetCANParam(USBCAN can)
        {
            InitializeComponent();            
            textBoxBTR0.Text = textBoxBTR1.Text = "";
            usbCAN = can;

            uint device_count = USBCAN.detectCONNECT() ;
            if (device_count == 0xff) { MessageBox.Show("无设备连接！"); return; }
            int add_count = 0;
            while (device_count > 0)
            {
                device_count--;
                this.comboBoxCANIndex.Items.Add("设备" + add_count);
                add_count++;
            }

            if (usbCAN.CANIndex == 0 || usbCAN.CANIndex == 1)
            {
                comboBoxCANIndex.SelectedIndex = usbCAN.CANIndex;
                textBoxBTR0.Text = usbCAN.BTR0.ToString("X2");
                textBoxBTR1.Text = usbCAN.BTR1.ToString("X2");
            }
            CheckForIllegalCrossThreadCalls = false;
            form_setcan = this;
            this.应用.Visible = false;
        }

        public Form_SetCANParam(ref Dictionary<int, USBCAN> usbcan_dictionary)
        {
            InitializeComponent();
            textBoxBTR0.Text = textBoxBTR1.Text = "";
            this.USBCAN_dictionary = usbcan_dictionary;

            uint device_count = USBCAN.detectCONNECT();
            if (device_count == 0xff) { MessageBox.Show("无设备连接！"); return; }
            int add_count = 0;
            while (device_count > 0)
            {
                device_count--;
                this.comboBoxCANIndex.Items.Add("设备" + add_count);
                add_count++;
            }
            CheckForIllegalCrossThreadCalls = false;
            form_setcan = this;
            this.buttonOK.Visible = false;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxCANIndex.SelectedIndex == -1)
            {
                MessageBox.Show("请指定通道号");
                return;
            }
            if (comboBoxBaudRate.SelectedIndex == -1)
            {
                MessageBox.Show("请指定波特率");
                return;
            }

            if (textBoxBTR0.Text.Equals("") || textBoxBTR1.Text.Equals("") )
            {
                MessageBox.Show("请设置比特率寄存器");
                return;
            }

            byte BTR0 = 0, BTR1 = 0;
            try
            {
                BTR0 = Convert.ToByte(textBoxBTR0.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("BTR0转化成16进制失败");
                return;
            }
            try
            {
                BTR1 = Convert.ToByte(textBoxBTR1.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("BTR1转化成16进制失败");
                return;
            }

            usbCAN.PCAN_PARA1.BTR0_1 = PCAN_COMMON_VAL.BTR0_1_buf;
            //PCAN_COMMON_VAL_1.can_channel_1 = PCAN_COMMON_VAL.can_channel_buf;
            usbCAN.PCAN_PARA1.PCANIndex = PCAN_COMMON_VAL.can_channel_buf;
            usbCAN.BTR0 = BTR0;
            usbCAN.BTR1 = BTR1;
            usbCAN.CANIndex = (byte)comboBoxCANIndex.SelectedIndex;           
            this.Close();
        }

        private void 应用_Click(object sender, EventArgs e)
        {
            if (comboBoxCANIndex.SelectedIndex == -1)
            {
                MessageBox.Show("请指定通道号");
                return;
            }

            if (textBoxBTR0.Text.Equals("") || textBoxBTR1.Text.Equals(""))
            {
                MessageBox.Show("请设置比特率寄存器");
                return;
            }

            byte BTR0 = 0, BTR1 = 0;
            try
            {
                BTR0 = Convert.ToByte(textBoxBTR0.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("BTR0转化成16进制失败");
                return;
            }
            try
            {
                BTR1 = Convert.ToByte(textBoxBTR1.Text, 16);
            }
            catch (Exception)
            {
                MessageBox.Show("BTR1转化成16进制失败");
                return;
            }

            int index = comboBoxCANIndex.SelectedIndex;
            if (!USBCAN_dictionary.ContainsKey(index))
            {
                USBCAN_dictionary.Add(index, new USBCAN());
            }
            else
            {
                USBCAN_dictionary[index] = new USBCAN();
            }
            USBCAN_dictionary[index].BTR0 = BTR0;
            USBCAN_dictionary[index].BTR1 = BTR1;
            USBCAN_dictionary[index].PCAN_PARA1.PCANIndex = PCAN_COMMON_VAL.can_channel_buf;
            USBCAN_dictionary[index].PCAN_PARA1.BTR0_1 = PCAN_COMMON_VAL.BTR0_1_buf;
            MessageBox.Show("设定成功");
        }

        private void comboBoxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sBaudRate = comboBoxBaudRate.Items[comboBoxBaudRate.SelectedIndex].ToString();
            if (sBaudRate.Equals("自定义"))
            {
                textBoxBTR0.Text = textBoxBTR1.Text = "";
                textBoxBTR0.Enabled = true;
                textBoxBTR1.Enabled = true;
            }
            else
            {
                textBoxBTR0.Enabled = false;
                textBoxBTR1.Enabled = false;
                
                if (sBaudRate.Equals("1Mbps"))
                {
                    textBoxBTR0.Text = "00";
                    textBoxBTR1.Text = "14";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_1M;
                }
                else if (sBaudRate.Equals("800kbps"))
                {
                    textBoxBTR0.Text = "00";
                    textBoxBTR1.Text = "16";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_800K;

                }
                else if (sBaudRate.Equals("500kbps"))
                {
                    textBoxBTR0.Text = "00";
                    textBoxBTR1.Text = "1C";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_500K;

                }
                else if (sBaudRate.Equals("250kbps"))
                {
                    textBoxBTR0.Text = "01";
                    textBoxBTR1.Text = "1C";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_250K;

                }
                else if (sBaudRate.Equals("125kbps"))
                {
                    textBoxBTR0.Text = "03";
                    textBoxBTR1.Text = "1C";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_125K;

                }
                else if (sBaudRate.Equals("100kbps"))
                {
                    textBoxBTR0.Text = "43";
                    textBoxBTR1.Text = "2F";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_100K;

                }
                else if (sBaudRate.Equals("95kbps"))
                {
                    textBoxBTR0.Text = "C3";
                    textBoxBTR1.Text = "4E";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_95K;

                }
                else if (sBaudRate.Equals("83kbps"))
                {
                    textBoxBTR0.Text = "85";
                    textBoxBTR1.Text = "2B";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_83K;

                }
                else if (sBaudRate.Equals("50kbps"))
                {
                    textBoxBTR0.Text = "47";
                    textBoxBTR1.Text = "2F";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_50K;

                }
                else if (sBaudRate.Equals("47kbps"))
                {
                    textBoxBTR0.Text = "14";
                    textBoxBTR1.Text = "14";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_47K;

                }
                else if (sBaudRate.Equals("33kbps"))
                {
                    textBoxBTR0.Text = "8B";
                    textBoxBTR1.Text = "2F";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_33K;

                }
                else if (sBaudRate.Equals("20kbps"))
                {
                    textBoxBTR0.Text = "53";
                    textBoxBTR1.Text = "2F";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_20K;

                }
                else if (sBaudRate.Equals("10kbps"))
                {
                    textBoxBTR0.Text = "67";
                    textBoxBTR1.Text = "2F";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_10K;

                }
                else if (sBaudRate.Equals("5kbps"))
                {
                    textBoxBTR0.Text = "7F";
                    textBoxBTR1.Text = "7F";
                    PCAN_COMMON_VAL.BTR0_1_buf = Peak.Can.Basic.TPCANBaudrate.PCAN_BAUD_5K;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public uint Check_opendevice_ifFD()//检测打开当前的设备是否支持FD
        {
            TPCANStatus stsResult;
            uint iChannelsCount = 0;
            stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS_COUNT, out iChannelsCount, sizeof(uint));
            if (stsResult == TPCANStatus.PCAN_ERROR_OK)
            {
                TPCANChannelInformation[] info = new TPCANChannelInformation[iChannelsCount];
                stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS, info);
                if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                {
                    //Form_BasicFunction.form_basic.checkEnableCANFD.Checked = false;
                    //Form_BasicFunction.form_basic.EnableCANFD_BRS.Enabled = false;
                    //Form_BasicFunction.form_basic.EnableCANFD_BRS.Checked = false;
                    if (PCAN_COMMON_VAL.can_channel_buf == 0x0051)//channel = 0 即通道1
                    {
                        if (info[0].device_name == "PCAN-USB FD")
                        {
                            checkCANFDMode.Enabled = true;
                        }
                        else
                        {
                            checkCANFDMode.Enabled = false;
                        }
                    }
                    else if (PCAN_COMMON_VAL.can_channel_buf == 0x0052)//channel = 1 即通道2
                    {
                        if (info[1].device_name == "PCAN-USB FD")
                            checkCANFDMode.Enabled = true;
                        else
                            checkCANFDMode.Enabled = false;
                    }

                }
                return iChannelsCount;
            }
            else
                return 0xff;//error
        }

        private void comboBoxCANIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string can_chnnel = comboBoxCANIndex.Items[comboBoxCANIndex.SelectedIndex].ToString();         
            //if (can_chnnel.Equals("通道1"))
            //{
            //    PCAN_COMMON_VAL.can_channel_buf = 0x0051;
            //}
            //if (can_chnnel.Equals("通道2"))
            //{
            //    PCAN_COMMON_VAL.can_channel_buf = 0x0052;
            //}
            PCAN_COMMON_VAL.can_channel_buf = (UInt16)(comboBoxCANIndex.SelectedIndex + 0x51);
            Check_opendevice_ifFD();
        }

        private void textBoxBTR0_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxBTR1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_SetCANParam_Load(object sender, EventArgs e)
        {

        }

        private void checkEnableCANFD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkCANFDMode_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
