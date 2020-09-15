namespace CANProject
{
    partial class Form_GateWayDetect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设备操作ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.设备参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始检测 = new System.Windows.Forms.Button();
            this.停止检测 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.清空结果 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.发送ID范围 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.id样例 = new System.Windows.Forms.Button();
            this.sankeyBrowser = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备操作ToolStripMenuItem1,
            this.测试用ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1434, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设备操作ToolStripMenuItem1
            // 
            this.设备操作ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备参数ToolStripMenuItem,
            this.关闭设备ToolStripMenuItem});
            this.设备操作ToolStripMenuItem1.Name = "设备操作ToolStripMenuItem1";
            this.设备操作ToolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.设备操作ToolStripMenuItem1.Text = "设备操作";
            // 
            // 设备参数ToolStripMenuItem
            // 
            this.设备参数ToolStripMenuItem.Name = "设备参数ToolStripMenuItem";
            this.设备参数ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.设备参数ToolStripMenuItem.Text = "设备参数";
            this.设备参数ToolStripMenuItem.Click += new System.EventHandler(this.设备参数ToolStripMenuItem_Click);
            // 
            // 关闭设备ToolStripMenuItem
            // 
            this.关闭设备ToolStripMenuItem.Name = "关闭设备ToolStripMenuItem";
            this.关闭设备ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭设备ToolStripMenuItem.Text = "关闭设备";
            this.关闭设备ToolStripMenuItem.Click += new System.EventHandler(this.关闭设备ToolStripMenuItem_Click);
            // 
            // 测试用ToolStripMenuItem
            // 
            this.测试用ToolStripMenuItem.Name = "测试用ToolStripMenuItem";
            this.测试用ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.测试用ToolStripMenuItem.Text = "测试用";
            this.测试用ToolStripMenuItem.Click += new System.EventHandler(this.测试用ToolStripMenuItem_Click);
            // 
            // 开始检测
            // 
            this.开始检测.Location = new System.Drawing.Point(3, 3);
            this.开始检测.Name = "开始检测";
            this.开始检测.Size = new System.Drawing.Size(75, 23);
            this.开始检测.TabIndex = 3;
            this.开始检测.Text = "开始检测";
            this.开始检测.UseVisualStyleBackColor = true;
            this.开始检测.Click += new System.EventHandler(this.开始检测_Click);
            // 
            // 停止检测
            // 
            this.停止检测.Location = new System.Drawing.Point(84, 3);
            this.停止检测.Name = "停止检测";
            this.停止检测.Size = new System.Drawing.Size(75, 23);
            this.停止检测.TabIndex = 3;
            this.停止检测.Text = "停止检测";
            this.停止检测.UseVisualStyleBackColor = true;
            this.停止检测.Click += new System.EventHandler(this.停止检测_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.开始检测);
            this.flowLayoutPanel1.Controls.Add(this.停止检测);
            this.flowLayoutPanel1.Controls.Add(this.清空结果);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(260, 30);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // 清空结果
            // 
            this.清空结果.Location = new System.Drawing.Point(165, 3);
            this.清空结果.Name = "清空结果";
            this.清空结果.Size = new System.Drawing.Size(75, 23);
            this.清空结果.TabIndex = 3;
            this.清空结果.Text = "清空结果";
            this.清空结果.UseVisualStyleBackColor = true;
            this.清空结果.Click += new System.EventHandler(this.清空结果_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 667);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1040, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(3, 15);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(386, 647);
            this.textBox1.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.textBox1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1043, 28);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(391, 662);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "输出日志";
            // 
            // 发送ID范围
            // 
            this.发送ID范围.AutoSize = true;
            this.发送ID范围.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.发送ID范围.Location = new System.Drawing.Point(3, 0);
            this.发送ID范围.Name = "发送ID范围";
            this.发送ID范围.Size = new System.Drawing.Size(65, 12);
            this.发送ID范围.TabIndex = 8;
            this.发送ID范围.Text = "发送ID范围\r\n";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.发送ID范围);
            this.flowLayoutPanel3.Controls.Add(this.textBox2);
            this.flowLayoutPanel3.Controls.Add(this.id样例);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(269, 28);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(377, 30);
            this.flowLayoutPanel3.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(74, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(232, 21);
            this.textBox2.TabIndex = 0;
            // 
            // id样例
            // 
            this.id样例.Location = new System.Drawing.Point(312, 3);
            this.id样例.Name = "id样例";
            this.id样例.Size = new System.Drawing.Size(62, 23);
            this.id样例.TabIndex = 9;
            this.id样例.Text = "id样例";
            this.id样例.UseVisualStyleBackColor = true;
            this.id样例.Click += new System.EventHandler(this.Id样例_Click);
            // 
            // sankeyBrowser
            // 
            this.sankeyBrowser.Location = new System.Drawing.Point(3, 60);
            this.sankeyBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.sankeyBrowser.Name = "sankeyBrowser";
            this.sankeyBrowser.Size = new System.Drawing.Size(1037, 601);
            this.sankeyBrowser.TabIndex = 10;
            // 
            // Form_GateWayDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 691);
            this.Controls.Add(this.sankeyBrowser);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_GateWayDetect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form_GateWayDetect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_GateWayDetect_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设备操作ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 设备参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭设备ToolStripMenuItem;
        private System.Windows.Forms.Button 开始检测;
        private System.Windows.Forms.Button 停止检测;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button 清空结果;
        private System.Windows.Forms.Label 发送ID范围;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button id样例;
        private System.Windows.Forms.WebBrowser sankeyBrowser;
        private System.Windows.Forms.ToolStripMenuItem 测试用ToolStripMenuItem;
    }
}