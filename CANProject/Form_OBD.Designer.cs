namespace CANProject
{
    partial class Form_OBD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.canMenuStrip1 = new CANProject.CANMenuStrip();
            this.设备操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonEx = new System.Windows.Forms.RadioButton();
            this.radioButtonNotEx = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSelAll = new System.Windows.Forms.Button();
            this.buttonInverse = new System.Windows.Forms.Button();
            this.checkBoxRepeat = new System.Windows.Forms.CheckBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.dataGridViewshow = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.canMenuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewshow)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.canMenuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewshow, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1384, 830);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // canMenuStrip1
            // 
            this.canMenuStrip1.BackColor = System.Drawing.Color.Silver;
            this.canMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.canMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备操作ToolStripMenuItem});
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxRepeat);
            this.flowLayoutPanel1.Controls.Add(this.buttonGet);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 53);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(933, 56);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonEx);
            this.groupBox2.Controls.Add(this.radioButtonNotEx);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(177, 52);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帧格式";
            // 
            // radioButtonEx
            // 
            this.radioButtonEx.AutoSize = true;
            this.radioButtonEx.Location = new System.Drawing.Point(95, 24);
            this.radioButtonEx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonEx.Name = "radioButtonEx";
            this.radioButtonEx.Size = new System.Drawing.Size(73, 19);
            this.radioButtonEx.TabIndex = 1;
            this.radioButtonEx.TabStop = true;
            this.radioButtonEx.Text = "扩展帧";
            this.radioButtonEx.UseVisualStyleBackColor = true;
            // 
            // radioButtonNotEx
            // 
            this.radioButtonNotEx.AutoSize = true;
            this.radioButtonNotEx.Location = new System.Drawing.Point(8, 24);
            this.radioButtonNotEx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonNotEx.Name = "radioButtonNotEx";
            this.radioButtonNotEx.Size = new System.Drawing.Size(73, 19);
            this.radioButtonNotEx.TabIndex = 0;
            this.radioButtonNotEx.TabStop = true;
            this.radioButtonNotEx.Text = "标准帧";
            this.radioButtonNotEx.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSelAll);
            this.groupBox1.Controls.Add(this.buttonInverse);
            this.groupBox1.Location = new System.Drawing.Point(189, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(225, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择";
            // 
            // buttonSelAll
            // 
            this.buttonSelAll.Location = new System.Drawing.Point(8, 16);
            this.buttonSelAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSelAll.Name = "buttonSelAll";
            this.buttonSelAll.Size = new System.Drawing.Size(100, 29);
            this.buttonSelAll.TabIndex = 1;
            this.buttonSelAll.Text = "全选";
            this.buttonSelAll.UseVisualStyleBackColor = true;
            this.buttonSelAll.Click += new System.EventHandler(this.buttonSelAll_Click);
            // 
            // buttonInverse
            // 
            this.buttonInverse.Location = new System.Drawing.Point(116, 16);
            this.buttonInverse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonInverse.Name = "buttonInverse";
            this.buttonInverse.Size = new System.Drawing.Size(100, 29);
            this.buttonInverse.TabIndex = 1;
            this.buttonInverse.Text = "反选";
            this.buttonInverse.UseVisualStyleBackColor = true;
            this.buttonInverse.Click += new System.EventHandler(this.buttonInverse_Click);
            // 
            // checkBoxRepeat
            // 
            this.checkBoxRepeat.AutoSize = true;
            this.checkBoxRepeat.Location = new System.Drawing.Point(445, 25);
            this.checkBoxRepeat.Margin = new System.Windows.Forms.Padding(27, 25, 4, 4);
            this.checkBoxRepeat.Name = "checkBoxRepeat";
            this.checkBoxRepeat.Size = new System.Drawing.Size(89, 19);
            this.checkBoxRepeat.TabIndex = 1;
            this.checkBoxRepeat.Text = "循环获取";
            this.checkBoxRepeat.UseVisualStyleBackColor = true;
            this.checkBoxRepeat.CheckedChanged += new System.EventHandler(this.checkBoxRepeat_CheckedChanged);
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(542, 19);
            this.buttonGet.Margin = new System.Windows.Forms.Padding(4, 19, 4, 4);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(100, 29);
            this.buttonGet.TabIndex = 2;
            this.buttonGet.Text = "获取";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // dataGridViewshow
            // 
            this.dataGridViewshow.AllowUserToAddRows = false;
            this.dataGridViewshow.AllowUserToDeleteRows = false;
            this.dataGridViewshow.AllowUserToResizeColumns = false;
            this.dataGridViewshow.AllowUserToResizeRows = false;
            this.dataGridViewshow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewshow.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewshow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewshow.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewshow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewshow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewshow.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewshow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewshow.Location = new System.Drawing.Point(4, 117);
            this.dataGridViewshow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewshow.Name = "dataGridViewshow";
            this.dataGridViewshow.RowHeadersVisible = false;
            this.dataGridViewshow.RowTemplate.Height = 23;
            this.dataGridViewshow.Size = new System.Drawing.Size(1376, 709);
            this.dataGridViewshow.TabIndex = 2;
            this.dataGridViewshow.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewshow_CellValueChanged);
            this.dataGridViewshow.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewshow_CurrentCellDirtyStateChanged);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "选中";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "描述";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "值";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Form_OBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1384, 830);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.canMenuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form_OBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.canMenuStrip1.ResumeLayout(false);
            this.canMenuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewshow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CANMenuStrip canMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设备操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭设备ToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSelAll;
        private System.Windows.Forms.Button buttonInverse;
        private System.Windows.Forms.CheckBox checkBoxRepeat;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.DataGridView dataGridViewshow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonEx;
        private System.Windows.Forms.RadioButton radioButtonNotEx;
    }
}