
namespace RW.PMS.WinForm.UI
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.MenuBarToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolbtnPoint = new System.Windows.Forms.ToolStripButton();
            this.toolbtnExit = new System.Windows.Forms.ToolStripButton();
            this.toolbtnShutdown = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTorque = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFormulaCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cmbProdNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.axTcpClient1 = new SocketHelper.AxTcpClient(this.components);
            this.axTcpClient2 = new SocketHelper.AxTcpClient(this.components);
            this.tmrTorque = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panelTitle.SuspendLayout();
            this.MenuBarToolStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(1920, 55);
            this.panelTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Window;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1920, 52);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "RWAMS 桩 工 混 线 回 转 支 承 扭 矩 管 理 系 统";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuBarToolStrip
            // 
            this.MenuBarToolStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MenuBarToolStrip.ImageScalingSize = new System.Drawing.Size(70, 60);
            this.MenuBarToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtnPoint,
            this.toolbtnExit,
            this.toolbtnShutdown});
            this.MenuBarToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuBarToolStrip.Location = new System.Drawing.Point(0, 55);
            this.MenuBarToolStrip.Name = "MenuBarToolStrip";
            this.MenuBarToolStrip.Size = new System.Drawing.Size(1920, 68);
            this.MenuBarToolStrip.TabIndex = 74;
            this.MenuBarToolStrip.Text = "toolStrip1";
            // 
            // toolbtnPoint
            // 
            this.toolbtnPoint.AutoSize = false;
            this.toolbtnPoint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.toolbtnPoint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolbtnPoint.Image = global::RW.PMS.WinForm.Properties.Resources.report;
            this.toolbtnPoint.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolbtnPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPoint.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.toolbtnPoint.Name = "toolbtnPoint";
            this.toolbtnPoint.Size = new System.Drawing.Size(65, 65);
            this.toolbtnPoint.Text = "点位信息";
            this.toolbtnPoint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolbtnPoint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbtnPoint.Click += new System.EventHandler(this.toolbtnPoint_Click);
            // 
            // toolbtnExit
            // 
            this.toolbtnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbtnExit.AutoSize = false;
            this.toolbtnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolbtnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnExit.Image")));
            this.toolbtnExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnExit.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolbtnExit.Name = "toolbtnExit";
            this.toolbtnExit.Size = new System.Drawing.Size(65, 65);
            this.toolbtnExit.Text = "退出系统";
            this.toolbtnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbtnExit.Click += new System.EventHandler(this.toolbtnExit_Click);
            // 
            // toolbtnShutdown
            // 
            this.toolbtnShutdown.AutoSize = false;
            this.toolbtnShutdown.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.toolbtnShutdown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolbtnShutdown.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnShutdown.Image")));
            this.toolbtnShutdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnShutdown.Margin = new System.Windows.Forms.Padding(20, 1, 0, 2);
            this.toolbtnShutdown.Name = "toolbtnShutdown";
            this.toolbtnShutdown.Size = new System.Drawing.Size(65, 65);
            this.toolbtnShutdown.Text = "关机";
            this.toolbtnShutdown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolbtnShutdown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolbtnShutdown.Click += new System.EventHandler(this.toolbtnShutdown_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.45763F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.54237F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 939F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 126);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.8159F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 934F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1904, 937);
            this.tableLayoutPanel1.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblTorque);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblFormulaCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtLog);
            this.panel1.Controls.Add(this.cmbProdNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 925);
            this.panel1.TabIndex = 0;
            // 
            // lblTorque
            // 
            this.lblTorque.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTorque.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTorque.Location = new System.Drawing.Point(158, 95);
            this.lblTorque.Name = "lblTorque";
            this.lblTorque.Size = new System.Drawing.Size(196, 23);
            this.lblTorque.TabIndex = 82;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(43, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 22);
            this.label3.TabIndex = 81;
            this.label3.Text = "产品扭矩：";
            // 
            // lblFormulaCode
            // 
            this.lblFormulaCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFormulaCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblFormulaCode.Location = new System.Drawing.Point(158, 58);
            this.lblFormulaCode.Name = "lblFormulaCode";
            this.lblFormulaCode.Size = new System.Drawing.Size(196, 23);
            this.lblFormulaCode.TabIndex = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(43, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 79;
            this.label2.Text = "配方编号：";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtLog.Font = new System.Drawing.Font("宋体", 12F);
            this.txtLog.ForeColor = System.Drawing.Color.Black;
            this.txtLog.Location = new System.Drawing.Point(2, 132);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(490, 793);
            this.txtLog.TabIndex = 78;
            // 
            // cmbProdNo
            // 
            this.cmbProdNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProdNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProdNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbProdNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbProdNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbProdNo.FormattingEnabled = true;
            this.cmbProdNo.Location = new System.Drawing.Point(162, 16);
            this.cmbProdNo.Name = "cmbProdNo";
            this.cmbProdNo.Size = new System.Drawing.Size(213, 29);
            this.cmbProdNo.TabIndex = 77;
            this.cmbProdNo.SelectedIndexChanged += new System.EventHandler(this.cmbformula_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(43, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 76;
            this.label1.Text = "产品型号：";
            // 
            // axTcpClient1
            // 
            this.axTcpClient1.Isclosed = false;
            this.axTcpClient1.IsStartTcpthreading = false;
            this.axTcpClient1.Receivestr = null;
            this.axTcpClient1.ReConectedCount = 0;
            this.axTcpClient1.ReConnectionTime = 3000;
            this.axTcpClient1.ServerIp = null;
            this.axTcpClient1.ServerPort = 0;
            this.axTcpClient1.Tcpclient = null;
            this.axTcpClient1.Tcpthread = null;
            // 
            // axTcpClient2
            // 
            this.axTcpClient2.Isclosed = false;
            this.axTcpClient2.IsStartTcpthreading = false;
            this.axTcpClient2.Receivestr = null;
            this.axTcpClient2.ReConectedCount = 0;
            this.axTcpClient2.ReConnectionTime = 3000;
            this.axTcpClient2.ServerIp = null;
            this.axTcpClient2.ServerPort = 0;
            this.axTcpClient2.Tcpclient = null;
            this.axTcpClient2.Tcpthread = null;
            // 
            // tmrTorque
            // 
            this.tmrTorque.Interval = 6000;
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 

            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.MenuBarToolStrip);
            this.Controls.Add(this.panelTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1438, 822);
            this.Name = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load_1);
            this.panelTitle.ResumeLayout(false);
            this.MenuBarToolStrip.ResumeLayout(false);
            this.MenuBarToolStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStrip MenuBarToolStrip;
        private System.Windows.Forms.ToolStripButton toolbtnExit;
        private System.Windows.Forms.ToolStripButton toolbtnShutdown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolbtnPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbProdNo;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFormulaCode;
        private System.Windows.Forms.Label lblTorque;
        private System.Windows.Forms.Label label3;
        private SocketHelper.AxTcpClient axTcpClient1;
        private SocketHelper.AxTcpClient axTcpClient2;
        private System.Windows.Forms.Timer tmrTorque;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}