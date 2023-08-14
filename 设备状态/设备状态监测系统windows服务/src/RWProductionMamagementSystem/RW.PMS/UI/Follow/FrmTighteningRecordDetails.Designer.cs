
namespace RW.PMS.WinForm.UI.Follow
{
    partial class FrmTighteningRecordDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTighteningRecordDetails));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTorqueShow = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.txtIsQualified = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTighteningDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOperatorValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoltNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPmodel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtprodNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.txtIsQualified);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtTighteningDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOperatorValue);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtBoltNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPmodel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtprodNo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 623);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(664, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 32);
            this.button1.TabIndex = 119;
            this.button1.Text = "打 印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cartesianChart1);
            this.groupBox1.Location = new System.Drawing.Point(83, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 330);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "拧紧曲线";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(3, 17);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(517, 310);
            this.cartesianChart1.TabIndex = 1;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTorqueShow);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblAngle);
            this.groupBox2.Location = new System.Drawing.Point(84, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 80);
            this.groupBox2.TabIndex = 117;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拧紧轴";
            // 
            // lblTorqueShow
            // 
            this.lblTorqueShow.AutoSize = true;
            this.lblTorqueShow.Font = new System.Drawing.Font("微软雅黑", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTorqueShow.Location = new System.Drawing.Point(269, 19);
            this.lblTorqueShow.Name = "lblTorqueShow";
            this.lblTorqueShow.Size = new System.Drawing.Size(85, 43);
            this.lblTorqueShow.TabIndex = 82;
            this.lblTorqueShow.Text = "扭力";
            this.lblTorqueShow.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F);
            this.label6.Location = new System.Drawing.Point(184, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 20);
            this.label6.TabIndex = 80;
            this.label6.Text = "°";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15F);
            this.label8.Location = new System.Drawing.Point(367, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 20);
            this.label8.TabIndex = 81;
            this.label8.Text = "牛·米";
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Font = new System.Drawing.Font("微软雅黑", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAngle.Location = new System.Drawing.Point(100, 19);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(85, 43);
            this.lblAngle.TabIndex = 83;
            this.lblAngle.Text = "角度";
            this.lblAngle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIsQualified
            // 
            this.txtIsQualified.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIsQualified.Location = new System.Drawing.Point(448, 116);
            this.txtIsQualified.Margin = new System.Windows.Forms.Padding(2);
            this.txtIsQualified.Name = "txtIsQualified";
            this.txtIsQualified.ReadOnly = true;
            this.txtIsQualified.Size = new System.Drawing.Size(170, 29);
            this.txtIsQualified.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(359, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 115;
            this.label5.Text = "是否合格";
            // 
            // txtTighteningDate
            // 
            this.txtTighteningDate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTighteningDate.Location = new System.Drawing.Point(145, 114);
            this.txtTighteningDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtTighteningDate.Name = "txtTighteningDate";
            this.txtTighteningDate.ReadOnly = true;
            this.txtTighteningDate.Size = new System.Drawing.Size(170, 29);
            this.txtTighteningDate.TabIndex = 114;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(64, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 113;
            this.label4.Text = "操作时间";
            // 
            // txtOperatorValue
            // 
            this.txtOperatorValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOperatorValue.Location = new System.Drawing.Point(448, 66);
            this.txtOperatorValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtOperatorValue.Name = "txtOperatorValue";
            this.txtOperatorValue.ReadOnly = true;
            this.txtOperatorValue.Size = new System.Drawing.Size(170, 29);
            this.txtOperatorValue.TabIndex = 112;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(359, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 111;
            this.label3.Text = "操作人员";
            // 
            // txtBoltNo
            // 
            this.txtBoltNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBoltNo.Location = new System.Drawing.Point(145, 65);
            this.txtBoltNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoltNo.Name = "txtBoltNo";
            this.txtBoltNo.ReadOnly = true;
            this.txtBoltNo.Size = new System.Drawing.Size(170, 29);
            this.txtBoltNo.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(81, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 109;
            this.label2.Text = "螺栓号";
            // 
            // txtPmodel
            // 
            this.txtPmodel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPmodel.Location = new System.Drawing.Point(448, 16);
            this.txtPmodel.Margin = new System.Windows.Forms.Padding(2);
            this.txtPmodel.Name = "txtPmodel";
            this.txtPmodel.ReadOnly = true;
            this.txtPmodel.Size = new System.Drawing.Size(170, 29);
            this.txtPmodel.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(359, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 107;
            this.label1.Text = "产品型号";
            // 
            // txtprodNo
            // 
            this.txtprodNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtprodNo.Location = new System.Drawing.Point(145, 16);
            this.txtprodNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtprodNo.Name = "txtprodNo";
            this.txtprodNo.ReadOnly = true;
            this.txtprodNo.Size = new System.Drawing.Size(170, 29);
            this.txtprodNo.TabIndex = 106;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(64, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 105;
            this.label7.Text = "产品编号";
            // 
            // FrmTighteningRecordDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 639);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTighteningRecordDetails";
            this.Text = "FrmTighteningRecordDetails";
            this.Load += new System.EventHandler(this.FrmTighteningRecordDetails_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTorqueShow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.TextBox txtIsQualified;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTighteningDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOperatorValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoltNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPmodel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtprodNo;
        private System.Windows.Forms.Label label7;
    }
}