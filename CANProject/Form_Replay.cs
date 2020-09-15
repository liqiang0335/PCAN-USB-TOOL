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
    public partial class Form_Replay : Form
    {
        USBCAN usbCAN;
        Thread thread = null;

        public Form_Replay(USBCAN can, DataGridViewRow row)
        {
            InitializeComponent();

            usbCAN = can;
            dataGridViewsend.Rows.Add();
            int i;
            for (i = 1; i < row.Cells.Count; i++)
                dataGridViewsend.Rows[0].Cells[i - 1].Value = row.Cells[i].Value;
            dataGridViewsend.Rows[0].Cells[i - 1].Value = "30";
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            fillSendBuf();
            if (checkBoxloop.Checked == false)
                usbCAN.sendFame(1);
            else
            {
                if (thread == null)
                {
                    thread = new Thread(sendingThread);
                    thread.Start();
                }
            }
        }
        /*---------------------------------------------------------------------------------------------------*/
        unsafe void fillSendBuf()
        {
            usbCAN.arrSendBuf[0].ID = Convert.ToUInt32(dataGridViewsend.Rows[0].Cells[0].Value.ToString(), 16);
            usbCAN.arrSendBuf[0].DataLen = Convert.ToByte(dataGridViewsend.Rows[0].Cells[1].Value.ToString() );
            for (int i = 2; i < 10; i++)
                usbCAN.arrSendBuf[0].Data[i - 2] = Convert.ToByte(dataGridViewsend.Rows[0].Cells[i].Value.ToString(), 16);
            if (dataGridViewsend.Rows[0].Cells[10].Value.ToString().Equals("扩展帧"))
                usbCAN.arrSendBuf[0].ExternFlag = 1;
            else
                usbCAN.arrSendBuf[0].ExternFlag = 0;
            if (dataGridViewsend.Rows[0].Cells[11].Value.ToString().Equals("远程帧"))
                usbCAN.arrSendBuf[0].RemoteFlag = 1;
            else
                usbCAN.arrSendBuf[0].RemoteFlag = 0;
            usbCAN.arrSendBuf[0].SendType = 0;
        }
        /*---------------------------------------------------------------------------------------------------*/
        void sendingThread()
        {
            int t = Convert.ToByte(dataGridViewsend.Rows[0].Cells[12].Value.ToString());
            while (true)
            {
                usbCAN.sendFame(1);
                Thread.Sleep(t);
            }
        }

        private void checkBoxloop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxloop.Checked == false)
            {
                if (thread != null)
                {
                    thread.Abort();
                    thread = null;
                }
            }
        }

        private void Form_Replay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

        private void dataGridViewsend_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /*---------------------------------------------------------------------------------------------------*/
    }
}
