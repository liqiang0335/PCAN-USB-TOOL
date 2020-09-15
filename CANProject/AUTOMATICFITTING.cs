using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANProject
{
    public class AUTOMATICFITTING
    {
        class IDCLASS
        {
            public uint id;
            public bool hasFoundFramePoint;
            public List<FRAMEPOINT> framePoints = new List<FRAMEPOINT>();

            public IDCLASS(uint ID)
            {
                id = ID;
            }
        }

        class FRAMEPOINT
        {
            public CAN_OBJ frameX;
            public double lfY;

            public FRAMEPOINT(CAN_OBJ x, double y)
            {
                frameX = x;
                lfY = y;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        List<IDCLASS> idClasses = new List<IDCLASS>();
        void countIdClass(List<CAN_OBJ> dataPacket)
        {
            idClasses.Clear();
            for (int i = 0; i < dataPacket.Count; i++)
            {
                int index = idClasses.FindIndex(x => x.id.Equals(dataPacket[i].ID) );
                if (index == -1)
                    idClasses.Add(new IDCLASS(dataPacket[i].ID) );
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        List<int> udsFrameIndex = new List<int>();
        void countUdsFrame(List<CAN_OBJ> dataPacket)
        {
            udsFrameIndex.Clear();
            udsFrameIndex.Add(0); //哨兵

            for (int index = 0; index < dataPacket.Count; index++)
            {
                if (dataPacket[index].ID >= 0x7E8)
                    udsFrameIndex.Add(index);
            }

            udsFrameIndex.Add(dataPacket.Count); //哨兵
        }
        /*---------------------------------------------------------------------------------------------------*/
        void setNotFoundFramePoint()
        {
            for (int i = 0; i < idClasses.Count; i++)
                idClasses[i].hasFoundFramePoint = false;
        }

        void countFramePoint(List<CAN_OBJ> dataPacket, UDSServiceFormat udsReq)
        {
            for (int i = 1; i < udsFrameIndex.Count -1; i++)
            {
                int prevIndex = udsFrameIndex[i - 1];
                int index = udsFrameIndex[i];
                int nextIndex = udsFrameIndex[i + 1];

                //检验UDS是否正确
                CAN_OBJ udsFrame = dataPacket[index];
                int valueIndex = 2, c;
                for (c = 0; c < udsReq.parameterList.Count; c++, valueIndex++)
                {
                    if (udsReq.parameterList[c] != udsFrame.Data[valueIndex])
                        break;
                }
                if (c < udsReq.parameterList.Count)
                    continue;

                //计算值
                float udsValue = 0;
                int cnt = udsFrame.Data[0] & 0x0f;
                for (int d = valueIndex; d <= cnt; d++)
                {
                    udsValue *= 256;
                    udsValue += udsFrame.Data[d];
                }

                int allIdCount;
                //找前面
                allIdCount = idClasses.Count;
                setNotFoundFramePoint();
                for (int p = index - 1; p > prevIndex && allIdCount > 0; p--)
                {
                    IDCLASS idClass = idClasses.Find(x => x.id.Equals(dataPacket[p].ID) );
                    if (idClass.hasFoundFramePoint == false)
                    {
                        idClass.hasFoundFramePoint = true;
                        allIdCount--;
                        idClass.framePoints.Add(new FRAMEPOINT(dataPacket[p], udsValue) );
                    }
                }

                //找后面
                allIdCount = idClasses.Count;
                setNotFoundFramePoint();
                for (int n = index + 1; n < nextIndex && allIdCount > 0; n++)
                {
                    IDCLASS idClass = idClasses.Find(x => x.id.Equals(dataPacket[n].ID));
                    if (idClass.hasFoundFramePoint == false)
                    {
                        idClass.hasFoundFramePoint = true;
                        allIdCount--;
                        idClass.framePoints.Add(new FRAMEPOINT(dataPacket[n], udsValue));
                    }
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public class DOUBLEPOINT
        {
            public double X, Y;
            public DOUBLEPOINT(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public class CALCULATIONRESULT
        {
            public uint id;
            public int startIndex;
            public double standardDeviation;
            public Coefficient coefficient;
            public List<DOUBLEPOINT> points;

            public CALCULATIONRESULT(uint ID, int StartIndex, double SD, Coefficient Coef, List<DOUBLEPOINT> Points)
            {
                id = ID;
                startIndex = StartIndex;
                standardDeviation = SD;
                coefficient = Coef;
                points = Points;
            }
        }

        double jointValue(CAN_OBJ obj, int start, int digit, int endian)
        {
            double val = 0;
            //大端序
            if (endian == 0)
            {
                for (int d = 0; d < digit; d++)
                {
                    val *= 256;
                    val += obj.Data[start + d];
                }
            }
            else
            {
                for (int d = 0; d < digit; d++)
                {
                    val *= 256;
                    val += obj.Data[start + digit - d - 1];
                }
            }

            return val;
        }

        List<CALCULATIONRESULT> calcResults = new List<CALCULATIONRESULT>();
        void Train(double digit, int endian)
        {
            calcResults.Clear();

            for (int idIndex = 0; idIndex < idClasses.Count; idIndex++)
            {
                IDCLASS idClass = idClasses[idIndex];
                if (idClass.framePoints.Count == 0)
                    continue;

                int dlc = idClass.framePoints[0].frameX.DataLen;
                for (int startIndex = 0; startIndex <= dlc - digit; startIndex++)
                {
                    List<DOUBLEPOINT> lfPoints = new List<DOUBLEPOINT>();

                    double lfX;
                    for (int f = 0; f < idClass.framePoints.Count; f++)
                    {
                        lfX = jointValue(idClass.framePoints[f].frameX, startIndex, Convert.ToInt32(Math.Floor(digit+0.6)), endian);
                        lfPoints.Add(new DOUBLEPOINT(lfX, idClass.framePoints[f].lfY) );
                    }

                    Coefficient coefficient = linearRegression(lfPoints);
                    double sd = calculateStandardDeviation(lfPoints, coefficient);
                    calcResults.Add(new CALCULATIONRESULT(idClass.id, startIndex, sd, coefficient, lfPoints) );
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public class Coefficient
        {
            public double w, b;
        }

        double calculateStandardDeviation(List<DOUBLEPOINT> points, Coefficient coefficient)
        {
            double lfStandardDeviation = 0;

            int i;
            for (i = 0; i < points.Count; i++)
            {
                try
                {
                    checked
                    {
                        double calcY = coefficient.w * points[i].X + coefficient.b;
                        lfStandardDeviation += (calcY - points[i].Y) * (calcY - points[i].Y);
                    }
                }
                catch(Exception)
                {
                    break;
                }
            }

            if (i < points.Count)
                lfStandardDeviation = double.MaxValue;
            else
            {
                lfStandardDeviation /= points.Count;
                lfStandardDeviation = Math.Sqrt(lfStandardDeviation);
            }

            return lfStandardDeviation;
        }

        Coefficient linearRegression(List<DOUBLEPOINT> points)
        {
            int i;
            Coefficient coefficient = new Coefficient();

            double sumX = 0, averX, tmp;
            for (i = 0; i < points.Count; i++)
                sumX += points[i].X;
            averX = sumX / points.Count;

            tmp = 0;
            for (i = 0; i < points.Count; i++)
                tmp += points[i].Y * (points[i].X - averX);
            coefficient.w = tmp;

            tmp = 0;
            for (i = 0; i < points.Count; i++)
                tmp += points[i].X * points[i].X;
            tmp -= (sumX * sumX) / points.Count;

            coefficient.w /= tmp;
            if (double.IsNaN(coefficient.w))
            {
                coefficient.w = double.MaxValue;
                coefficient.b = points[0].Y;
                return coefficient;
            }

            tmp = 0;
            for (i = 0; i < points.Count; i++)
                tmp += (points[i].Y - coefficient.w * points[i].X);
            coefficient.b = tmp / points.Count;

            return coefficient;
        }
        /*---------------------------------------------------------------------------------------------------*/
        void makeResultOrderly()
        {
            calcResults.Sort(
                delegate(CALCULATIONRESULT x, CALCULATIONRESULT y)
                {
                    double r = x.standardDeviation - y.standardDeviation;
                    if (r < 0)
                        return -1;
                    else if (r > 0)
                        return 1;
                    else
                        return 0;
                }
                );
        }
        /*---------------------------------------------------------------------------------------------------*/
        public List<CALCULATIONRESULT> toGetResult(List<CAN_OBJ> dataPacket, UDSServiceFormat udsReq, double digit, int endian)
        {
            countIdClass(dataPacket);
            countUdsFrame(dataPacket);
            countFramePoint(dataPacket, udsReq);
            Train(digit, endian);

            makeResultOrderly();
            return calcResults;
        }
        /*---------------------------------------------------------------------------------------------------*/
    }
}
