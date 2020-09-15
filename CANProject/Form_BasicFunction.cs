using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Peak.Can.Basic;
namespace CANProject
{
    public partial class Form_BasicFunction : Form
    {
        public static Form_BasicFunction form_basic;
        USBCAN usbCAN;
        DISPLAYCLASS displayClass;
        public Form_BasicFunction()
        {
            InitializeComponent();
            dataGridViewsend.Rows.Add();
            dataGridViewsend.Rows[0].Cells[0].Value = false;
            dataGridViewsend.Rows[0].Cells[11].Value = "标准帧";
            dataGridViewsend.Rows[0].Cells[12].Value = "数据帧";
            dataGridViewsend.Rows[0].Cells[13].Value = "0";
            dataGridViewsend.CellValueChanged += dataGridViewsend_CellValueChanged;

            dataGridViewfilter.Rows.Add();
            dataGridViewfilter.Rows[0].Cells[0].Value = false;
            dataGridViewfilter.CellValueChanged += dataGridViewfilter_CellValueChanged;

            displayClass = new DISPLAYCLASS(dataGridViewshow);
            usbCAN = new USBCAN(displayClass.ProcessingFrame);


            canMenu.usbCAN = usbCAN;
            CheckForIllegalCrossThreadCalls = false;
            form_basic = this;
        }

        private void 打开设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SetCANParam formParm = new Form_SetCANParam(usbCAN);
            formParm.ShowDialog();
            if (canMenu.startDevice())
                MessageBox.Show("设备打开成功");
        }

        private void 关闭设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (canMenu.shutDevice())
            {
                if(PCAN_COMMON_VAL_1.can_channel_1 == 0x0051)//当前通道1关闭
                    MessageBox.Show("通道1" + "设备关闭成功");
                else if (PCAN_COMMON_VAL_1.can_channel_1 == 0x0052)//当前通道2关闭
                    MessageBox.Show("通道2" + "设备关闭成功");

            }*/
        }

        private void 辅助解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usbCAN.shutDevice() == false)
                return;

            Form_AidedAnalysis form = new Form_AidedAnalysis(usbCAN);
            this.Hide();
            form.ShowDialog();

            usbCAN.BTR0 = form.usbCAN.BTR0;
            usbCAN.BTR1 = form.usbCAN.BTR1;
            usbCAN.CANIndex = form.usbCAN.CANIndex;

            this.Show();
        }

        private void Form_BasicFunction_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PCAN_COMMON_VAL.pcan_init_flag1 == 0x69 || PCAN_COMMON_VAL.pcan_init_flag2 == 0x69)//初始化成功
            {                                                // usbCAN.shutDevice();
                PCANBasic.Uninitialize(0x0051);
                PCANBasic.Uninitialize(0x0052);
                //MessageBox.Show("通道all设备关闭成功");
            }
        }

        private void oBD解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usbCAN.shutDevice() == false)
                return;

            Form_OBD form = new Form_OBD();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void 自动化解析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usbCAN.shutDevice() == false)
                return;

            Form_AutomatedAnalysis form = new Form_AutomatedAnalysis();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void dataGridViewsend_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dataGridViewsend.CurrentCell.ColumnIndex;
            if (columnIndex == 1 || (columnIndex >= 3 && columnIndex <= 10))
            {
                TextBox txtBoxHex = e.Control as TextBox;
                txtBoxHex.TextChanged -= TxtBoxHex_TextChanged;
                txtBoxHex.TextChanged += TxtBoxHex_TextChanged;
            }
            else if (columnIndex == 2 || columnIndex == 13)
            {
                TextBox txtBoxDec = e.Control as TextBox;
                txtBoxDec.TextChanged -= TxtBoxDec_TextChanged;
                txtBoxDec.TextChanged += TxtBoxDec_TextChanged;
            }
        }

        private void TxtBoxDec_TextChanged(object sender, EventArgs e)
        {
            string txt = (sender as TextBox).Text;
            int columnIndex = dataGridViewsend.CurrentCell.ColumnIndex;
            if (columnIndex == 2 || columnIndex == 13)
            {
                for (int len = 0; len < txt.Length;)
                {
                    if (txt[len] < '0' || txt[len] > '9')
                        txt = txt.Remove(len, 1);
                    else
                        len++;
                }

                 (sender as TextBox).Text = txt;
                (sender as TextBox).Select(txt.Length, 0);
            }
        }

        private void TxtBoxHex_TextChanged(object sender, EventArgs e)
        {
            string txt = (sender as TextBox).Text;

            for (int len = 0; len < txt.Length;)
            {
                if (isHexLetter(txt[len]) == false)
                    txt = txt.Remove(len, 1);
                else
                    len++;
            }

            (sender as TextBox).Text = txt;
            (sender as TextBox).Select(txt.Length, 0);
        }

        bool isHexLetter(char ch)
        {
            if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'f') || (ch >= 'A' && ch <= 'F'))
                return true;
            else
                return false;
        }

        private void dataGridViewsend_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewsend.IsCurrentCellDirty)
                dataGridViewsend.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewsend_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1)
            {
                if ((bool)dataGridViewsend.Rows[e.RowIndex].Cells[0].Value)
                {
                    if (checkSendFrame(e.RowIndex) == false)
                    {
                        dataGridViewsend.CellValueChanged -= dataGridViewsend_CellValueChanged;
                        dataGridViewsend.Rows[e.RowIndex].Cells[0].Value = false;
                        dataGridViewsend.CellValueChanged += dataGridViewsend_CellValueChanged;
                    }
                }
            }
        }

        bool checkSendFrame(int index)
        {
            uint id = 0;
            try { id = Convert.ToUInt32(dataGridViewsend.Rows[index].Cells[1].Value.ToString(), 16); }
            catch (Exception) { MessageBox.Show(string.Format("第{0}行ID输入错误", index + 1)); return false; }
            usbCAN.arrSendBuf[SENDCOUNT].ID = id;

            byte len = 0;
            try { len = Convert.ToByte(dataGridViewsend.Rows[index].Cells[2].Value.ToString()); }
            catch (Exception) { MessageBox.Show(string.Format("第{0}行长度输入错误", index + 1)); return false; }
            if (len > 8)
            {
                MessageBox.Show(string.Format("第{0}行长度大于8", index + 1)); return false;
            }
            usbCAN.arrSendBuf[SENDCOUNT].DataLen = len;

            byte canData;
            for (int d = 0; d < len; d++)
            {
                try { canData = Convert.ToByte(dataGridViewsend.Rows[index].Cells[d + 3].Value.ToString(), 16); }
                catch (Exception) { MessageBox.Show(string.Format("第{0}行FormClosingDATA{1}输入错误", index + 1, d)); return false; }
            }

            if (dataGridViewsend.Rows[index].Cells[11].Value.ToString().Equals("标准帧"))
                usbCAN.arrSendBuf[SENDCOUNT].ExternFlag = 0;
            else if (dataGridViewsend.Rows[index].Cells[11].Value.ToString().Equals("扩展帧"))
                usbCAN.arrSendBuf[SENDCOUNT].ExternFlag = 1;
            else
            {
                MessageBox.Show(string.Format("第{0}行帧格式不能为空", index + 1)); return false;
            }

            if (dataGridViewsend.Rows[index].Cells[12].Value.ToString().Equals("数据帧"))
                usbCAN.arrSendBuf[SENDCOUNT].RemoteFlag = 0;
            else if (dataGridViewsend.Rows[index].Cells[12].Value.ToString().Equals("远程帧"))
                usbCAN.arrSendBuf[SENDCOUNT].RemoteFlag = 1;
            else
            {
                MessageBox.Show(string.Format("第{0}行帧类型不能为空", index + 1)); return false;
            }

            uint delay = 0;
            try { delay = Convert.ToUInt32(dataGridViewsend.Rows[index].Cells[13].Value.ToString()); }
            catch (Exception) { MessageBox.Show(string.Format("第{0}行延时输入错误", index + 1)); return false; }
            usbCAN.arrSendBuf[SENDCOUNT].TimeStamp = delay;

            return true;
        }

        int SENDCOUNT;
        unsafe bool prepareFrame()
        {
            SENDCOUNT = 0;
            for (int index = 0; index < dataGridViewsend.Rows.Count; index++)
            {
                if ((bool)dataGridViewsend.Rows[index].Cells[0].Value)
                {
                    uint id = 0;
                    try { id = Convert.ToUInt32(dataGridViewsend.Rows[index].Cells[1].Value.ToString(), 16); }
                    catch (Exception) { MessageBox.Show(string.Format("第{0}行ID输入错误", index + 1)); return false; }
                    usbCAN.arrSendBuf[SENDCOUNT].ID = id;

                    byte len = 0;
                    try { len = Convert.ToByte(dataGridViewsend.Rows[index].Cells[2].Value.ToString()); }
                    catch (Exception) { MessageBox.Show(string.Format("第{0}行长度输入错误", index + 1)); return false; }
                    if (len > 8)
                    {
                        MessageBox.Show(string.Format("第{0}行长度大于8", index + 1)); return false;
                    }
                    usbCAN.arrSendBuf[SENDCOUNT].DataLen = len;

                    for (int d = 0; d < len; d++)
                    {
                        try { usbCAN.arrSendBuf[SENDCOUNT].Data[d] = Convert.ToByte(dataGridViewsend.Rows[index].Cells[d + 3].Value.ToString(), 16); }
                        catch (Exception) { MessageBox.Show(string.Format("第{0}行DATA{1}输入错误", index + 1, d)); return false; }
                    }

                    if (dataGridViewsend.Rows[index].Cells[11].Value.ToString().Equals("标准帧") )
                        usbCAN.arrSendBuf[SENDCOUNT].ExternFlag = 0;
                    else if (dataGridViewsend.Rows[index].Cells[11].Value.ToString().Equals("扩展帧"))
                        usbCAN.arrSendBuf[SENDCOUNT].ExternFlag = 1;
                    else
                    {
                        MessageBox.Show(string.Format("第{0}行帧格式不能为空", index + 1)); return false;
                    }

                    if (dataGridViewsend.Rows[index].Cells[12].Value.ToString().Equals("数据帧"))
                        usbCAN.arrSendBuf[SENDCOUNT].RemoteFlag = 0;
                    else if (dataGridViewsend.Rows[index].Cells[12].Value.ToString().Equals("远程帧"))
                        usbCAN.arrSendBuf[SENDCOUNT].RemoteFlag = 1;
                    else
                    {
                        MessageBox.Show(string.Format("第{0}行帧类型不能为空", index + 1)); return false;
                    }

                    uint delay = 0;
                    try { delay = Convert.ToUInt32(dataGridViewsend.Rows[index].Cells[13].Value.ToString()); }
                    catch (Exception) { MessageBox.Show(string.Format("第{0}行延时输入错误", index + 1)); return false; }
                    usbCAN.arrSendBuf[SENDCOUNT].TimeStamp = delay;

                    SENDCOUNT++;
                }
            }

            return true;
        }
        /*---------------------------------------------------------------------------------------------------*/
        public UInt64 tx_pkgcounts = 0;
        void sendThreading()
        {
            UInt64 delay;
            if (checkBoxRepeat.Checked == false)
            {
                for (int i = 0; i < SENDCOUNT; i++)
                {
                    usbCAN.sendFame(1, Convert.ToUInt32(i));
                    tx_pkgcounts++;
                    //TXCountsLabel.Text = "Tx:" + tx_pkgcounts;
                    Thread.Sleep(Convert.ToInt32(usbCAN.arrSendBuf[i].TimeStamp));
                }
            }
            else
            {
                while (true)
                {
                    for (int i = 0; i < SENDCOUNT; i++)
                    {
                        usbCAN.sendFame(1, Convert.ToUInt32(i));
                        tx_pkgcounts++;
                        for (delay = 0; delay < 200000; delay++)//确保约0.5ms间隔发包
                            ;
                        ///TXCountsLabel.Text = "Tx:" + tx_pkgcounts;//需要2ms
                        Thread.Sleep(Convert.ToInt32(usbCAN.arrSendBuf[i].TimeStamp));
                        
                    }
                }
            }

            buttonSend.Invoke(new Action(() => { buttonSend.Enabled = true; }));
        }

        Thread threadSend = null;
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (usbCAN.IsOpen == false)
            {
                if (usbCAN.startDevice() == false)
                    return;
            }

            prepareFrame();
            if (SENDCOUNT <= 0)
                return;
            if (SENDCOUNT > 48)
            {
                MessageBox.Show("此处发送尽量满足准确的时间间隔，为保证要求选中帧不能超过48个。\n"+
                    "如需大于48帧可构造重放文件进行发送");
                return;
            }

            buttonSend.Enabled = false;

            if (threadSend != null)
                threadSend.Abort();
            threadSend = null;
            threadSend = new Thread(sendThreading);
            threadSend.Start();
        }
        /*---------------------------------------------------------------------------------------------------*/
        private void buttonSendAdd_Click(object sender, EventArgs e)
        {
            int index = dataGridViewsend.Rows.Count;

            dataGridViewsend.Rows.Add();
            dataGridViewsend.Rows[index].Cells[0].Value = false;
            dataGridViewsend.Rows[index].Cells[11].Value = "标准帧";
            dataGridViewsend.Rows[index].Cells[12].Value = "数据帧";
            dataGridViewsend.Rows[index].Cells[13].Value = "0";
        }

        private void checkBoxRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRepeat.Checked == false)
            {
                if (threadSend != null)
                {
                    threadSend.Abort();
                    threadSend = null;
                }

                buttonSend.Enabled = true;
            }
        }

        private void buttonSendRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewsend.CurrentCell == null)
                return;

            int index = dataGridViewsend.CurrentCell.RowIndex;
            if (index != -1)
                dataGridViewsend.Rows.RemoveAt(index);

        }

        private void buttonSendUp_Click(object sender, EventArgs e)
        {
            if (dataGridViewsend.CurrentCell == null)
                return;

            dataGridViewsend.CellValueChanged -= dataGridViewsend_CellValueChanged;
            int index = dataGridViewsend.CurrentCell.RowIndex;
            if (index != -1 && index >= 1)
            {
                for (int c = 0; c < 14; c++)
                {
                    object tmp = dataGridViewsend.Rows[index].Cells[c].Value;
                    dataGridViewsend.Rows[index].Cells[c].Value = dataGridViewsend.Rows[index-1].Cells[c].Value;
                    dataGridViewsend.Rows[index-1].Cells[c].Value = tmp;
                }
            }
            dataGridViewsend.CellValueChanged += dataGridViewsend_CellValueChanged;
        }

        private void buttonSendDown_Click(object sender, EventArgs e)
        {
            if (dataGridViewsend.CurrentCell == null)
                return;

            dataGridViewsend.CellValueChanged -= dataGridViewsend_CellValueChanged;
            int index = dataGridViewsend.CurrentCell.RowIndex;
            if (index != -1 && index <= dataGridViewsend.Rows.Count-2)
            {
                object tmp;
                for (int c = 0; c < 14; c++)
                {
                    tmp = dataGridViewsend.Rows[index].Cells[c].Value;
                    dataGridViewsend.Rows[index].Cells[c].Value = dataGridViewsend.Rows[index+1].Cells[c].Value;
                    dataGridViewsend.Rows[index+1].Cells[c].Value = tmp;
                }
            }
            dataGridViewsend.CellValueChanged += dataGridViewsend_CellValueChanged;
        }

        private void checkBoxFilter_CheckedChanged(object sender, EventArgs e)
        {
            bool prevIsOpen = usbCAN.IsOpen;

            if (prevIsOpen)
            {
                if (usbCAN.shutDevice() == false)
                    return;
            }

            dataGridViewshow.Rows.Clear();
            if (checkBoxFilter.Checked)
            {
                dataGridViewfilter.Enabled = false;
                prepareID();
            }
            else
            {
                dataGridViewfilter.Enabled = true;
            }

            displayClass.FilterEnabled = checkBoxFilter.Checked;

            if (prevIsOpen)
            {
                if (usbCAN.startDevice() == false)
                    return;
            }
        }

        private void dataGridViewfilter_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dataGridViewfilter.CurrentCell.ColumnIndex;
            if (columnIndex == 1)
            {
                TextBox txtBoxHex = e.Control as TextBox;
                txtBoxHex.TextChanged -= TxtBoxHex_TextChanged;
                txtBoxHex.TextChanged += TxtBoxHex_TextChanged;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        bool checkID(int index)
        {
            uint id;
            try { id = Convert.ToUInt32(dataGridViewfilter.Rows[index].Cells[1].Value.ToString(), 16); }
            catch (Exception) { MessageBox.Show(string.Format("第{0}行ID输入错误", index + 1)); return false; }

            return true;
        }

        private void dataGridViewfilter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewfilter.IsCurrentCellDirty)
                dataGridViewfilter.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewfilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1)
            {
                if ((bool)dataGridViewfilter.Rows[e.RowIndex].Cells[0].Value)
                {
                    if (checkID(e.RowIndex) == false)
                    {
                        dataGridViewfilter.CellValueChanged -= dataGridViewfilter_CellValueChanged;
                        dataGridViewfilter.Rows[e.RowIndex].Cells[0].Value = false;
                        dataGridViewfilter.CellValueChanged += dataGridViewfilter_CellValueChanged;
                    }
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        void prepareID()
        {
            displayClass.clearID();
            for (int index = 0; index < dataGridViewfilter.Rows.Count; index++)
            {
                if ((bool)dataGridViewfilter.Rows[index].Cells[0].Value)
                {
                    uint id;
                    try { id = Convert.ToUInt32(dataGridViewfilter.Rows[index].Cells[1].Value.ToString(), 16); }
                    catch (Exception) { MessageBox.Show(string.Format("第{0}行ID输入错误", index + 1)); return; }

                    displayClass.insertID(id);
                }
            }
        }

        private void buttonIDAdd_Click(object sender, EventArgs e)
        {
            int index = dataGridViewfilter.Rows.Count;

            dataGridViewfilter.Rows.Add();
            dataGridViewfilter.Rows[index].Cells[0].Value = false;
        }

        private void buttonIDRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewfilter.CurrentCell == null)
                return;

            int index = dataGridViewfilter.CurrentCell.RowIndex;

            if (index != -1)
                dataGridViewfilter.Rows.RemoveAt(index);
        }

        private void radioButtonRollingMode_CheckedChanged(object sender, EventArgs e)
        {
            bool prevIsOpen = usbCAN.IsOpen;

            if (prevIsOpen)
            {
                if (usbCAN.shutDevice() == false)
                    return;
            }

            dataGridViewshow.Rows.Clear();
            if (radioButtonRollingMode.Checked)
                displayClass.dispalyMode = DISPLAYMODE.ROLLINGMODE;

            if (prevIsOpen)
            {
                if (usbCAN.startDevice() == false)
                    return;
            }

        }

        private void radioButtonCountingMode_CheckedChanged(object sender, EventArgs e)
        {
            bool prevIsOpen = usbCAN.IsOpen;

            if (prevIsOpen)
            {
                if (usbCAN.shutDevice() == false)
                    return;
            }

            dataGridViewshow.Rows.Clear();
            if (radioButtonCountingMode.Checked)
                displayClass.dispalyMode = DISPLAYMODE.COUNTINGMODE;

            if (prevIsOpen)
            {
                if (usbCAN.startDevice() == false)
                    return;
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        class SAVEPARAM
        {
            public StreamWriter sw;
            public Form_ProgressBar bar;
            public ManualResetEvent mrEvent;
        }

        void saveThreading(object saveParam)
        {
            SAVEPARAM param = (SAVEPARAM)saveParam;
            StreamWriter sw = param.sw;
            Form_ProgressBar form = param.bar;

            param.mrEvent.WaitOne();

            string line = "";
            for (int i = 0; i < dataGridViewshow.Rows.Count; i++)
            {
                line = "";
                for (int l = 0; l <= 12; l++)
                {
                    if (dataGridViewshow.Rows[i].Cells[l].Value != null)
                        line += dataGridViewshow.Rows[i].Cells[l].Value.ToString() + " ";
                }

                sw.WriteLine(line);
                form.addValue();
            }

            form.returnResult = DialogResult.OK;
            form.Invoke(new Action(() => { form.Close(); }));
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            save.FileName = "save-" + Convert.ToUInt64(ts.TotalSeconds).ToString();
            if (save.ShowDialog() != DialogResult.OK)
                return;

            bool prevIsOpen = usbCAN.IsOpen;

            if (prevIsOpen)
            {
                if (usbCAN.shutDevice() == false)
                    return;
            }

            Thread threadSave = new Thread(new ParameterizedThreadStart(saveThreading));
            string sPath = save.FileName;
            FileStream file = new FileStream(sPath, FileMode.Create);
            SAVEPARAM parm = new SAVEPARAM();          
            parm.sw = new StreamWriter(file);
            parm.mrEvent = new ManualResetEvent(false);

            parm.bar = new Form_ProgressBar(dataGridViewshow.Rows.Count, parm.mrEvent, threadSave);

            threadSave.Start(parm);
            parm.bar.ShowDialog();
            if (parm.bar.returnResult == DialogResult.Abort)
                MessageBox.Show("你终止了操作");
            else
                MessageBox.Show("保存成功");

            threadSave.Abort();
            threadSave = null;

            parm.sw.Close();
            file.Close();

            if (prevIsOpen)
            {
                if (usbCAN.startDevice() == false)
                    return;
            }
        }

        /*---------------------------------------------------------------------------------------------------*/
        class REPLAYPARAM
        {
            public StreamReader sr;
            public Form_ProgressBar bar;
            public ManualResetEvent mrEvent;
        }

        unsafe void replayThreading(object replayParm)
        {
            REPLAYPARAM parm = (REPLAYPARAM)replayParm;
            StreamReader sr = parm.sr;
            Form_ProgressBar form = parm.bar;
            parm.mrEvent.WaitOne();

            string line = null;
            line = sr.ReadLine();
            if (line == null)
                goto endProcess;

            Int64 timePrev, timeNow;
            string[] arr = line.Split(' ');
            try { usbCAN.arrSendBuf[0].ID = Convert.ToUInt32(arr[1], 16); }
            catch (Exception) { goto wrongFormat; }

            try { usbCAN.arrSendBuf[0].DataLen = Convert.ToByte(arr[2]); }
            catch (Exception) { goto wrongFormat; }

            try
            {
                for (int d = 0; d < 8; d++)
                    usbCAN.arrSendBuf[0].Data[d] = Convert.ToByte(arr[d + 3], 16);
            }
            catch (Exception) { goto wrongFormat; }

            if (arr[11].Equals("扩展帧"))
                usbCAN.arrSendBuf[0].ExternFlag = 1;
            else if (arr[11].Equals("标准帧"))
                usbCAN.arrSendBuf[0].ExternFlag = 0;
            else
                goto wrongFormat;

            if (arr[12].Equals("远程帧"))
                usbCAN.arrSendBuf[0].RemoteFlag = 1;
            else if (arr[12].Equals("数据帧"))
                usbCAN.arrSendBuf[0].RemoteFlag = 0;
            else
                goto wrongFormat;

            try { timePrev = Convert.ToInt64(arr[0]); }
            catch (Exception) { goto wrongFormat; }

            usbCAN.sendFame(1);
            form.addValue();

            while ((line = sr.ReadLine()) != null)
            {
                arr = line.Split(' ');
                try { timeNow = Convert.ToInt64(arr[0]); }
                catch (Exception) { goto wrongFormat; }

                try { usbCAN.arrSendBuf[0].ID = Convert.ToUInt32(arr[1], 16); }
                catch (Exception) { goto wrongFormat; }


                try { usbCAN.arrSendBuf[0].DataLen = Convert.ToByte(arr[2]); }
                catch (Exception) { goto wrongFormat; }

                try
                {
                    for (int d = 0; d < 8; d++)
                        usbCAN.arrSendBuf[0].Data[d] = Convert.ToByte(arr[d + 3], 16);
                }
                catch (Exception) { goto wrongFormat; }

                if (arr[11].Equals("扩展帧"))
                    usbCAN.arrSendBuf[0].ExternFlag = 1;
                else if (arr[11].Equals("标准帧"))
                    usbCAN.arrSendBuf[0].ExternFlag = 0;
                else
                    goto wrongFormat;

                if (arr[12].Equals("远程帧"))
                    usbCAN.arrSendBuf[0].RemoteFlag = 1;
                else if (arr[12].Equals("数据帧"))
                    usbCAN.arrSendBuf[0].RemoteFlag = 0;
                else
                    goto wrongFormat;

                Int64 delay = timeNow - timePrev;
                if (delay > 10)
                    Thread.Sleep(Convert.ToInt32(delay * 0.1));

                timePrev = timeNow;
                usbCAN.sendFame(1);
                form.addValue();
            }

            endProcess:
            form.returnResult = DialogResult.OK;
            form.Invoke(new Action(() => { form.Close(); }));
            return;

            wrongFormat:
            form.returnResult = DialogResult.No;
            form.Invoke(new Action(() => { form.Close(); }));
        }

        private void buttonReplay_Click(object sender, EventArgs e)
        {
            if (usbCAN.startDevice() == false)
                return;

            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() != DialogResult.OK)
                return;

            REPLAYPARAM param = new REPLAYPARAM();

            param.sr = new StreamReader(open.FileName);
            Thread threadReplay = new Thread(new ParameterizedThreadStart(replayThreading) );
            int lines = 0;
            while (param.sr.ReadLine() != null)
                lines++;
            param.mrEvent = new ManualResetEvent(false);

            param.bar = new Form_ProgressBar(lines, param.mrEvent, threadReplay);

            param.sr.BaseStream.Seek(0, SeekOrigin.Begin);
            threadReplay.Start(param);
            param.bar.ShowDialog();
            if (param.bar.returnResult == DialogResult.Abort)
                MessageBox.Show("你终止了操作");
            else if (param.bar.returnResult == DialogResult.OK)
                MessageBox.Show("重放结束");
            else
                MessageBox.Show("重放文件格式出错");

            threadReplay.Abort();
            threadReplay = null;

            param.sr.Close();
        }

    private void buttonClear_Click(object sender, EventArgs e)
        {
            bool prevIsOpen = usbCAN.IsOpen;
            usbCAN.rx_pkg_counts = 0;
            tx_pkgcounts = 0;
            //TXCountsLabel.Text = "Tx:0";
            //RXCountsLabel.Text = "Rx:0";
            if (prevIsOpen)
            {
                if (usbCAN.shutDevice() == false)
                    return;
            }

            dataGridViewshow.Rows.Clear();

            if (prevIsOpen)
            {
                if (usbCAN.startDevice() == false)
                    return;
            }
        }

        private void Form_BasicFunction_Load(object sender, EventArgs e)
        {

        }

        private void canMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridViewsend_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewshow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void 设备操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 通道1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // PCAN_COMMON_VAL.can_channel_buf = usbCAN.PCAN_PARA1.PCANIndex;//保存当前有效通道ID
            //usbCAN.PCAN_PARA1.PCANIndex = 0x0051;
            if (canMenu.shutDevice())                            
                MessageBox.Show("当前设备关闭成功");
            //usbCAN.PCAN_PARA1.PCANIndex = PCAN_COMMON_VAL.can_channel_buf;//回复当前有效通道ID
        }

 

        private void 通道allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PCAN_COMMON_VAL.pcan_init_flag0 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag0 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0051;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag1 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag1 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0052;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag2 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag2 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0053;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag3 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag3 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0054;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag4 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag4 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0055;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag5 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag5 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0056;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag6 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag6 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0057;
                canMenu.shutDevice();
            }
            if (PCAN_COMMON_VAL.pcan_init_flag7 == 0x69)
            {
                PCAN_COMMON_VAL.pcan_init_flag7 = 0xff;
                usbCAN.PCAN_PARA1.PCANIndex = 0x0058;
                canMenu.shutDevice();
            }
            MessageBox.Show("所有设备关闭成功");
        }
        private void EnableCANFD_BRS_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableCANFD_BRS.Checked == true)
                usbCAN.PCAN_PARA1.canfd_brs_en = true;//选择FD-BRS通信
            else
                usbCAN.PCAN_PARA1.canfd_brs_en = false;//选择FD-BRS通信
        }
        private void checkEnableCANFD_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkEnableCANFD.Checked == true)
            {
                EnableCANFD_BRS.Enabled = true;//CANFD BRS帧复选框使能
                usbCAN.PCAN_PARA1.canfd_en = true;//选择FD通信
            }
            else
            {
                EnableCANFD_BRS.Enabled = false;//CANFD BRS帧复选禁止
                usbCAN.PCAN_PARA1.canfd_en = false;//禁止FD功能
            }

        }

        private void dataGridViewshow_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 网关检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_GateWayDetect form = new Form_GateWayDetect();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }







        /*---------------------------------------------------------------------------------------------------*/
    }
}
