using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    public partial class Form_AidedAnalysis : Form
    {
        public USBCAN usbCAN;
        DISCRETEVARIABLES dsClass;
        DISPLAYCLASS dispalyClass;
        CONTINUOUSVARIABLE ctClass;

        public Form_AidedAnalysis(USBCAN can)
        {         
            InitializeComponent();

            dsClass = new DISCRETEVARIABLES();
            ctClass = new CONTINUOUSVARIABLE();
            usbCAN = new USBCAN(dsClass.saveData);
            dispalyClass = new DISPLAYCLASS(dataGridViewshow);

            usbCAN.BTR0 = can.BTR0;
            usbCAN.BTR1 = can.BTR1;
            usbCAN.CANIndex = can.CANIndex;

            dataGridViewfuzzy.Rows.Add();
        }

        
        private void buttonDSstep1_Click(object sender, EventArgs e)
        {
            groupBoxDSstep1.Enabled = false;

            int delay = 0, sUpper = 0;
            try
            {
                delay = Convert.ToInt32(textBoxdsStep1.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("收集时间输入错误");
                goto errorProcessing;
            }
            //
            try
            {
                sUpper = Convert.ToInt32(textBoxSILENCEUPPER.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("静默状态数量输入错误");
                goto errorProcessing;
            }
            dsClass.SlienceUpper = sUpper;
            //
            if (usbCAN.shutDevice() == false)
                goto errorProcessing;
            //关闭了再清除
            dataGridViewshow.Rows.Clear();

            //!!
            usbCAN.ProcessingFrame = dsClass.saveData;
            //
            dsClass.Step = 0;
            dsClass.clearCANset();
            if (usbCAN.startDevice() == false)
                goto errorProcessing;

            DateTime dtPrev = DateTime.Now;
            while ((DateTime.Now - dtPrev).TotalMilliseconds < delay * 1000)
            {
                buttonDSstep1.Text = ((delay * 1000 - (DateTime.Now - dtPrev).TotalMilliseconds) / 1000).ToString("f2") + "s";
                Application.DoEvents();
            }
            buttonDSstep1.Text = "开始收集";

            if (usbCAN.shutDevice() == false)
                goto errorProcessing;
            dsClass.toFilter();

            groupBoxDSstep2.Enabled = true;
            return;

        errorProcessing:
            groupBoxDSstep1.Enabled = true;
        }

        private void buttonDSstep2_Click(object sender, EventArgs e)
        {
            groupBoxDSstep2.Enabled = false;

            int delay = 0, aUpper = 0, alLower = 0;
            try
            {
                delay = Convert.ToInt32(textBoxdsStep2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("收集时间输入错误");
                goto errorProcessing;
            }
            //
            try
            {
                aUpper = Convert.ToInt32(textBoxACTIONUPPER.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("动作状态数量输入错误");
                goto errorProcessing;
            }
            dsClass.ActionUpper = aUpper;
            //
            try
            {
                alLower = Convert.ToInt32(textBoxALTERNATELOWER.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("动作状态数量输入错误");
                goto errorProcessing;
            }
            dsClass.AlternateLower = alLower;
            //
            if (usbCAN.shutDevice() == false)
                goto errorProcessing;

            usbCAN.ProcessingFrame = dsClass.saveData;
            //
            dsClass.Step = 1;
            dsClass.clearCANset();
            if (usbCAN.startDevice() == false)
                goto errorProcessing;

            DateTime dtPrev = DateTime.Now;
            while ((DateTime.Now - dtPrev).TotalMilliseconds < delay * 1000)
            {
                buttonDSstep2.Text = ((delay * 1000 - (DateTime.Now - dtPrev).TotalMilliseconds) / 1000).ToString("f2") + "s";
                Application.DoEvents();
            }
            buttonDSstep2.Text = "开始收集";

            if (usbCAN.shutDevice() == false)
                goto errorProcessing;

            dsClass.toFilter();

            dsAddID();
            dsResultShow();
            usbCAN.ProcessingFrame = dispalyClass.ProcessingFrame;
            if (usbCAN.startDevice() == false)
                goto errorProcessing;

            groupBoxDSstep1.Enabled = true;
            return;
            errorProcessing:
            groupBoxDSstep2.Enabled = true;
        }

        void dsAddID()
        {
            dispalyClass.clearID();
            dispalyClass.FilterEnabled = true;
            dispalyClass.dispalyMode = DISPLAYMODE.COUNTINGMODE;

            for (int i = 0; i < dsClass.discreteIDs.Count; i++)
                dispalyClass.insertID(dsClass.discreteIDs[i].obj.ID);
        }
        void dsResultShow()
        {
            dataGridViewshow.Rows.Clear();
            for (int i = 0; i < dsClass.discreteIDs.Count; i++)
            {           
                dataGridViewshow.Rows.Add();
                dataGridViewshow.Rows[i].Cells[1].Value = dsClass.discreteIDs[i].obj.ID.ToString("X2");
                dataGridViewshow.Rows[i].Cells[2].Value = dsClass.discreteIDs[i].obj.DataLen.ToString();
                for (int d = 3; d < 11; d++)
                    dataGridViewshow.Rows[i].Cells[d].Value = dsClass.discreteIDs[i].obj.Data[d - 3];

                if (dsClass.discreteIDs[i].obj.ExternFlag == 1)
                    dataGridViewshow.Rows[i].Cells[11].Value = "扩展帧";
                else
                    dataGridViewshow.Rows[i].Cells[11].Value = "标准帧";

                if (dsClass.discreteIDs[i].obj.RemoteFlag == 1)
                    dataGridViewshow.Rows[i].Cells[12].Value = "远程帧";
                else
                    dataGridViewshow.Rows[i].Cells[12].Value = "数据帧";

                for (int index = 0; index < dsClass.discreteIDs[i].idIndexs.Count; index++)
                {
                    int indexVal = dsClass.discreteIDs[i].idIndexs[index].index / 2;
                    dataGridViewshow.Rows[i].Cells[indexVal + 3].Style.BackColor = Color.Yellow;
                }

                dataGridViewshow.Rows[i].Cells[1].ToolTipText = "右击帧重放";
            }
        }

        private void Form_AidedAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            usbCAN.shutDevice();
        }

        private void 打开设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SetCANParam form = new Form_SetCANParam(usbCAN);
            form.ShowDialog();
        }

        private void dataGridViewshow_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_Replay form = new Form_Replay(usbCAN, dataGridViewshow.CurrentRow);
            form.ShowDialog();
        }

        private void buttonCTprev_Click(object sender, EventArgs e)
        {
            buttonFilter.Enabled = buttonEnd.Enabled = false;

            groupBoxCTprev.Enabled = false;
            int delay = 0;
            try
            {
                delay = Convert.ToInt32(textBoxCTprevDelay.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("收集时间输入错误");
                goto errorProcessing;
            }

            if (usbCAN.shutDevice() == false)
                goto errorProcessing;

            usbCAN.ProcessingFrame = ctClass.saveData;
            ctClass.clearCANset();
            if (usbCAN.startDevice() == false)
                goto errorProcessing;

            DateTime dtPrev = DateTime.Now;
            while ((DateTime.Now - dtPrev).TotalMilliseconds < delay * 1000)
            {
                buttonCTprev.Text = ((delay * 1000 - (DateTime.Now - dtPrev).TotalMilliseconds) / 1000).ToString("f2") + "s";
                Application.DoEvents();
            }
            buttonCTprev.Text = "开始收集";

            if (usbCAN.shutDevice() == false)
                goto errorProcessing;

            ctClass.countingStatus(0);

            groupBoxCTnext.Enabled = true;
            return;
        errorProcessing:
            groupBoxCTprev.Enabled = true;
        }

        private void buttonCTnext_Click(object sender, EventArgs e)
        {
            buttonFilter.Enabled = buttonEnd.Enabled = false;

            groupBoxCTnext.Enabled = false;
            int delay = 0;
            try
            {
                delay = Convert.ToInt32(textBoxCTnextDelay.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("收集时间输入错误");
                goto errorProcessing;
            }

            if (usbCAN.shutDevice() == false)
                goto errorProcessing;

            usbCAN.ProcessingFrame = ctClass.saveData;
            ctClass.clearCANset();
            if (usbCAN.startDevice() == false)
                goto errorProcessing;

            DateTime dtPrev = DateTime.Now;
            while ((DateTime.Now - dtPrev).TotalMilliseconds < delay * 1000)
            {
                buttonCTnext.Text = ((delay * 1000 - (DateTime.Now - dtPrev).TotalMilliseconds) / 1000).ToString("f2") + "s";
                Application.DoEvents();
            }
            buttonCTnext.Text = "开始收集";

            if (usbCAN.shutDevice() == false)
                goto errorProcessing;

            ctClass.countingStatus(1);

            buttonFilter.Enabled = true;
            return;
        errorProcessing:
            groupBoxCTnext.Enabled = true;
        }

        bool isFilterStarted = false;
        int prevNo = 0, nextNo = 1;
        int countNo = 2;
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            int endian = 0;
            if (radioButtonBigEndian.Checked)
                endian = 0;
            else if (radioButtonLittleEndian.Checked)
                endian = 1;
            else
            {
                MessageBox.Show("请指定数据端序");
                return;
            }
            ctClass.Endian = endian;

            double digit = 0;
            if (radioButtonDigit0_5.Checked)
                digit = 0.5;
            else if (radioButtonDigit1.Checked)
                digit = 1;
            else if (radioButtonDigit1_5.Checked)
                digit = 1.5;
            else if (radioButtonDigit2.Checked)
                digit = 2.0;
            else
            {
                MessageBox.Show("请选择数据长度");
                return;
            }
            ctClass.Digit = digit;

            int trend = 0;
            if (radioButtonInc.Checked)
                trend = 1;
            else if (radioButtonDeinc.Checked)
                trend = -1;
            else
            {
                MessageBox.Show("请指定状态的变化趋势");
                return;
            }
            ctClass.Trend = trend;

            if (isFilterStarted == false)
            {
                isFilterStarted = true;
                ctClass.countingIDs();
            }

            ctClass.toFilter();
            ctResultShow();
            if (ctClass.continousIDs.Count == 0)
                MessageBox.Show("没有满足要求的数据帧");

            Form_ChooseKeep form = new Form_ChooseKeep(prevNo, nextNo);
            form.ShowDialog();
            if (form.Result == prevNo)
            {
                groupBoxCTnext.Text = "物理量 No." + countNo.ToString().PadLeft(2, '0');
                nextNo = countNo;
                textBoxNextNote.Text = "";

                groupBoxCTnext.Enabled = true;
                countNo++;             
            }
            else if (form.Result == nextNo)
            {
                prevNo = nextNo;
                groupBoxCTprev.Text = "物理量 No." + nextNo.ToString().PadLeft(2, '0');
                groupBoxCTnext.Text = "物理量 No." + countNo.ToString().PadLeft(2, '0');
                nextNo = countNo;

                textBoxPrevNote.Text = textBoxNextNote.Text;
                textBoxNextNote.Text = "";
                groupBoxCTnext.Enabled = true;
                countNo++;

                ctClass.copyNextToPrev();
            }
            else if (form.Result == -1)
            {
                prevNo = countNo;
                groupBoxCTprev.Text = "物理量 No." + countNo.ToString().PadLeft(2, '0');
                countNo++;
                nextNo = countNo;
                groupBoxCTnext.Text = "物理量 No." + countNo.ToString().PadLeft(2, '0');
                countNo++;

                textBoxNextNote.Text = textBoxPrevNote.Text = "";
                groupBoxCTprev.Enabled = true;
            }

            buttonFilter.Enabled = false;
            buttonEnd.Enabled = true;
        }

        void ctAddID()
        {
            dispalyClass.clearID();
            dispalyClass.FilterEnabled = true;
            dispalyClass.dispalyMode = DISPLAYMODE.COUNTINGMODE;

            for (int i = 0; i < ctClass.continousIDs.Count; i++)
                dispalyClass.insertID(ctClass.continousIDs[i].obj.ID);
        }

        void ctRestart()
        {
            isFilterStarted = false;
            prevNo = 0;
            nextNo = 1;
            countNo = 2;

            groupBoxCTprev.Text = "物理量 No.00";
            groupBoxCTnext.Text = "物理量 No.01";

            textBoxPrevNote.Text = textBoxNextNote.Text = "";
            groupBoxCTprev.Enabled = true;
            groupBoxCTnext.Enabled = false;
            buttonFilter.Enabled = buttonEnd.Enabled = false;
        }
        private void buttonEnd_Click(object sender, EventArgs e)
        {
            ctAddID();
            if (usbCAN.shutDevice() == false)
                return;

            usbCAN.ProcessingFrame = dispalyClass.ProcessingFrame;

            if (usbCAN.startDevice() == false)
                return;

            ctRestart();
        }

        Thread threadFuzzy;
        class FUZZINGPARM
        {
            public Form_ProgressBar form;
            public CAN_OBJ obj = new CAN_OBJ();
            public int delay, rep;

            public byte index, min, max;

            public ManualResetEvent mrEvent;
        }

        void NullProc(ref VCI_CAN_OBJ canObj) { }
        private void buttonFuzzy_Click(object sender, EventArgs e)
        {           
            FUZZINGPARM parm = new FUZZINGPARM();
            
            try { parm.obj.ID = Convert.ToUInt32(dataGridViewfuzzy.Rows[0].Cells[0].Value.ToString(), 16); }
            catch (Exception) { MessageBox.Show("请填写正确的16进制ID"); return; }

            try { parm.obj.DataLen = Convert.ToByte(dataGridViewfuzzy.Rows[0].Cells[1].Value); }
            catch (Exception) { MessageBox.Show("请填写正确长度"); return; }

            for (int d = 0; d < 8; d++)
            {
                try { parm.obj.Data[d] = Convert.ToByte(dataGridViewfuzzy.Rows[0].Cells[2+d].Value.ToString(), 16); }
                catch (Exception) { MessageBox.Show("请填写正确的16进制DATA" + d.ToString() ); return; }
            }

            if (dataGridViewfuzzy.Rows[0].Cells[10].Value.ToString().Equals("标准帧"))
                parm.obj.ExternFlag = 0;
            else if (dataGridViewfuzzy.Rows[0].Cells[10].Value.ToString().Equals("扩展帧"))
                parm.obj.ExternFlag = 1;
            else
            {
                MessageBox.Show("请选择格式");
                return;
            }

            if (dataGridViewfuzzy.Rows[0].Cells[11].Value.ToString().Equals("数据帧"))
                parm.obj.RemoteFlag = 0;
            else if (dataGridViewfuzzy.Rows[0].Cells[11].Value.ToString().Equals("远程帧"))
                parm.obj.RemoteFlag = 1;
            else
            {
                MessageBox.Show("请选择类型");
                return;
            }

            try { parm.delay = Convert.ToInt32(dataGridViewfuzzy.Rows[0].Cells[12].Value); }
            catch (Exception) { MessageBox.Show("请输入正确的延时"); return; }

            if (comboBoxDataIndex.SelectedIndex == -1)
            {
                MessageBox.Show("请选择数据索引");
                return;
            }

            parm.index = (byte)comboBoxDataIndex.SelectedIndex;

            try { parm.min = Convert.ToByte(textBoxFuzzyMin.Text, 16); }
            catch (Exception) { MessageBox.Show("请输入正确的16进制下界范围"); return; }

            try { parm.max = Convert.ToByte(textBoxFuzzyMax.Text, 16); }
            catch (Exception) { MessageBox.Show("请输入正确的16进制上界范围"); return; }

            if (parm.min > parm.max)
            {
                byte tmp = parm.min;
                parm.min = parm.max;
                parm.max = tmp;
            }

            try { parm.rep = Convert.ToInt32(textBoxRepeat.Text); }
            catch (Exception) { MessageBox.Show("请输入正确的循环次数"); return; }

            if (usbCAN.shutDevice() == false)
                return;

            usbCAN.ProcessingFrame = NullProc;

            if (usbCAN.startDevice() == false)
                return;

            threadFuzzy = new Thread(new ParameterizedThreadStart(fuzzingThread));
            parm.mrEvent = new ManualResetEvent(false);
            parm.form = new Form_ProgressBar(parm.max - parm.min + 1, parm.mrEvent, threadFuzzy);
            
            threadFuzzy.Start(parm);

            parm.form.ShowDialog();

            if (parm.form.returnResult == DialogResult.Abort)
                MessageBox.Show("你终止了操作");

            threadFuzzy.Abort();
            threadFuzzy = null;
        }

        unsafe void fuzzingThread(object objParm)
        {
            FUZZINGPARM parm = (FUZZINGPARM)objParm;
            Form_ProgressBar form = parm.form;
            CAN_OBJ obj = parm.obj;
            int delay = parm.delay;
            byte index = parm.index;
            byte min = parm.min;
            byte max = parm.max;
            int rep = parm.rep;

            parm.mrEvent.WaitOne();

            usbCAN.arrSendBuf[0].ID = obj.ID;
            usbCAN.arrSendBuf[0].DataLen = obj.DataLen;
            usbCAN.arrSendBuf[0].ExternFlag = obj.ExternFlag;
            usbCAN.arrSendBuf[0].RemoteFlag = obj.RemoteFlag;
            for (int d = 0; d < 8; d++)
                usbCAN.arrSendBuf[0].Data[d] = obj.Data[d];

            for (int i = min; i <= max; i++)
            {
                for (int t = 0; t < rep; t++)
                {
                    usbCAN.arrSendBuf[0].Data[index] = (byte)i;
                    usbCAN.sendFame(1);
                    Thread.Sleep(delay);
                }

                form.addValue();               
            }

            form.returnResult = DialogResult.OK;
            form.Invoke(new Action(() => { form.Close(); }));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        void ctResultShow()
        {
            dataGridViewshow.Rows.Clear();
            for (int i = 0; i < ctClass.continousIDs.Count; i++)
            {
                dataGridViewshow.Rows.Add();
                dataGridViewshow.Rows[i].Cells[1].Value = ctClass.continousIDs[i].obj.ID.ToString("X2");
                dataGridViewshow.Rows[i].Cells[2].Value = ctClass.continousIDs[i].obj.DataLen.ToString();
                for (int d = 3; d < 11; d++)
                    dataGridViewshow.Rows[i].Cells[d].Value = ctClass.continousIDs[i].obj.Data[d - 3];

                if (ctClass.continousIDs[i].obj.ExternFlag == 1)
                    dataGridViewshow.Rows[i].Cells[11].Value = "扩展帧";
                else
                    dataGridViewshow.Rows[i].Cells[11].Value = "标准帧";

                if (ctClass.continousIDs[i].obj.RemoteFlag == 1)
                    dataGridViewshow.Rows[i].Cells[12].Value = "远程帧";
                else
                    dataGridViewshow.Rows[i].Cells[12].Value = "数据帧";

                for (int index = 0; index < ctClass.continousIDs[i].indexs.Count; index++)
                {
                    int indexVal = ctClass.continousIDs[i].indexs[index];
                    dataGridViewshow.Rows[i].Cells[indexVal + 3].Style.BackColor = Color.Yellow;
                }

                dataGridViewshow.Rows[i].Cells[1].ToolTipText = "右击帧重放";
            }
        }
    }
}
