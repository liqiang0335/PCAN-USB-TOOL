using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    public partial class Form_ChooseKeep : Form
    {
        public int Result = -2;
        int prev = -1, next = -1;

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            Result = prev;
            this.Close();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Result = next;
            this.Close();
        }

        private void buttonDelAll_Click(object sender, EventArgs e)
        {
            Result = -1;
            this.Close();
        }

        public Form_ChooseKeep(int prevNo, int nextNo)
        {
            InitializeComponent();

            buttonPrev.Text = " 保留No." + prevNo.ToString().PadLeft(2, '0');
            buttonNext.Text = "保留No" + nextNo.ToString().PadLeft(2, '0');

            prev = prevNo;
            next = nextNo;
        }


    }
}
