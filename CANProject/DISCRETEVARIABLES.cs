using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANProject
{
    public class DISCRETEID
    {
        public CAN_OBJ obj = new CAN_OBJ();
        public List<DISCRETEIDINDEX> idIndexs = new List<DISCRETEIDINDEX>();

        public DISCRETEID(CAN_OBJ canObj)
        {
            obj.ID = canObj.ID;
            obj.DataLen = canObj.DataLen;
            obj.ExternFlag = canObj.ExternFlag;
            obj.RemoteFlag = canObj.RemoteFlag;

            idIndexs.Clear();
            for (int i = 0; i < obj.DataLen * 2; i++)
                idIndexs.Add(new DISCRETEIDINDEX(i));
        }
    }

    public class DISCRETEIDINDEX
    {
        public int index;
        public List<VALUECOUNT> silenceValueCount = new List<VALUECOUNT>();
        public List<VALUECOUNT> actionValueCount = new List<VALUECOUNT>();

        public DISCRETEIDINDEX(int i)
        {
            index = i;
            silenceValueCount.Clear();
            actionValueCount.Clear();
        }
    }

    public class VALUECOUNT
    {
        public byte value;
        public int count;

        public VALUECOUNT(byte val)
        {
            value = val;
            count = 0;
        }
    }

    public class DISCRETEVARIABLES
    {
        int SILENCEUPPER = 1;
        int ACTIONUPPER = 1;
        int ALTERNATELOWER = 10;
        //帧集合
        List<CAN_OBJ> canSet = new List<CAN_OBJ>();

        public List<DISCRETEID> discreteIDs = new List<DISCRETEID>();

        int step = 0;

        public int Step
        {
            set { step = value; }
            get { return step; }
        }

        public int SlienceUpper
        {
            set { SILENCEUPPER = value; }
        }

        public int ActionUpper
        {
            set { ACTIONUPPER = value; }
        }

        public int AlternateLower
        {
            set { ALTERNATELOWER = value; }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void clearCANset()
        {
            canSet.Clear();
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

        void countingID()
        {
            discreteIDs.Clear();
            for (int i = 0; i < canSet.Count; i++)
            {
                int idIndex = discreteIDs.FindIndex(x => x.obj.ID.Equals(canSet[i].ID) );
                if (idIndex == -1)
                    discreteIDs.Add(new DISCRETEID(canSet[i]) );
            }
        }
        /*---------------------------------------------------------------------------------------------------*/

        void ensureSilence()
        {
            byte[] splitArr = new byte[16];

            for (int i = 0; i < canSet.Count; i++)
            {
                DISCRETEID dcID = discreteIDs.Find(x => x.obj.ID.Equals(canSet[i].ID) );
                if (dcID == null)
                    continue;

                //拆分
                for (int s = 0; s < canSet[i].DataLen; s++)
                {
                    splitArr[2 * s] = (byte)(canSet[i].Data[s] >> 4);
                    splitArr[2 * s + 1] = (byte)(canSet[i].Data[s] & 0x0f);
                }
                //
                for (int index = 0; index < dcID.obj.DataLen * 2; index++)
                {
                    int dcIDindexIndex = dcID.idIndexs.FindIndex(x => x.index.Equals(index));
                    if (dcIDindexIndex == -1)
                        continue;

                    DISCRETEIDINDEX dcIDindex = dcID.idIndexs[dcIDindexIndex];
                    List<VALUECOUNT> silences = dcIDindex.silenceValueCount;


                    VALUECOUNT valcnt = silences.Find(x => x.value.Equals(splitArr[index]));
                    if (valcnt == null)
                    {
                        if (silences.Count < SILENCEUPPER)
                            silences.Add(new VALUECOUNT(splitArr[index]));
                        else
                            dcID.idIndexs.RemoveAt(dcIDindexIndex);
                    }
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void ensureAction()
        {
            byte[] splitArr = new byte[16];
            for (int i = 0; i < canSet.Count; i++)
            {
                DISCRETEID dcID = discreteIDs.Find(x => x.obj.ID.Equals(canSet[i].ID));
                if (dcID == null)
                    continue;

                //拆分
                for (int s = 0; s < canSet[i].DataLen; s++)
                {
                    splitArr[2 * s] = (byte)(canSet[i].Data[s] >> 4);
                    splitArr[2 * s + 1] = (byte)(canSet[i].Data[s] & 0x0f);
                }
                //

                for (int index = 0; index < dcID.obj.DataLen * 2; index++)
                {
                    int dcIDindexIndex = dcID.idIndexs.FindIndex(x => x.index.Equals(index));
                    if (dcIDindexIndex == -1)
                        continue;

                    DISCRETEIDINDEX dcIDindex = dcID.idIndexs[dcIDindexIndex];
                    List<VALUECOUNT> silences = dcIDindex.silenceValueCount;
                    List<VALUECOUNT> actions = dcIDindex.actionValueCount;

                    VALUECOUNT valcnt;
                    valcnt = silences.Find(x => x.value.Equals(splitArr[index]) );
                    if (valcnt != null)
                    {
                        valcnt.count++;
                    }
                    else
                    {
                        valcnt = actions.Find(x => x.value.Equals(splitArr[index]));
                        if (valcnt != null)
                            valcnt.count++;
                        else
                        {
                            if (actions.Count < ACTIONUPPER)
                                actions.Add(new VALUECOUNT(splitArr[index]) );
                            else
                                dcID.idIndexs.RemoveAt(dcIDindexIndex);
                        }
                    }
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void removeIndexListNone()
        {
            for (int i = 0; i < discreteIDs.Count;)
            {
                if (discreteIDs[i].idIndexs.Count == 0)
                    discreteIDs.RemoveAt(i);
                else
                    i++;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void removeActionListNone()
        {
            for (int i = 0; i < discreteIDs.Count; i++)
            {
                List<DISCRETEIDINDEX> dcIDIndexes = discreteIDs[i].idIndexs;
                for (int index = 0; index < dcIDIndexes.Count;)
                {
                    DISCRETEIDINDEX dcIndex = dcIDIndexes[index];
                    if (dcIndex.actionValueCount.Count == 0)
                        dcIDIndexes.RemoveAt(index);
                    else
                        index++;
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void removeAlternateError()
        {
            for (int i = 0; i < discreteIDs.Count; i++)
            {
                List<DISCRETEIDINDEX> dcIDIndexes = discreteIDs[i].idIndexs;
                for (int index = 0; index < dcIDIndexes.Count;)
                {
                    List<VALUECOUNT> silences = dcIDIndexes[index].silenceValueCount;
                    List<VALUECOUNT> actions = dcIDIndexes[index].actionValueCount;

                    int s;
                    for (s = 0; s < silences.Count && silences[s].count < ALTERNATELOWER; s++) ;

                    int a;
                    for (a = 0; a < actions.Count && actions[a].count < ALTERNATELOWER; a++) ;

                    if (s >= silences.Count || a >= actions.Count)
                        dcIDIndexes.RemoveAt(index);
                    else
                        index++;
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public void toFilter()
        {
            if (step == 0)
            {
                countingID();
                ensureSilence();
            }
            else
            {
                ensureAction();
                removeActionListNone();
                removeAlternateError();  
            }

            removeIndexListNone();
        }

    }
}
