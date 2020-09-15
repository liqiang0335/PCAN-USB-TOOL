using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANProject
{
    public class CONTINUOUSID
    {
        public CAN_OBJ obj = new CAN_OBJ();
        public List<int> indexs = new List<int>();

        public CONTINUOUSID(CAN_OBJ canObj)
        {
            obj.ID = canObj.ID;
            obj.DataLen = canObj.DataLen;
            obj.ExternFlag = canObj.ExternFlag;
            obj.RemoteFlag = canObj.RemoteFlag;

            indexs.Clear();
            for (int i = 0; i < obj.DataLen; i++)
                indexs.Add(i);
        }

    }

    public class CONTINUOUSVARIABLE
    {
        //帧集合
        List<CAN_OBJ> canSet = new List<CAN_OBJ>();
        //状态
        List<CAN_OBJ> prevStatus = new List<CAN_OBJ>();
        List<CAN_OBJ> nextStatus = new List<CAN_OBJ>();

        public List<CONTINUOUSID> continousIDs = new List<CONTINUOUSID>();

        int ENDIAN = 0; //0大端序 1小端序
        double DIGIT = 1; //位数
        int TREND = 0; //趋势 1上升 -1下降
        /*---------------------------------------------------------------------------------------------------*/
        public int Endian
        {
            set { ENDIAN = value; }
        }

        public double Digit
        {
            set { DIGIT = value; }
        }

        public int Trend
        {
            set { TREND = value; }
        }
        /*---------------------------------------------------------------------------------------------------*/
        unsafe public void saveData(ref VCI_CAN_OBJ canObj)
        {
            CAN_OBJ obj = new CAN_OBJ();
            obj.ID = canObj.ID;
            obj.DataLen = canObj.DataLen;
            obj.ExternFlag = canObj.ExternFlag;
            obj.RemoteFlag = canObj.RemoteFlag;
            for (int i = 0; i < 8; i++)
                obj.Data[i] = canObj.Data[i];

            canSet.Add(obj);
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void countingStatus(int step)
        {
            List<CAN_OBJ> pList;

            if (step == 0)
                pList = prevStatus;
            else
                pList = nextStatus;

            pList.Clear();
            for (int i = canSet.Count-1; i >= 0; i--)
            {
                CAN_OBJ canObj = pList.Find(x => x.ID.Equals(canSet[i].ID));
                if (canObj == null)
                    pList.Add(new CAN_OBJ(canSet[i]));
            }

            /*******************************************************
            for (int i = 0; i < canSet.Count; i++)
            {
                CAN_OBJ canObj = pList.Find(x => x.ID.Equals(canSet[i].ID) );
                if (canObj == null)
                    pList.Add(new CAN_OBJ(canSet[i]));
                else
                {
                    for (int d = 0; d < 8; d++)
                        canObj.Data[d] = canSet[i].Data[d];
                }
            }
            *******************************************************/
        }
        /*---------------------------------------------------------------------------------------------------*/
            public void countingIDs()
        {
            continousIDs.Clear();
            for (int i = 0; i < prevStatus.Count; i++)
            {
                CONTINUOUSID ctID = continousIDs.Find(x => x.obj.ID.Equals(prevStatus[i].ID) );
                if (ctID == null)
                    continousIDs.Add(new CONTINUOUSID(prevStatus[i]) );
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void copyNextToPrev()
        {
            prevStatus.Clear();
            for (int i = 0; i < nextStatus.Count; i++)
                prevStatus.Add(new CAN_OBJ(nextStatus[i]) );
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void toFilter()
        {
            int nDigit = Convert.ToInt32(Math.Floor(DIGIT+0.6) );

            for (int i = 0; i < prevStatus.Count; i++)
            {
                CONTINUOUSID ctID = continousIDs.Find(x => x.obj.ID.Equals(prevStatus[i].ID) );
                if (ctID == null)
                    continue;

                CAN_OBJ prevObj = prevStatus[i];
                CAN_OBJ nextObj = nextStatus.Find(x => x.ID.Equals(prevObj.ID) );
                if (nextObj == null)
                    continue;

                int d;
                for (d = 0; d <= prevObj.DataLen - nDigit; d++)
                {
                    int idIndexIndex = ctID.indexs.FindIndex(x => x.Equals(d) );
                    if (idIndexIndex == -1)
                        continue;

                    Int64 prev = 0, next = 0;
                    prev = jointValue(prevObj, d, nDigit, ENDIAN);
                    next = jointValue(nextObj, d, nDigit, ENDIAN);

                    if (DIGIT == 1.5)
                    {
                        prev = prev & 0x0FFF;
                        next = next & 0x0FFF;
                    }

                    //不正确
                    if ((next-prev)*TREND <= 0)
                    {
                        ctID.indexs.RemoveAt(idIndexIndex);
                    }
                }

                //删除后续的索引!
                for (; d < prevObj.DataLen; d++)
                {
                    int idIndexIndex = ctID.indexs.FindIndex(x => x.Equals(d));
                    if (idIndexIndex == -1)
                        continue;
                    ctID.indexs.RemoveAt(idIndexIndex);
                }
            }

            //删除多余ID
            removeIndexListNone();
        }
        /*---------------------------------------------------------------------------------------------------*/
        void removeIndexListNone()
        {
            for (int i = 0; i < continousIDs.Count;)
            {
                if (continousIDs[i].indexs.Count == 0)
                    continousIDs.RemoveAt(i);
                else
                    i++;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        Int64 jointValue(CAN_OBJ obj, int start, int digit, int endian)
        {
            Int64 val = 0;
            //大端序
            if (endian == 0)
            {
                for (int d = 0; d < digit; d++)
                {
                    val <<= 8;
                    val += obj.Data[start + d];
                }
            }
            else
            {
                for (int d = 0; d < digit; d++)
                {
                    val <<= 8;
                    val += obj.Data[start + digit - d - 1];
                }
            }
            return val;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void clearCANset()
        {
            canSet.Clear();
        }
        /*---------------------------------------------------------------------------------------------------*/

    }
}
