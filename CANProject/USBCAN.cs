using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Peak.Can.Basic;
using TPCANHandle = System.UInt16;
using TPCANBitrateFD = System.String;
using TPCANTimestampFD = System.UInt64;
using System.Security.Cryptography.X509Certificates;

namespace CANProject
{
    using TPCANHandle = System.UInt16;
    using TPCANBitrateFD = System.String;
    using TPCANTimestampFD = System.UInt64;
    public class CAN_OBJ
    {
        public uint ID;
        public byte RemoteFlag;       //是否是远程帧
        public byte ExternFlag;       //是否是扩展帧
        public byte DataLen;          //数据长度
        public byte[] Data = new byte[8];    //数据

        public CAN_OBJ() { }
        public CAN_OBJ(CAN_OBJ canObj)
        {
            ID = canObj.ID;
            RemoteFlag = canObj.RemoteFlag;
            ExternFlag = canObj.ExternFlag;
            DataLen = canObj.DataLen;
            for (int i = 0; i < 8; i++)
                Data[i] = canObj.Data[i];
        }
    }
    public class PCAN_COMMON_VAL
    {
        //TPCANStatus pcan_init_flag = TPCANStatus.PCAN_ERROR_BUSOFF;
        public static byte pcan_init_flag0 = 0xff;
        public static byte pcan_init_flag1 = 0xff;
        public static byte pcan_init_flag2 = 0xff;
        public static byte pcan_init_flag3 = 0xff;
        public static byte pcan_init_flag4 = 0xff;
        public static byte pcan_init_flag5 = 0xff;
        public static byte pcan_init_flag6 = 0xff;
        public static byte pcan_init_flag7 = 0xff;

        public static TPCANBaudrate BTR0_1_buf;
        public static TPCANHandle can_channel_buf;


    }
    unsafe public struct VCI_CAN_OBJ  //使用不安全代码
    {
        public uint ID;
        public UInt64 TimeStamp;        //时间标识
        public byte TimeFlag;         //是否使用时间标识
        public byte SendType;         //发送标志。保留，未用
        public byte RemoteFlag;       //是否是远程帧
        public byte ExternFlag;       //是否是扩展帧
        public byte DataLen;          //数据长度
        public fixed byte Data[64];    //数据
        public fixed byte Reserved[3];//保留位        
        public byte FDflag;//FD Package
        public byte FDBrs;          //FD BRS
        public byte FDEsi;          //FD errror status indicator
        public byte ErrorFrame;          // error frame
    }

    public class USBCAN
    {
        [DllImport("controlcan.dll")]
        static extern int VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
        [DllImport("controlcan.dll")]
        static extern int VCI_InitCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_INIT_CONFIG pInitConfig);
        [DllImport("controlcan.dll")]
        static extern int VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        [DllImport("controlcan.dll")]
        static extern int VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pReceive, UInt32 Len, Int32 WaitTime);
        [DllImport("controlcan.dll")]
        static extern int VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
        [DllImport("controlcan.dll")]
        static extern int VCI_Transmit(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pSend, UInt32 Len);

        public struct VCI_INIT_CONFIG
        {
            public UInt32 AccCode;
            public UInt32 AccMask;
            public UInt32 Reserved;
            public byte Filter;   //0或1接收所有帧。2标准帧滤波，3是扩展帧滤波。
            public byte Timing0;  //波特率参数，具体配置，请查看二次开发库函数说明书。
            public byte Timing1;
            public byte Mode;     //模式，0表示正常模式，1表示只听模式,2自测模式
        }

        public USBCAN(processingFramefunc processingFramefunction)
        {
            ProcessingFrame = processingFramefunction;

            config.AccCode = 0x00000000;
            config.AccMask = 0xFFFFFFFF;
            config.Filter = 0x00;
            config.Mode = 0;
        }

        public USBCAN()
        {
            config.AccCode = 0x00000000;
            config.AccMask = 0xFFFFFFFF;
            config.Filter = 0x00;
            config.Mode = 0;
            ProcessingFrame = null;
        }

        public bool IsOpen = false;

        Thread thread = null;

        VCI_INIT_CONFIG config = new VCI_INIT_CONFIG();

        byte CANINDEX = 0xFF;
        /*---------------------------------------------------------------------------------------------------*/
        public byte BTR0
        {
            set { config.Timing0 = value; }
            get { return config.Timing0; }
        }

        public byte BTR1
        {
            set { config.Timing1 = value; }
            get { return config.Timing1; }
        }
        public struct PCAN_PARA
        {
            public TPCANBaudrate BTR0_1;
            public TPCANHandle PCANIndex;
            public bool canfd_en;
            public bool canfd_brs_en;

        }
        public PCAN_PARA PCAN_PARA1 = new PCAN_PARA();
        public byte CANIndex
        {
            set { CANINDEX = value; }
            get { return CANINDEX; }
        }


        public byte USBMode
        {
            set { config.Mode = value; }
            get { return config.Mode; }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public bool startDevice()
        {
            TPCANStatus result1 = 0;           
            //if (CANINDEX != 0 && CANINDEX != 1)
            //{
            //    MessageBox.Show("请指定设备参数");
            //    return false;
            //}
            if (Form_SetCANParam.form_setcan.checkCANFDMode.Checked == true)
                result1 = PCANBasic.InitializeFD(PCAN_PARA1.PCANIndex, "f_clock_mhz=20, nom_brp=5, nom_tseg1=2, nom_tseg2=1, nom_sjw=1, data_brp=2, data_tseg1=3, data_tseg2=1, data_sjw=1");
            else
                result1 = PCANBasic.Initialize(PCAN_PARA1.PCANIndex, PCAN_PARA1.BTR0_1, TPCANType.PCAN_TYPE_ISA, 0x378, 3);
            if (result1 == TPCANStatus.PCAN_ERROR_OK)
            {
                //MessageBox.Show("设备启动成功");
                switch (PCAN_PARA1.PCANIndex)
                {
                    case 0x0051:
                        PCAN_COMMON_VAL.pcan_init_flag0 = 0x69;//通道1初始化成功标志
                        break;
                    case 0x0052:
                        PCAN_COMMON_VAL.pcan_init_flag1 = 0x69;//通道1初始化成功标志}
                        break;
                    case 0x0053:
                        PCAN_COMMON_VAL.pcan_init_flag2 = 0x69;//通道1初始化成功标志
                        break;
                    case 0x0054:
                        PCAN_COMMON_VAL.pcan_init_flag3 = 0x69;//通道1初始化成功标志}
                        break;
                    case 0x0055:
                        PCAN_COMMON_VAL.pcan_init_flag4 = 0x69;//通道1初始化成功标志
                        break;
                    case 0x0056:
                        PCAN_COMMON_VAL.pcan_init_flag5 = 0x69;//通道1初始化成功标志}
                        break;
                    case 0x0057:
                        PCAN_COMMON_VAL.pcan_init_flag6 = 0x69;//通道1初始化成功标志
                        break;
                    case 0x0058:
                        PCAN_COMMON_VAL.pcan_init_flag7 = 0x69;//通道1初始化成功标志}
                        break;
                }
                if (Form_SetCANParam.form_setcan.checkCANFDMode.Checked == true)
                {
                    Form_BasicFunction.form_basic.checkEnableCANFD.Enabled = true;
                    Form_BasicFunction.form_basic.EnableCANFD_BRS.Enabled = true;
                }
                else
                {
                    Form_BasicFunction.form_basic.checkEnableCANFD.Checked = false;
                    Form_BasicFunction.form_basic.checkEnableCANFD.Enabled = false;
                    Form_BasicFunction.form_basic.EnableCANFD_BRS.Enabled = false;
                    Form_BasicFunction.form_basic.EnableCANFD_BRS.Checked = false;
                }
                if (thread == null)
                //if(true)//方便连续直接打开设备
                {
                   
                    thread = new Thread(receivingThread);
                    thread.Start();
                }
                IsOpen = true;
                return true;
            }
            else
            {                               
                goto errorReporting;
            }

        errorReporting:
            if (result1 == TPCANStatus.PCAN_ERROR_ILLHW)
                MessageBox.Show("请插入PCAN-USB设备");
            else
                MessageBox.Show("PCAN-USB打开失败");
            return false;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public bool shutDevice()
        {
            TPCANStatus result1;
            result1 = PCANBasic.Uninitialize(PCAN_PARA1.PCANIndex);
            if (result1 == TPCANStatus.PCAN_ERROR_OK)
            {
                if (thread != null)
                {
                    thread.Abort();
                    thread = null;
                }
                //MessageBox.Show("关闭设备成功");
                switch (PCAN_PARA1.PCANIndex)
                {
                    case 0x0051:
                        PCAN_COMMON_VAL.pcan_init_flag0 = 0xff;//通道0关闭成功标志
                        break;
                    case 0x0052:
                        PCAN_COMMON_VAL.pcan_init_flag1 = 0xff;//通道1关闭成功标志
                        break;
                    case 0x0053:
                        PCAN_COMMON_VAL.pcan_init_flag2 = 0xff;//通道2关闭成功标志
                        break;
                    case 0x0054:
                        PCAN_COMMON_VAL.pcan_init_flag3 = 0xff;//通道3关闭成功标志
                        break;
                    case 0x0055:
                        PCAN_COMMON_VAL.pcan_init_flag4 = 0xff;//通道4关闭成功标志
                        break;
                    case 0x0056:
                        PCAN_COMMON_VAL.pcan_init_flag5 = 0xff;//通道5关闭成功标志
                        break;
                    case 0x0057:
                        PCAN_COMMON_VAL.pcan_init_flag6 = 0xff;//通道6关闭成功标志
                        break;
                    case 0x0058:
                        PCAN_COMMON_VAL.pcan_init_flag7 = 0xff;//通道7关闭成功标志
                        break;
                }              
                Thread.Sleep(100);
                IsOpen = false;
                return true;
            }
            else
                goto errorReporting;

            errorReporting:
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
            if (result1 == TPCANStatus.PCAN_ERROR_INITIALIZE)
                MessageBox.Show("关闭设备失败");
            else
                MessageBox.Show("关闭设备失败");

            return false;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public delegate void processingFramefunc(ref VCI_CAN_OBJ canObj);

        public processingFramefunc ProcessingFrame;

        private VCI_CAN_OBJ[] arrCANOBJ = new VCI_CAN_OBJ[33000];

        public List<VCI_CAN_OBJ> rxCANOBJLIST = new List<VCI_CAN_OBJ>();
        public UInt64 rx_pkg_counts = 0;        
        unsafe private void receivingThread()
        {
            int i;
            int rx_index = 0;
            rxCANOBJLIST.Clear();
            TPCANStatus stsResult = 0;
            while (true)
            {
                //arrCANOBJ[33000] = new VCI_CAN_OBJ();
                do
                {
                    if (Form_SetCANParam.form_setcan.checkCANFDMode.Checked == false)//非FD
                    {
                        TPCANMsg CANMsg;
                        TPCANTimestamp CANTimeStamp;
                        stsResult = PCANBasic.Read(PCAN_PARA1.PCANIndex, out CANMsg, out CANTimeStamp);                        
                        //if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                        if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                        {
                            arrCANOBJ[rx_index].ID = CANMsg.ID;
                            arrCANOBJ[rx_index].DataLen = CANMsg.LEN;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x02) == 0x02)//是扩展帧
                                arrCANOBJ[rx_index].ExternFlag = 1;
                            else
                                arrCANOBJ[rx_index].ExternFlag = 0;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x01) == 0x01)//是请求帧
                                arrCANOBJ[rx_index].RemoteFlag = 1;
                            else
                                arrCANOBJ[rx_index].RemoteFlag = 0;

                            arrCANOBJ[rx_index].TimeStamp = Convert.ToUInt64(CANTimeStamp.micros + 1000 * CANTimeStamp.millis + 0x100000000 * 1000 * CANTimeStamp.millis_overflow);
                            
                            for (i = 0; i < CANMsg.LEN; i++)//原来用8固定长度
                            {
                                arrCANOBJ[rx_index].Data[i] = CANMsg.DATA[i];
                            }
                            rx_pkg_counts++;
                            rx_index++;
                            if (rx_index > 32768)
                                rx_index = 32767;
                            //Form_BasicFunction.form_basic.RXCountsLabel.Text = "Rx:" + rx_pkgcounts;//耗时2ms
                                /* if (ProcessingFrame == null)
                                 {
                                     if (rxCANOBJLIST.Count >= 100000)
                                     {
                                         MessageBox.Show("设备" + (PCAN_PARA1.PCANIndex - 0x51) + "接收缓冲区已满，已自动清空");
                                         rxCANOBJLIST.Clear();
                                     }
                                     rxCANOBJLIST.Add(arrCANOBJ[0]);
                                 }
                                 else
                                 {
                                     ProcessingFrame(ref arrCANOBJ[0]);                                
                                 }*/

                        }                        
                        if (stsResult == TPCANStatus.PCAN_ERROR_ILLOPERATION)
                            break;
                        //else                                               
                    }
                    else
                    {
                        TPCANMsgFD CANMsg;
                        TPCANTimestampFD CANTimeStamp;
                        stsResult = PCANBasic.ReadFD(PCAN_PARA1.PCANIndex, out CANMsg, out CANTimeStamp);
                        if (stsResult == TPCANStatus.PCAN_ERROR_ILLOPERATION)
                            break;
                        //if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                        if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                        {
                            arrCANOBJ[rx_index].ID = CANMsg.ID;
                            arrCANOBJ[rx_index].DataLen = CANMsg.DLC;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x02) == 0x02)//是扩展帧
                                arrCANOBJ[rx_index].ExternFlag = 1;
                            else
                                arrCANOBJ[rx_index].ExternFlag = 0;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x01) == 0x01)//是请求帧
                                arrCANOBJ[rx_index].RemoteFlag = 1;
                            else
                                arrCANOBJ[rx_index].RemoteFlag = 0;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x04) == 0x04)//是FD帧
                                arrCANOBJ[rx_index].FDflag = 1;
                            else
                                arrCANOBJ[rx_index].FDflag = 0;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x08) == 0x08)//是FD-BRS
                                arrCANOBJ[rx_index].FDflag = 1;
                            else
                                arrCANOBJ[rx_index].FDflag = 0;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x10) == 0x10)//是FD-ESI error status indicator
                                arrCANOBJ[rx_index].FDEsi = 1;
                            else
                                arrCANOBJ[rx_index].FDEsi = 0;

                            if (((byte)CANMsg.MSGTYPE & (byte)0x40) == 0x40)//是error frame
                                arrCANOBJ[rx_index].ErrorFrame = 1;
                            else
                                arrCANOBJ[rx_index].ErrorFrame = 0;

                            arrCANOBJ[rx_index].TimeStamp = CANTimeStamp;
                            for (i = 0; i < CANMsg.DLC; i++)//原来用8固定长度
                            {
                                arrCANOBJ[0].Data[i] = CANMsg.DATA[i];
                            }
                            rx_pkg_counts++;
                            rx_index++;
                            if (rx_index > 32768)
                                rx_index = 32767;
                            // for (i = 0; i < len; i++)
                            //ProcessingFrame(ref arrCANOBJ[0]);
                            /*if (ProcessingFrame == null)
                            {
                                if (rxCANOBJLIST.Count >= 100000)
                                {
                                    MessageBox.Show("设备" + (PCAN_PARA1.PCANIndex - 0x51) + "接收缓冲区已满，已自动清空");
                                    rxCANOBJLIST.Clear();
                                }
                                rxCANOBJLIST.Add(arrCANOBJ[0]);
                            }
                            else
                            {
                                ProcessingFrame(ref arrCANOBJ[0]);
                            }*/
                        }
                        //else                        
                    }
                } while (!Convert.ToBoolean(stsResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
                if (rx_index > 0)
                {
                    for (i = 0; i < rx_index; i++)
                    {
                        ProcessingFrame(ref arrCANOBJ[i]);
                        Thread.Sleep(1);
                    }
                    rx_index = 0;
                }                                           
                Thread.Sleep(2);                
            }
        }
        /*---------------------------------------------------------------------------------------------------*/

        public VCI_CAN_OBJ[] arrSendBuf = new VCI_CAN_OBJ[64];
        public static uint detectCONNECT()
        {
            TPCANStatus stsResult;
            uint iChannelsCount = 0;
            stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS_COUNT, out iChannelsCount, sizeof(uint));
            if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                return iChannelsCount;
            else
                return 0xff;//error
        }

        unsafe public void sendFame(UInt32 len, UInt32 startIndex = 0)
        {
            if (Form_SetCANParam.form_setcan.checkCANFDMode.Checked == false)//send can frame
            {
                TPCANStatus result1;
                TPCANMsg CANMsg;
                CANMsg = new TPCANMsg();
                CANMsg.DATA = new byte[8];
                CANMsg.MSGTYPE = 0x00;
                if (len > 48)
                    return;
                CANMsg.ID = arrSendBuf[startIndex].ID;
                CANMsg.LEN = arrSendBuf[startIndex].DataLen;
                CANMsg.MSGTYPE |= (arrSendBuf[startIndex].ExternFlag == 1) ? TPCANMessageType.PCAN_MESSAGE_EXTENDED : TPCANMessageType.PCAN_MESSAGE_STANDARD;
                if (arrSendBuf[startIndex].RemoteFlag == 1)
                    CANMsg.MSGTYPE |= TPCANMessageType.PCAN_MESSAGE_RTR;//0数据帧；1远程帧
                for (int i = 0; i < CANMsg.LEN; i++)
                { CANMsg.DATA[i] = arrSendBuf[startIndex].Data[i]; }

                result1 = PCANBasic.Write(PCAN_PARA1.PCANIndex, ref CANMsg);

                // int result = VCI_Transmit(0x04, 0x00, CANINDEX, ref arrSendBuf[startIndex], len);

                if (result1 < 0)
                    MessageBox.Show("发送失败");
            }
            else//send can fd frame
            {
                TPCANStatus result1;
                TPCANMsgFD CANMsg;
                CANMsg = new TPCANMsgFD();
                CANMsg.DATA = new byte[64];
                // CANMsg.MSGTYPE = 0x00;
                if (len > 48)
                    return;
                CANMsg.ID = arrSendBuf[startIndex].ID;
                CANMsg.DLC = arrSendBuf[startIndex].DataLen;
                CANMsg.MSGTYPE |= (arrSendBuf[startIndex].ExternFlag == 1) ? TPCANMessageType.PCAN_MESSAGE_EXTENDED : TPCANMessageType.PCAN_MESSAGE_STANDARD;
                CANMsg.MSGTYPE |= (Form_BasicFunction.form_basic.checkEnableCANFD.Checked) ? TPCANMessageType.PCAN_MESSAGE_FD : TPCANMessageType.PCAN_MESSAGE_STANDARD;
                CANMsg.MSGTYPE |= (Form_BasicFunction.form_basic.EnableCANFD_BRS.Checked) ? TPCANMessageType.PCAN_MESSAGE_BRS : TPCANMessageType.PCAN_MESSAGE_STANDARD;
                if (arrSendBuf[startIndex].RemoteFlag == 1)
                    CANMsg.MSGTYPE |= TPCANMessageType.PCAN_MESSAGE_RTR;//0数据帧；1远程帧
                for (int i = 0; i < CANMsg.DLC; i++)
                { CANMsg.DATA[i] = arrSendBuf[startIndex].Data[i]; }

                result1 = PCANBasic.WriteFD(PCAN_PARA1.PCANIndex, ref CANMsg);

                // int result = VCI_Transmit(0x04, 0x00, CANINDEX, ref arrSendBuf[startIndex], len);

                if (result1 < 0)
                    MessageBox.Show("发送失败");
            }
        }
        /*---------------------------------------------------------------------------------------------------*/

    }
}
