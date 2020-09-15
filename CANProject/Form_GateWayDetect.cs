using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CANProject
{
    public partial class Form_GateWayDetect : Form
    {
        Dictionary<int, USBCAN> USBCAN_dictionary;
        GATEWAYDETECT GateWayDetect;
        public Form_GateWayDetect()
        {
            InitializeComponent();
            USBCAN_dictionary = new Dictionary<int, USBCAN>();
            GateWayDetect = new GATEWAYDETECT(ref USBCAN_dictionary, ref textBox1, ref progressBar1, textBox2, ref sankeyBrowser);
        }

        private void 设备参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_SetCANParam formParm = new Form_SetCANParam(ref USBCAN_dictionary);
            formParm.ShowDialog();
            for (var i =0; i<=7; i++)
            {
                try
                {
                    if (USBCAN_dictionary[i].startDevice())
                    {
                        textBox1.AppendText("设备" + i + "开启成功\r\n");
                    }else
                    {
                        textBox1.AppendText("设备" + i + "开启失败\r\n");
                    }
                    
                }
                catch
                {
                }
            }
        }

        private void 清空结果_Click(object sender, EventArgs e)
        {
            if(textBox1 != null)
            {
                textBox1.Text = "";
                sankeyBrowser.Navigate("about:blank");
            }else
            {
                MessageBox.Show("清空错误！");
            }
        }

        private void 开始检测_Click(object sender, EventArgs e)
        {
            //textBox1.AppendText("hello world!"+"\r\n");
            //Thread.Sleep(10000);
            //textBox1.AppendText("1" + "\r\n");
            //textBox1.GateWayDetect.Delegate.BeginInvoke(1, null, null);
            //textBox1.BeginInvoke(GateWayDetect.showDelegate, 1);
            //GateWayDetect.Excall();
            //Thread.Sleep(3000);
            //textBox1.AppendText("2" + "\r\n");
            //GateWayDetect.newthread();
            GateWayDetect.startdetect();

        }

        private void 停止检测_Click(object sender, EventArgs e)
        {
            GateWayDetect.stopdetect();
        }

        private void 关闭设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (var i = 0; i <= 7; i++)
            {
                try
                {
                    USBCAN_dictionary[i].shutDevice();
                }
                catch
                {

                }
            }
            MessageBox.Show("设备关闭成功");
        }

        private void Id样例_Click(object sender, EventArgs e)
        {
            MessageBox.Show("样例ID范围：\r\n100,2,[300-310],4,[500-510],600");
        }

        private void Form_GateWayDetect_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (var i = 0; i <= 7; i++)
            {
                try
                {
                    USBCAN_dictionary[i].shutDevice();
                }
                catch
                {
                }
            }
        }

        private void 测试用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sankeyBrowser.Navigate("https://ie.icoa.cn/");
            //GateWayDetect.webBrowsershow(Application.StartupPath + "\\" + "sankey.html" + "?nodes=" + "&links=");
            sankeyBrowser.Navigate(Application.StartupPath + "\\" + "sankey.html"+ "?nodes=[{name: 'a'},{name: 'b'},{name: 'a1'},{name:'a2'},{name: 'b1'},{name:'c'}]&links=[{source: 'a',target: 'a1',value: 5},{source:'a',target: 'a2',value: 3},{source: 'b',target: 'b1',value: 8},{source: 'a',target: 'b1',value: 3}, {source: 'b1',target: 'a1',value: 1}, {source: 'b1',target: 'c',value: 2}]");
        }
    }
}
