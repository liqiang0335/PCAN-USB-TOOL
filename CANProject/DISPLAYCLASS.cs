using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    public enum DISPLAYMODE
    {
        ROLLINGMODE = 0,
        COUNTINGMODE = 1
    }

    public class DISPLAYCLASS
    {
        bool filterIsturnOn = false;
        DISPLAYMODE mode = DISPLAYMODE.ROLLINGMODE; //显示模式
        List<uint> IDs = new List<uint>(); //ID过滤表
        DataGridView view;

        public DISPLAYCLASS(DataGridView dataGridview)
        {
            view = dataGridview;
            showFramedelegate = new ShowFrameDelegate(showFrame);
            
        }






        /*---------------------------------------------------------------------------------------------------*/
        public DISPLAYMODE dispalyMode
        {
            set { mode = value; }
        }
        /*---------------------------------------------------------------------------------------------------*/
        public bool FilterEnabled
        {
            set { filterIsturnOn = value; }
        }

        public void insertID(uint id)
        {
            int index = IDs.FindIndex(x => x.Equals(id) );
            if (index == -1)
                IDs.Add(id);
        }

        public void deleteID(uint id)
        {
            int index = IDs.FindIndex(x => x.Equals(id));
            if (index != -1)
                IDs.RemoveAt(index);
        }

        public void clearID()
        {
            IDs.Clear();
        }
        /*---------------------------------------------------------------------------------------------------*/
        public delegate void ShowFrameDelegate(ref VCI_CAN_OBJ canObj);
        ShowFrameDelegate showFramedelegate;


        public void ProcessingFrame(ref VCI_CAN_OBJ canObj)
        {
            view.BeginInvoke(showFramedelegate, canObj);
        }
        static UInt64 t1 = 0, t2 = 0;
        unsafe public void showFrame(ref VCI_CAN_OBJ canObj)
        {
            uint id = canObj.ID;
        
            if (filterIsturnOn)
            {
                if (IDs.FindIndex(x => x.Equals(id)) == -1)
                    return;
            }

            int viewCount = view.RowCount;
            

            if (mode == DISPLAYMODE.COUNTINGMODE)
            {
                int idIndex = IDs.FindIndex(x => x.Equals(id));
                if (idIndex == -1)
                {
                    idIndex = IDs.Count;
                    IDs.Add(id);
                }

                if (idIndex > viewCount - 1)
                {
                    view.Rows.Add();
                    t1 = canObj.TimeStamp;//t1保存上次时间戳值
                    view.Rows[viewCount].Cells[0].Value = 0+"ms";
                    //view.Rows[viewCount].Cells[0].Value = canObj.TimeStamp.ToString();                   
                    view.Rows[viewCount].Cells[1].Value = canObj.ID.ToString("X2");
                    view.Rows[viewCount].Cells[2].Value = canObj.DataLen.ToString();
                    for (int i = 3; i < 11; i++)
                        view.Rows[viewCount].Cells[i].Value = canObj.Data[i - 3].ToString("X2");

                    if (canObj.ExternFlag == 1)
                        view.Rows[viewCount].Cells[11].Value = "扩展帧";
                    else
                        view.Rows[viewCount].Cells[11].Value = "标准帧";
                    if (canObj.RemoteFlag == 1)
                        view.Rows[viewCount].Cells[12].Value = "远程帧";
                    else
                        view.Rows[viewCount].Cells[12].Value = "数据帧";

                    //idIndex 要更新为 viewCount
                    if (idIndex != viewCount)
                    {
                        if (viewCount < IDs.Count)
                        {
                            uint tmp = IDs[idIndex];
                            IDs[idIndex] = IDs[viewCount];
                            IDs[viewCount] = tmp;
                        }
                    }
                }
                else
                {                    
                    t2 = canObj.TimeStamp;
                    view.Rows[idIndex].Cells[0].Value = (t2 - t1)/1000+"."+ ((t2 - t1)%1000)/100 + "ms";
                    t1 = canObj.TimeStamp;
                    //view.Rows[idIndex].Cells[0].Value = (canObj.TimeStamp-Convert.ToUInt64(view.Rows[idIndex].Cells[0].Value))/1000;
                    view.Rows[idIndex].Cells[1].Value = canObj.ID.ToString("X2");
                    view.Rows[idIndex].Cells[2].Value = canObj.DataLen.ToString();
                    for (int i = 3; i < 11; i++)
                    {
                        string canData = canObj.Data[i-3].ToString("X2");
                        if (!view.Rows[idIndex].Cells[i].Value.Equals(canData))
                            view.Rows[idIndex].Cells[i].Value = canData;
                    }
                }
            }
            else
            {
                view.Rows.Add();
                view.Rows[viewCount].Cells[0].Value = (canObj.TimeStamp/1000).ToString()+"ms";
                view.Rows[viewCount].Cells[1].Value = canObj.ID.ToString("X2");
                view.Rows[viewCount].Cells[2].Value = canObj.DataLen.ToString();
                for (int i = 3; i < 11; i++)
                    view.Rows[viewCount].Cells[i].Value = canObj.Data[i - 3].ToString("X2");

                if (canObj.ExternFlag == 1)
                    view.Rows[viewCount].Cells[11].Value = "扩展帧";
                else
                    view.Rows[viewCount].Cells[11].Value = "标准帧";
                if (canObj.RemoteFlag == 1)
                    view.Rows[viewCount].Cells[12].Value = "远程帧";
                else
                    view.Rows[viewCount].Cells[12].Value = "数据帧";
            }
            this.view.MultiSelect = false;
            this.view.FirstDisplayedScrollingRowIndex = this.view.Rows.Count - 1;//滚动条置底
            if(viewCount > 5000)
                view.Rows.Clear();
        }
        /*---------------------------------------------------------------------------------------------------*/
        
    }
}
