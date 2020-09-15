namespace CANProject
{
    partial class Form_AddOptional
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonOE = new System.Windows.Forms.RadioButton();
            this.radioButtonOS = new System.Windows.Forms.RadioButton();
            this.listBoxOBD = new System.Windows.Forms.ListBox();
            this.buttonAddOBD = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonUE = new System.Windows.Forms.RadioButton();
            this.radioButtonUS = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCANID = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSID = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPARAM = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDES = new System.Windows.Forms.TextBox();
            this.buttonAddUDS = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(151, 328);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(143, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OBD";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.groupBox1);
            this.flowLayoutPanel3.Controls.Add(this.listBoxOBD);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddOBD);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(137, 296);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonOE);
            this.groupBox1.Controls.Add(this.radioButtonOS);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 45);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帧格式";
            // 
            // radioButtonOE
            // 
            this.radioButtonOE.AutoSize = true;
            this.radioButtonOE.Location = new System.Drawing.Point(71, 20);
            this.radioButtonOE.Name = "radioButtonOE";
            this.radioButtonOE.Size = new System.Drawing.Size(59, 16);
            this.radioButtonOE.TabIndex = 1;
            this.radioButtonOE.TabStop = true;
            this.radioButtonOE.Text = "扩展帧";
            this.radioButtonOE.UseVisualStyleBackColor = true;
            // 
            // radioButtonOS
            // 
            this.radioButtonOS.AutoSize = true;
            this.radioButtonOS.Location = new System.Drawing.Point(6, 20);
            this.radioButtonOS.Name = "radioButtonOS";
            this.radioButtonOS.Size = new System.Drawing.Size(59, 16);
            this.radioButtonOS.TabIndex = 0;
            this.radioButtonOS.TabStop = true;
            this.radioButtonOS.Text = "标准帧";
            this.radioButtonOS.UseVisualStyleBackColor = true;
            // 
            // listBoxOBD
            // 
            this.listBoxOBD.FormattingEnabled = true;
            this.listBoxOBD.ItemHeight = 12;
            this.listBoxOBD.Location = new System.Drawing.Point(3, 54);
            this.listBoxOBD.Name = "listBoxOBD";
            this.listBoxOBD.Size = new System.Drawing.Size(130, 208);
            this.listBoxOBD.TabIndex = 0;
            // 
            // buttonAddOBD
            // 
            this.buttonAddOBD.Location = new System.Drawing.Point(3, 268);
            this.buttonAddOBD.Name = "buttonAddOBD";
            this.buttonAddOBD.Size = new System.Drawing.Size(130, 23);
            this.buttonAddOBD.TabIndex = 1;
            this.buttonAddOBD.Text = "添加";
            this.buttonAddOBD.UseVisualStyleBackColor = true;
            this.buttonAddOBD.Click += new System.EventHandler(this.buttonAddOBD_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(143, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "UDS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel6);
            this.flowLayoutPanel1.Controls.Add(this.buttonAddUDS);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(137, 296);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonUE);
            this.groupBox2.Controls.Add(this.radioButtonUS);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帧格式";
            // 
            // radioButtonUE
            // 
            this.radioButtonUE.AutoSize = true;
            this.radioButtonUE.Location = new System.Drawing.Point(71, 20);
            this.radioButtonUE.Name = "radioButtonUE";
            this.radioButtonUE.Size = new System.Drawing.Size(59, 16);
            this.radioButtonUE.TabIndex = 1;
            this.radioButtonUE.TabStop = true;
            this.radioButtonUE.Text = "扩展帧";
            this.radioButtonUE.UseVisualStyleBackColor = true;
            // 
            // radioButtonUS
            // 
            this.radioButtonUS.AutoSize = true;
            this.radioButtonUS.Location = new System.Drawing.Point(6, 20);
            this.radioButtonUS.Name = "radioButtonUS";
            this.radioButtonUS.Size = new System.Drawing.Size(59, 16);
            this.radioButtonUS.TabIndex = 0;
            this.radioButtonUS.TabStop = true;
            this.radioButtonUS.Text = "标准帧";
            this.radioButtonUS.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.textBoxCANID);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 54);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(139, 29);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "CAN ID";
            // 
            // textBoxCANID
            // 
            this.textBoxCANID.Location = new System.Drawing.Point(50, 3);
            this.textBoxCANID.Name = "textBoxCANID";
            this.textBoxCANID.Size = new System.Drawing.Size(80, 21);
            this.textBoxCANID.TabIndex = 1;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.label2);
            this.flowLayoutPanel4.Controls.Add(this.textBoxSID);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 89);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(139, 29);
            this.flowLayoutPanel4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(21, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "SID";
            // 
            // textBoxSID
            // 
            this.textBoxSID.Location = new System.Drawing.Point(50, 3);
            this.textBoxSID.Name = "textBoxSID";
            this.textBoxSID.Size = new System.Drawing.Size(80, 21);
            this.textBoxSID.TabIndex = 1;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.label3);
            this.flowLayoutPanel5.Controls.Add(this.textBoxPARAM);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 124);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(139, 29);
            this.flowLayoutPanel5.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(9, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "PARAM";
            // 
            // textBoxPARAM
            // 
            this.textBoxPARAM.Location = new System.Drawing.Point(50, 3);
            this.textBoxPARAM.Name = "textBoxPARAM";
            this.textBoxPARAM.Size = new System.Drawing.Size(80, 21);
            this.textBoxPARAM.TabIndex = 1;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.label4);
            this.flowLayoutPanel6.Controls.Add(this.textBoxDES);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 159);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(139, 29);
            this.flowLayoutPanel6.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "描述";
            // 
            // textBoxDES
            // 
            this.textBoxDES.Location = new System.Drawing.Point(50, 3);
            this.textBoxDES.Name = "textBoxDES";
            this.textBoxDES.Size = new System.Drawing.Size(80, 21);
            this.textBoxDES.TabIndex = 1;
            // 
            // buttonAddUDS
            // 
            this.buttonAddUDS.Location = new System.Drawing.Point(3, 194);
            this.buttonAddUDS.Name = "buttonAddUDS";
            this.buttonAddUDS.Size = new System.Drawing.Size(131, 23);
            this.buttonAddUDS.TabIndex = 6;
            this.buttonAddUDS.Text = "添加";
            this.buttonAddUDS.UseVisualStyleBackColor = true;
            this.buttonAddUDS.Click += new System.EventHandler(this.buttonAddUDS_Click);
            // 
            // Form_AddOptional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(151, 328);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_AddOptional";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonOE;
        private System.Windows.Forms.RadioButton radioButtonOS;
        private System.Windows.Forms.ListBox listBoxOBD;
        private System.Windows.Forms.Button buttonAddOBD;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonUE;
        private System.Windows.Forms.RadioButton radioButtonUS;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCANID;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSID;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPARAM;
        private System.Windows.Forms.Button buttonAddUDS;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDES;
    }
}