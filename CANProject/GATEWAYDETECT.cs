using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CANProject
{
    class GATEWAYDETECT
    {
        Dictionary<int, USBCAN> USBCAN_dictionary;
        TextBox text;
        TextBox txid;
        ProgressBar ratebar;
        WebBrowser sankeyBrowser;
        Thread detectThread ;
        //string Result = "Detect Mission do not be performed!";

        public GATEWAYDETECT(ref Dictionary<int, USBCAN> usbcan_dictionary, ref TextBox textbox, ref ProgressBar progressbar, TextBox TXID, ref WebBrowser sankeybrowser)
        {
            USBCAN_dictionary = usbcan_dictionary;
            text = textbox;
            txid = TXID;
            ratebar = progressbar;
            sankeyBrowser = sankeybrowser;
        }
        
        public List<uint> TxIDlistReverse(string txID)
        {
            List<uint> txIDlist = new List<uint>();
            if (txID == "")
            {
                for (uint i = 0; i <= 0x7ff; i++)
                {
                    txIDlist.Add(i);
                }
                return txIDlist;
            }

            List<string> IDlist1 = new List<string>(txID.Split(','));

            foreach (string member in IDlist1)
            {
                try
                {
                    if (Regex.Match(member, "\\[.*-.*\\]").Value == "")
                    {
                        txIDlist.Add(Convert.ToUInt32(member, 16));
                    }else
                    {
                        uint min = Convert.ToUInt32(member.Substring(member.IndexOf('[') + 1, member.IndexOf('-') - member.IndexOf('[') - 1), 16);
                        uint max = Convert.ToUInt32(member.Substring(member.IndexOf('-') + 1, member.IndexOf(']') - member.IndexOf('-') - 1), 16);
                        for (uint i = min; i <= max; i++)
                        {
                            txIDlist.Add(i);
                        }
                    }

                }catch
                {
                    MessageBox.Show("ID列表输入有误");
                }
                
            }
            if (txIDlist.TrueForAll(item => item <= 0x7ff))
            {
                return txIDlist;
            }
            MessageBox.Show("ID列表输入有误");
            return txIDlist = new List<uint>();
        }


        unsafe public void detectmission()
        {
            //Thread.Sleep(5000);
            showResultAction("\r\n开启检测任务\r\n");
            List<USBCAN> USBCANlist = new List<USBCAN>();
            byte[] datarray = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            List<uint> txIDlist = TxIDlistReverse(txid.Text);
            string nodes = "[]";
            string links = "[]";
            if (txIDlist.Count == 0 || txIDlist.TrueForAll(item => item == 0))
            {
                MessageBox.Show("检测发送列表为空");
                return;
            }
            List<uint> rxIDlist = new List<uint>();
            //string result;
            //string compareResult;
            int steps = 0;
            int rate = 0;

            showBarAction(rate);

            for (var i = 0; i <= 7; i++)
            {
                if (USBCAN_dictionary.ContainsKey(i))
                {
                    if (USBCAN_dictionary[i].IsOpen)
                    {
                        USBCANlist.Add(USBCAN_dictionary[i]);
                    }
                }
            }

            if (USBCANlist.Count < 2)
            {
                showResultAction("当前已开启设备数过少!");
                return;
            }
            steps = USBCANlist.Count * txIDlist.Count;

            foreach (USBCAN usbcan_tx in USBCANlist)
            {
                showResultAction("\r\n正在检测设备" + (usbcan_tx.PCAN_PARA1.PCANIndex - 0x51) + "主动发送情况\r\n");

                for (var i = 0; i <= 7; i++)
                {
                    usbcan_tx.arrSendBuf[0].Data[i] = datarray[i];
                }
                usbcan_tx.arrSendBuf[0].DataLen = 8;
                usbcan_tx.arrSendBuf[0].RemoteFlag = 0;
                usbcan_tx.arrSendBuf[0].ExternFlag = 0;
                foreach (uint id in txIDlist)
                {
                    usbcan_tx.arrSendBuf[0].ID = id;
                    usbcan_tx.sendFame(1, 0);
                    Thread.Sleep(5);
                    rate++;
                    showBarAction(rate / steps);
                }
                usbcan_tx.arrSendBuf[0] = new VCI_CAN_OBJ();
                Thread.Sleep(1000);
                foreach (USBCAN usbcan_rx in USBCANlist)
                {
                    if (usbcan_tx.PCAN_PARA1.PCANIndex != usbcan_rx.PCAN_PARA1.PCANIndex)
                    {
                        foreach (VCI_CAN_OBJ rxCANOBJ in usbcan_rx.rxCANOBJLIST)
                        {
                            for (var i = 0; i <= 7; i++)
                            {
                                if (rxCANOBJ.Data[i] != datarray[i])
                                {
                                    goto jumpout;
                                }
                            }
                            rxIDlist.Add(rxCANOBJ.ID);
                        jumpout:
                            continue;
                        }

                        links = links.Insert(links.IndexOf("]"), "{ source: '设备" + (usbcan_tx.PCAN_PARA1.PCANIndex - 0x51) + "-t',target: '设备" + (usbcan_rx.PCAN_PARA1.PCANIndex - 0x51) + "-r',value: " + rxIDlist.Count + "},");
                        showResultAction("设备" + (usbcan_rx.PCAN_PARA1.PCANIndex - 0x51) +"未接收到的ID包括：" + string.Join("、", txIDlist.Except(rxIDlist).ToArray())+"\r\n");
                        usbcan_rx.rxCANOBJLIST.Clear();
                        rxIDlist.Clear();
                    }
                //Thread.Sleep(100);
                }

            }
            foreach (USBCAN usbcan_tx in USBCANlist)
            {
                nodes = nodes.Insert(nodes.IndexOf("]"), "{name: '设备" + (usbcan_tx.PCAN_PARA1.PCANIndex - 0x51) + "-t'},{name: '设备" + (usbcan_tx.PCAN_PARA1.PCANIndex - 0x51) + "-r'},");
            }
            nodes = nodes.Remove(nodes.IndexOf("]") - 1, 1);
            links = links.Remove(links.IndexOf("]") - 1, 1);
            showBarAction(100);
            string url = Application.StartupPath + "\\" + "sankey.html" + "?nodes=" + nodes + "&links=" + links;
            webBrowsershow(url);
            showResultAction("当前检测任务已结束。");

        }

        public void webBrowsershow(string url)
        {
            sankeyBrowser.BeginInvoke(new Action<string>((Url) => { sankeyBrowser.Navigate(Url); }), url);
        }

        public void showResultAction(string result)
        {
            text.BeginInvoke(new Action<string>((Result) => { text.AppendText(Result); }), result);
            //Delegate.BeginInvoke(a,null,null);
        }

        public void showBarAction(int rate)
        {
            ratebar.BeginInvoke(new Action<int>((Rate) => { ratebar.Value = Rate; }), rate);
        }

        public void startdetect()
        {
            detectThread = new Thread(detectmission);
            detectThread.Start();
        }

        public void stopdetect()
        {
            try
            {
                if (detectThread.ThreadState == ThreadState.Unstarted)
                {
                    MessageBox.Show("检测线程未启动！");
                }
                else if (detectThread.ThreadState == ThreadState.Aborted)
                {
                    MessageBox.Show("检测线程已终止！");
                }
                else if (detectThread.ThreadState == ThreadState.AbortRequested)
                {
                    MessageBox.Show("检测线程正在终止！");
                }
                else if (detectThread.ThreadState == ThreadState.Stopped)
                {
                    MessageBox.Show("检测线程已停止！");
                }
                else if (detectThread.ThreadState == ThreadState.StopRequested)
                {
                    MessageBox.Show("检测线程正在停止！");
                }
                else
                {
                    try
                    {
                        detectThread.Abort();
                        //detectThread = null;
                    }
                    catch
                    {
                        MessageBox.Show("检测线程终止失败！");
                    }
                    showResultAction("收到停止指令!" + "\r\n");
                    showBarAction(0);
                }
            }catch
            {
                MessageBox.Show("未开启检测任务！");
            }
            
        }
    }
}
