using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    public class UDSServiceFormat
    {
        public uint address;
        public byte format;
        public byte serviceID;
        public List<byte> parameterList = new List<byte>();
        public byte expectedFrame;
        public string description;
    }

    public class UDSResponseList
    {
        public uint respAddress;
        public int remainingReceivingLength;
        public List<byte> responseList = new List<byte>();
        public int expectedIndex; //用于多帧
        public ERRORCODE errorCode;
        public enum ERRORCODE{
            SUCCEED = 0,
            EXPECTEDINDEX,
            SINGLETIMEOUT,
            FIRSTTIMEOUT,
            MULTITIMEOUT
        };
    }

    public class UDSBASEDCAN
    {
        USBCAN usbCAN;

        //帧类型
        const byte SINGLEFRAME = 0; //单帧
        const byte FIRSTFRAME = 1; //首帧
        const byte CONSECUTIVEFRAME = 2; //连续帧

        //期待的服务
        bool expectedReturnIsSingleFrame = false;
        bool expectedReturnIsFirstFrame = false;
        bool expectedReturnIsConsecutiveFrame = false;

        UDSResponseList responseListForSingleFrameExpected = new UDSResponseList();
        UDSResponseList responseListForMultiFrameExpected = new UDSResponseList();

        UDSServiceFormat udsCaller = new UDSServiceFormat();

        int DELAYTIME = 1200;

        public UDSBASEDCAN(USBCAN can)
        {
            usbCAN = can;
        }
        /*---------------------------------------------------------------------------------------------------*/
        unsafe public void udsProcess(ref VCI_CAN_OBJ canObj)
        {
            int size, d;
            byte tpType = (byte)(canObj.Data[0] >> 4);
            if (tpType == FIRSTFRAME) //首帧
            {
                if (expectedReturnIsFirstFrame == false)
                    return;
                
                //回应流量控制帧
                usbCAN.arrSendBuf[0].ID = udsCaller.address;
                usbCAN.arrSendBuf[0].ExternFlag = udsCaller.format;
                usbCAN.arrSendBuf[0].RemoteFlag = 0;
                usbCAN.arrSendBuf[0].SendType = 1;

                usbCAN.arrSendBuf[0].DataLen = 3;
                usbCAN.arrSendBuf[0].Data[0] = 0x30;
                usbCAN.arrSendBuf[0].Data[1] = 0x00;
                usbCAN.arrSendBuf[0].Data[2] = 0x7f;
                usbCAN.sendFame(1);

                //
                responseListForMultiFrameExpected.respAddress = canObj.ID;
                size = canObj.Data[0] & 0x0f;
                size = (size << 8) + canObj.Data[1];

                for (d = 2; d < 8; d++)
                    responseListForMultiFrameExpected.responseList.Add(canObj.Data[d]);
                responseListForMultiFrameExpected.remainingReceivingLength = size - 6;

                responseListForMultiFrameExpected.expectedIndex = 1;
                expectedReturnIsFirstFrame = false;
                expectedReturnIsConsecutiveFrame = true;
            }
            else if (tpType == CONSECUTIVEFRAME)
            {
                if (expectedReturnIsConsecutiveFrame == false)
                    return;

                int seq = canObj.Data[0] & 0x0f;
                if (seq != responseListForMultiFrameExpected.expectedIndex)
                {
                    responseListForMultiFrameExpected.errorCode = UDSResponseList.ERRORCODE.EXPECTEDINDEX;
                    return;
                }

                size = responseListForMultiFrameExpected.remainingReceivingLength;
                for (d = 1; d < 8 && size > 0; d++, size--)
                    responseListForMultiFrameExpected.responseList.Add(canObj.Data[d]);

                responseListForMultiFrameExpected.remainingReceivingLength = size;
                if (size == 0)
                    expectedReturnIsConsecutiveFrame = false;
                else
                    responseListForMultiFrameExpected.expectedIndex = (responseListForMultiFrameExpected.expectedIndex + 1) % 16;
            }
            else if (tpType == SINGLEFRAME)
            {
                if (expectedReturnIsSingleFrame == false)
                    return;

                responseListForSingleFrameExpected.respAddress = canObj.ID;

                size = canObj.Data[0] & 0x0f;
                for (d = 1; size > 0 && d < 8; size--, d++)
                    responseListForSingleFrameExpected.responseList.Add(canObj.Data[d]);

                expectedReturnIsSingleFrame = false;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        unsafe public bool createService(UDSServiceFormat udsservice)
        {
            udsCaller.address = udsservice.address;
            udsCaller.format = udsservice.format;

            //超过8字节
            if (udsservice.parameterList.Count >= 7)
            {
                MessageBox.Show("参数超过7字节");
                return false;
            }

            usbCAN.arrSendBuf[0].ID = udsservice.address;
            usbCAN.arrSendBuf[0].ExternFlag = udsservice.format;
            usbCAN.arrSendBuf[0].RemoteFlag = 0;
            usbCAN.arrSendBuf[0].SendType = 1;

            usbCAN.arrSendBuf[0].DataLen = 8;
            usbCAN.arrSendBuf[0].Data[0] = (byte)(udsservice.parameterList.Count + 1);
            usbCAN.arrSendBuf[0].Data[1] = udsservice.serviceID;
            for (int d = 0; d < udsservice.parameterList.Count; d++)
                usbCAN.arrSendBuf[0].Data[d + 2] = udsservice.parameterList[d];

            return true;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public UDSResponseList getResponseList(UDSServiceFormat udsservice)
        {
            DateTime prev;
            createService(udsservice);

            if (udsservice.expectedFrame == SINGLEFRAME)
            {
                responseListForSingleFrameExpected.responseList.Clear();

                expectedReturnIsSingleFrame = true;
                usbCAN.sendFame(1);

                prev = DateTime.Now;
                while (expectedReturnIsSingleFrame)
                {
                    if ( (DateTime.Now - prev).TotalMilliseconds > DELAYTIME)
                    {
                        responseListForSingleFrameExpected.errorCode = UDSResponseList.ERRORCODE.SINGLETIMEOUT; //单帧超时
                        break;
                    }

                    Application.DoEvents();
                }

                expectedReturnIsFirstFrame = false;
                return responseListForSingleFrameExpected;
            }
            else
            {
                responseListForMultiFrameExpected.responseList.Clear();

                expectedReturnIsFirstFrame = true;
                usbCAN.sendFame(1);

                prev = DateTime.Now;
                while (expectedReturnIsFirstFrame)
                {
                    if ((DateTime.Now - prev).TotalMilliseconds > DELAYTIME)
                    {
                        responseListForMultiFrameExpected.errorCode = UDSResponseList.ERRORCODE.FIRSTTIMEOUT;
                        break;
                    }
                    Application.DoEvents();
                }

                if (udsservice.address == 0x7DF || responseListForMultiFrameExpected.errorCode == UDSResponseList.ERRORCODE.FIRSTTIMEOUT)
                {
                    expectedReturnIsFirstFrame = expectedReturnIsConsecutiveFrame = false;
                    return responseListForMultiFrameExpected;
                }

                while (expectedReturnIsConsecutiveFrame)
                {
                    if ((DateTime.Now - prev).TotalMilliseconds > DELAYTIME)
                    {
                        responseListForMultiFrameExpected.errorCode = UDSResponseList.ERRORCODE.MULTITIMEOUT;
                        break;
                    }
                    Application.DoEvents();
                }

                expectedReturnIsConsecutiveFrame = false;
                return responseListForMultiFrameExpected;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
    }
}
