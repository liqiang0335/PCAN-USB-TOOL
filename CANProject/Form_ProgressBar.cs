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
    public partial class Form_ProgressBar : Form
    {
        public DialogResult returnResult = DialogResult.Abort;
        delegate void AddValueFunc();
        AddValueFunc addValueFunc;
        ManualResetEvent mrEvent;
        Thread thread;
        public Form_ProgressBar(int max, ManualResetEvent mrEvent, Thread th)
        {
            InitializeComponent();

            progressBar1.Maximum = max;
            addValueFunc = new AddValueFunc(delAddValue);
            this.mrEvent = mrEvent;
            thread = th;
        }

        public void addValue()
        {
            progressBar1.Invoke(addValueFunc);
            
        }

        void delAddValue()
        {
            try
            {
                progressBar1.Value += progressBar1.Step;
            }
            catch (Exception)
            {
                progressBar1.Maximum += 10;
                progressBar1.Value += progressBar1.Step;
            }
        }

        private void Form_ProgressBar_Load(object sender, EventArgs e)
        {
            mrEvent.Set();
        }

        private void Form_ProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.ControlBox = false;
            thread.Abort();
            DateTime prev = DateTime.Now;
            while ((DateTime.Now - prev).TotalMilliseconds < 100)
                Application.DoEvents();
        }
    }
}
