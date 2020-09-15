namespace CANProject
{
    partial class Form_AutomatedAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_AutomatedAnalysis));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.canMenuStrip1 = new CANProject.CANMenuStrip();
            this.设备操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空物理量列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空待测列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加全部物理量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRemoveReq = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddReq = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPointCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxUdsInterval = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.listBoxOptional = new System.Windows.Forms.ListBox();
            this.listBoxRequest = new System.Windows.Forms.ListBox();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton0_5Byte = new System.Windows.Forms.RadioButton();
            this.radioButton1_5Byte = new System.Windows.Forms.RadioButton();
            this.radioButton2Byte = new System.Windows.Forms.RadioButton();
            this.radioButton1Byte = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonSmallEndian = new System.Windows.Forms.RadioButton();
            this.radioButtonBigEndian = new System.Windows.Forms.RadioButton();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.chartCmp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1.SuspendLayout();
            this.canMenuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCmp)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.canMenuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxResult, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1384, 830);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // canMenuStrip1
            // 
            this.canMenuStrip1.BackColor = System.Drawing.Color.Silver;
            this.canMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.canMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备操作ToolStripMenuItem,
            this.操作ToolStripMenuItem});
            this.canMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.canMenuStrip1.Name = "canMenuStrip1";
            this.canMenuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.canMenuStrip1.Size = new System.Drawing.Size(1384, 28);
            this.canMenuStrip1.TabIndex = 0;
            this.canMenuStrip1.Text = "canMenuStrip1";
            // 
            // 设备操作ToolStripMenuItem
            // 
            this.设备操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备参数ToolStripMenuItem,
            this.关闭设备ToolStripMenuItem});
            this.设备操作ToolStripMenuItem.Name = "设备操作ToolStripMenuItem";
            this.设备操作ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.设备操作ToolStripMenuItem.Text = "设备操作";
            // 
            // 设备参数ToolStripMenuItem
            // 
            this.设备参数ToolStripMenuItem.Name = "设备参数ToolStripMenuItem";
            this.设备参数ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.设备参数ToolStripMenuItem.Text = "设备参数";
            this.设备参数ToolStripMenuItem.Click += new System.EventHandler(this.设备参数ToolStripMenuItem_Click);
            // 
            // 关闭设备ToolStripMenuItem
            // 
            this.关闭设备ToolStripMenuItem.Name = "关闭设备ToolStripMenuItem";
            this.关闭设备ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.关闭设备ToolStripMenuItem.Text = "关闭设备";
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空物理量列表ToolStripMenuItem,
            this.清空待测列表ToolStripMenuItem,
            this.添加全部物理量ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(87, 24);
            this.操作ToolStripMenuItem.Text = "添加/删除";
            // 
            // 清空物理量列表ToolStripMenuItem
            // 
            this.清空物理量列表ToolStripMenuItem.Name = "清空物理量列表ToolStripMenuItem";
            this.清空物理量列表ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.清空物理量列表ToolStripMenuItem.Text = "清空物理量列表";
            this.清空物理量列表ToolStripMenuItem.Click += new System.EventHandler(this.清空物理量列表ToolStripMenuItem_Click);
            // 
            // 清空待测列表ToolStripMenuItem
            // 
            this.清空待测列表ToolStripMenuItem.Name = "清空待测列表ToolStripMenuItem";
            this.清空待测列表ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.清空待测列表ToolStripMenuItem.Text = "清空待测列表";
            this.清空待测列表ToolStripMenuItem.Click += new System.EventHandler(this.清空待测列表ToolStripMenuItem_Click);
            // 
            // 添加全部物理量ToolStripMenuItem
            // 
            this.添加全部物理量ToolStripMenuItem.Name = "添加全部物理量ToolStripMenuItem";
            this.添加全部物理量ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.添加全部物理量ToolStripMenuItem.Text = "添加全部物理量";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 301F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.listBoxOptional, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.listBoxRequest, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 42);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1376, 184);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonAdd);
            this.flowLayoutPanel1.Controls.Add(this.buttonRemove);
            this.flowLayoutPanel1.Controls.Add(this.buttonModify);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(259, 23);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Image = global::CANProject.Properties.Resources.pls10;
            this.buttonAdd.Location = new System.Drawing.Point(0, 0);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(24, 22);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Image = global::CANProject.Properties.Resources.sub10;
            this.buttonRemove.Location = new System.Drawing.Point(24, 0);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(24, 22);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonModify
            // 
            this.buttonModify.Image = ((System.Drawing.Image)(resources.GetObject("buttonModify.Image")));
            this.buttonModify.Location = new System.Drawing.Point(48, 0);
            this.buttonModify.Margin = new System.Windows.Forms.Padding(0);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(24, 22);
            this.buttonModify.TabIndex = 3;
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "物理量列表";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.buttonRemoveReq);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(398, 4);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(259, 23);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // buttonRemoveReq
            // 
            this.buttonRemoveReq.Image = global::CANProject.Properties.Resources.sub10;
            this.buttonRemoveReq.Location = new System.Drawing.Point(0, 0);
            this.buttonRemoveReq.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRemoveReq.Name = "buttonRemoveReq";
            this.buttonRemoveReq.Size = new System.Drawing.Size(24, 22);
            this.buttonRemoveReq.TabIndex = 1;
            this.buttonRemoveReq.UseVisualStyleBackColor = true;
            this.buttonRemoveReq.Click += new System.EventHandler(this.buttonRemoveReq_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "待测物理量列表";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBoxLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(962, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(414, 186);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.HideSelection = false;
            this.richTextBoxLog.Location = new System.Drawing.Point(4, 22);
            this.richTextBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(406, 160);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.buttonAddReq);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(271, 4);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.tableLayoutPanel2.SetRowSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Size = new System.Drawing.Size(119, 178);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // buttonAddReq
            // 
            this.buttonAddReq.Image = global::CANProject.Properties.Resources.add15;
            this.buttonAddReq.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddReq.Location = new System.Drawing.Point(20, 69);
            this.buttonAddReq.Margin = new System.Windows.Forms.Padding(20, 69, 4, 4);
            this.buttonAddReq.Name = "buttonAddReq";
            this.buttonAddReq.Size = new System.Drawing.Size(76, 31);
            this.buttonAddReq.TabIndex = 0;
            this.buttonAddReq.Text = "选择";
            this.buttonAddReq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddReq.UseVisualStyleBackColor = true;
            this.buttonAddReq.Click += new System.EventHandler(this.buttonAddReq_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel4.Controls.Add(this.flowLayoutPanel6);
            this.flowLayoutPanel4.Controls.Add(this.buttonStart);
            this.flowLayoutPanel4.Controls.Add(this.buttonEnd);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(665, 4);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.tableLayoutPanel2.SetRowSpan(this.flowLayoutPanel4, 2);
            this.flowLayoutPanel4.Size = new System.Drawing.Size(293, 178);
            this.flowLayoutPanel4.TabIndex = 3;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.label3);
            this.flowLayoutPanel5.Controls.Add(this.textBoxPointCount);
            this.flowLayoutPanel5.Controls.Add(this.label4);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(4, 4);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(252, 39);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "收集对比点个数";
            // 
            // textBoxPointCount
            // 
            this.textBoxPointCount.Location = new System.Drawing.Point(124, 4);
            this.textBoxPointCount.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPointCount.Name = "textBoxPointCount";
            this.textBoxPointCount.Size = new System.Drawing.Size(81, 25);
            this.textBoxPointCount.TabIndex = 1;
            this.textBoxPointCount.Text = "10";
            this.textBoxPointCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "个";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.label5);
            this.flowLayoutPanel6.Controls.Add(this.textBoxUdsInterval);
            this.flowLayoutPanel6.Controls.Add(this.label6);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(4, 51);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(280, 39);
            this.flowLayoutPanel6.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "诊断指令发送间隔";
            // 
            // textBoxUdsInterval
            // 
            this.textBoxUdsInterval.Location = new System.Drawing.Point(139, 4);
            this.textBoxUdsInterval.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUdsInterval.Name = "textBoxUdsInterval";
            this.textBoxUdsInterval.Size = new System.Drawing.Size(65, 25);
            this.textBoxUdsInterval.TabIndex = 1;
            this.textBoxUdsInterval.Text = "2500";
            this.textBoxUdsInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(212, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "毫秒";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(4, 98);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 29);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "开始收集";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Enabled = false;
            this.buttonEnd.Location = new System.Drawing.Point(4, 135);
            this.buttonEnd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(100, 29);
            this.buttonEnd.TabIndex = 3;
            this.buttonEnd.Text = "结束收集";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // listBoxOptional
            // 
            this.listBoxOptional.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxOptional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxOptional.FormattingEnabled = true;
            this.listBoxOptional.ItemHeight = 15;
            this.listBoxOptional.Location = new System.Drawing.Point(4, 35);
            this.listBoxOptional.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxOptional.Name = "listBoxOptional";
            this.listBoxOptional.Size = new System.Drawing.Size(259, 147);
            this.listBoxOptional.TabIndex = 4;
            // 
            // listBoxRequest
            // 
            this.listBoxRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRequest.FormattingEnabled = true;
            this.listBoxRequest.ItemHeight = 15;
            this.listBoxRequest.Location = new System.Drawing.Point(398, 35);
            this.listBoxRequest.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxRequest.Name = "listBoxRequest";
            this.listBoxRequest.Size = new System.Drawing.Size(259, 147);
            this.listBoxRequest.TabIndex = 5;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResult.Location = new System.Drawing.Point(4, 234);
            this.groupBoxResult.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxResult.Size = new System.Drawing.Size(1376, 592);
            this.groupBoxResult.TabIndex = 2;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "解析结果";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.listBoxResult, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.listBoxItems, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.chartCmp, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 22);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1368, 566);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // listBoxResult
            // 
            this.listBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.ItemHeight = 15;
            this.listBoxResult.Location = new System.Drawing.Point(4, 4);
            this.listBoxResult.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(259, 558);
            this.listBoxResult.TabIndex = 0;
            this.listBoxResult.SelectedIndexChanged += new System.EventHandler(this.listBoxResult_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(271, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(115, 558);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 25);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 19);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "显示图像";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton0_5Byte);
            this.groupBox5.Controls.Add(this.radioButton1_5Byte);
            this.groupBox5.Controls.Add(this.radioButton2Byte);
            this.groupBox5.Controls.Add(this.radioButton1Byte);
            this.groupBox5.Location = new System.Drawing.Point(8, 145);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(100, 144);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "字节数";
            // 
            // radioButton0_5Byte
            // 
            this.radioButton0_5Byte.AutoSize = true;
            this.radioButton0_5Byte.Location = new System.Drawing.Point(8, 80);
            this.radioButton0_5Byte.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton0_5Byte.Name = "radioButton0_5Byte";
            this.radioButton0_5Byte.Size = new System.Drawing.Size(82, 19);
            this.radioButton0_5Byte.TabIndex = 3;
            this.radioButton0_5Byte.Text = "0.5字节";
            this.radioButton0_5Byte.UseVisualStyleBackColor = true;
            this.radioButton0_5Byte.CheckedChanged += new System.EventHandler(this.radioButton0_5Byte_CheckedChanged);
            // 
            // radioButton1_5Byte
            // 
            this.radioButton1_5Byte.AutoSize = true;
            this.radioButton1_5Byte.Location = new System.Drawing.Point(8, 108);
            this.radioButton1_5Byte.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1_5Byte.Name = "radioButton1_5Byte";
            this.radioButton1_5Byte.Size = new System.Drawing.Size(82, 19);
            this.radioButton1_5Byte.TabIndex = 2;
            this.radioButton1_5Byte.Text = "1.5字节";
            this.radioButton1_5Byte.UseVisualStyleBackColor = true;
            this.radioButton1_5Byte.CheckedChanged += new System.EventHandler(this.radioButton1_5Byte_CheckedChanged);
            // 
            // radioButton2Byte
            // 
            this.radioButton2Byte.AutoSize = true;
            this.radioButton2Byte.Location = new System.Drawing.Point(8, 52);
            this.radioButton2Byte.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2Byte.Name = "radioButton2Byte";
            this.radioButton2Byte.Size = new System.Drawing.Size(66, 19);
            this.radioButton2Byte.TabIndex = 1;
            this.radioButton2Byte.Text = "2字节";
            this.radioButton2Byte.UseVisualStyleBackColor = true;
            this.radioButton2Byte.CheckedChanged += new System.EventHandler(this.radioButton2Byte_CheckedChanged);
            // 
            // radioButton1Byte
            // 
            this.radioButton1Byte.AutoSize = true;
            this.radioButton1Byte.Checked = true;
            this.radioButton1Byte.Location = new System.Drawing.Point(8, 25);
            this.radioButton1Byte.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1Byte.Name = "radioButton1Byte";
            this.radioButton1Byte.Size = new System.Drawing.Size(66, 19);
            this.radioButton1Byte.TabIndex = 0;
            this.radioButton1Byte.TabStop = true;
            this.radioButton1Byte.Text = "1字节";
            this.radioButton1Byte.UseVisualStyleBackColor = true;
            this.radioButton1Byte.CheckedChanged += new System.EventHandler(this.radioButton1Byte_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonSmallEndian);
            this.groupBox4.Controls.Add(this.radioButtonBigEndian);
            this.groupBox4.Location = new System.Drawing.Point(8, 52);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(100, 85);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "字节序";
            // 
            // radioButtonSmallEndian
            // 
            this.radioButtonSmallEndian.AutoSize = true;
            this.radioButtonSmallEndian.Checked = true;
            this.radioButtonSmallEndian.Location = new System.Drawing.Point(8, 52);
            this.radioButtonSmallEndian.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonSmallEndian.Name = "radioButtonSmallEndian";
            this.radioButtonSmallEndian.Size = new System.Drawing.Size(73, 19);
            this.radioButtonSmallEndian.TabIndex = 1;
            this.radioButtonSmallEndian.TabStop = true;
            this.radioButtonSmallEndian.Text = "小端序";
            this.radioButtonSmallEndian.UseVisualStyleBackColor = true;
            this.radioButtonSmallEndian.CheckedChanged += new System.EventHandler(this.radioButtonSmallEndian_CheckedChanged);
            // 
            // radioButtonBigEndian
            // 
            this.radioButtonBigEndian.AutoSize = true;
            this.radioButtonBigEndian.Location = new System.Drawing.Point(8, 25);
            this.radioButtonBigEndian.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonBigEndian.Name = "radioButtonBigEndian";
            this.radioButtonBigEndian.Size = new System.Drawing.Size(73, 19);
            this.radioButtonBigEndian.TabIndex = 0;
            this.radioButtonBigEndian.Text = "大端序";
            this.radioButtonBigEndian.UseVisualStyleBackColor = true;
            this.radioButtonBigEndian.CheckedChanged += new System.EventHandler(this.radioButtonBigEndian_CheckedChanged);
            // 
            // listBoxItems
            // 
            this.listBoxItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.ItemHeight = 15;
            this.listBoxItems.Location = new System.Drawing.Point(394, 4);
            this.listBoxItems.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.Size = new System.Drawing.Size(259, 558);
            this.listBoxItems.TabIndex = 2;
            this.listBoxItems.SelectedIndexChanged += new System.EventHandler(this.listBoxItems_SelectedIndexChanged);
            // 
            // chartCmp
            // 
            chartArea1.Name = "ChartArea1";
            this.chartCmp.ChartAreas.Add(chartArea1);
            this.chartCmp.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartCmp.Legends.Add(legend1);
            this.chartCmp.Location = new System.Drawing.Point(661, 4);
            this.chartCmp.Margin = new System.Windows.Forms.Padding(4);
            this.chartCmp.Name = "chartCmp";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCmp.Series.Add(series1);
            this.chartCmp.Size = new System.Drawing.Size(703, 558);
            this.chartCmp.TabIndex = 3;
            this.chartCmp.Text = "chart1";
            // 
            // Form_AutomatedAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 830);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_AutomatedAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_AutomatedAnalysis_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.canMenuStrip1.ResumeLayout(false);
            this.canMenuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.groupBoxResult.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCmp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CANMenuStrip canMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.ListBox listBoxOptional;
        private System.Windows.Forms.ToolStripMenuItem 设备操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭设备ToolStripMenuItem;
        private System.Windows.Forms.Button buttonAddReq;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxRequest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPointCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListBox listBoxResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxItems;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCmp;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton0_5Byte;
        private System.Windows.Forms.RadioButton radioButton1_5Byte;
        private System.Windows.Forms.RadioButton radioButton2Byte;
        private System.Windows.Forms.RadioButton radioButton1Byte;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonSmallEndian;
        private System.Windows.Forms.RadioButton radioButtonBigEndian;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxUdsInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空物理量列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空待测列表ToolStripMenuItem;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Button buttonRemoveReq;
        private System.Windows.Forms.ToolStripMenuItem 添加全部物理量ToolStripMenuItem;
        private System.Windows.Forms.Button buttonEnd;
    }
}