﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.Model;
using RW.PMS.Model.BaseInfo;
using RW.PMS.Model.Sys;
using Spire.Xls;
using Spire.Xls.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Windows.Forms;
using Chart = Spire.Xls.Chart;
using Workbook = Spire.Xls.Workbook;
using Worksheet = Spire.Xls.Worksheet;

namespace RW.PMS.WinForm.UI.Follow
{
    public partial class FrmTighteningRecord : Form
    {
        private static IBLL_BaseInfo BLL = DIService.GetService<IBLL_BaseInfo>();
        private static IBLL_System SysBLL = DIService.GetService<IBLL_System>();
        public static List<TighteningRecordModel> resultList = new List<TighteningRecordModel>();

       
        public FrmTighteningRecord()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            this.dataGridView1.AutoGenerateColumns = false;

            //设置自动换行   
            this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //设置自动调整高度 
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //让整个DataGridView的所有cell都可以自动换行。
            this.dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void FrmTighteningRecord_Load(object sender, EventArgs e)
        {
            BindDDL();
            bindData();

            //System.Threading.ThreadPool.QueueUserWorkItem((object obj) =>
            //{
            //    HttpContext context =obj as HttpContext;
            //    if (context != null)
            //    {

            //    }

            //},HttpContext.Current);
        }

        //绑定查询 
        public void bindData()
        {
            try
            {
                List<TighteningRecordModel> list = BLL.GetTighteningRecord(new TighteningRecordModel() { PID = this.cmbPID.SelectedValue.ToInt(), Operators = this.cmbOperator.SelectedValue.ToInt(), prodNo = this.txtProdCode.Text.Trim() });
                dataGridView1.DataSource = list;
                resultList = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BindDDL()
        {
            List<BaseProductModelModel> faultTypelist = BLL.GetProductModelAll();
            faultTypelist.Insert(0, new BaseProductModelModel { ID = 0, Pmodel = "--请选择--" });
            this.cmbPID.DataSource = faultTypelist;
            this.cmbPID.ValueMember = "ID";
            this.cmbPID.DisplayMember = "Pmodel";

            List<UserModel> userlist = SysBLL.GetUserList(new UserModel());
            userlist.Insert(0, new UserModel { userID = 0, empName = "--请选择--" });
            this.cmbOperator.DataSource = userlist;
            this.cmbOperator.ValueMember = "userID";
            this.cmbOperator.DisplayMember = "empName";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PrintDoc();
            //this.printPreviewDialog1.ShowDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
             printPreviewDialog.ShowDialog();
        }

        /// <summary>
        /// 获得一个对象的所有属性
        /// </summary>
        /// <returns></returns>
        private string[] GetPropertyNameArray()
        {
            PropertyInfo[] props = null;
            try
            {
                Type type = typeof(TighteningRecordExcelModel);
                object obj = Activator.CreateInstance(type);
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                string[] array = props.Select(t => t.Name).ToArray();
                return array;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void PrintDoc()
        {




            //HttpContext.Current.Session["username"] = HttpContext.Current.User.Identity.Name;
            List<TighteningRecordExcelModel> list = BLL.GetTighteningRecordExcel(new TighteningRecordModel() { PID = this.cmbPID.SelectedValue.ToInt(), Operators = this.cmbOperator.SelectedValue.ToInt(), prodNo = this.txtProdCode.Text.Trim() });
            DataTable ExportDt = MySqlHelper.ListToTable(list);

            string[] oldColumn = GetPropertyNameArray();
            string[] newColumn = new string[] { "产品编号", "螺栓号", "拧紧值", "角度值", "操作人员", "拧紧时间" };

            //调用改写的NPOI方法   
            var data = ExcelHelper.MyExport(ExportDt, "拧紧信息导出文件", DateTime.Now.ToLongDateString() + "_拧紧信息", oldColumn, newColumn);

            string Fileurl = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Upload\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

            //文件夹不存在则创建 
            if (!Directory.Exists(Fileurl))
            {
                Directory.CreateDirectory(Fileurl);
            }
            string path = Fileurl + "拧紧信息导出文件" + DateTime.Now.ToLongDateString() + ".xlsx";
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(data, 0, data.Length);
            fs.Dispose();

            createChart(path, list.Count);

        }

        /// <summary>
        /// 创建图表
        /// </summary>
        /// <param name="path"></param>
        public void createChart(string path, int dataCount)
        {
            //加载Excel文档
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(path);
            //获取工作表
            Worksheet sheet = workbook.Worksheets[0];

            //根据示例数据创建柱形图
            Chart chart = sheet.Charts.Add();
            chart.DataRange = sheet.Range["A1:D" + dataCount + 1];
            chart.SeriesDataFromRange = false;

            //设置图表的位置
            chart.LeftColumn = 8;
            chart.TopRow = 1;
            chart.RightColumn = 20;
            chart.BottomRow = 28;

            //将图表类型设置为柱形图表
            var cs1 = (ChartSerie)chart.Series[1];
            cs1.Name = "拧紧值";
            cs1.SerieType = ExcelChartType.ColumnClustered;
            var cs2 = (ChartSerie)chart.Series[2];
            cs2.Name = "角度值";
            cs2.SerieType = ExcelChartType.ColumnClustered;

            //将图表类型设置为折线图表
            //var cs3 = (ChartSerie)chart.Series[0];
            //cs3.Name = "标准值";
            //cs3.SerieType = ExcelChartType.LineMarkers;

            //设置图表标题为空
            chart.ChartTitle = string.Empty;

            //保存生成的excel文档
            workbook.SaveToFile(path, ExcelVersion.Version2010);
            System.Diagnostics.Process.Start(path);

            PrintDialog dialog = new PrintDialog();
            workbook.PrintDialog = dialog;
            //设置打印页面范围
            dialog.PrinterSettings.FromPage = 0;
            dialog.PrinterSettings.ToPage = 1;
            dialog.PrinterSettings.PrintRange = PrintRange.SomePages;
            //设置打印份数
            dialog.PrinterSettings.Copies = 1;
            //设置打印机名称
            //dialog.PrinterSettings.PrinterName = "";
            PrintDocument pd = workbook.PrintDocument;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Printing.PaperSize pageSize = e.PageSettings.PaperSize;
            Font font = new Font("宋体", 12);
            e.Graphics.DrawString("菜单信息", font, Brushes.Red, 400, 20);
            e.Graphics.DrawString("时间" + DateTime.Now.ToLongDateString(), font, Brushes.Black, 20, 40);
            int x = 10;
            int y = 100;
            
            

            PrintDataGridView.Print(dataGridView1, true, e, ref x, ref y); //注意：PrintDataGridView该类位于上面所写的业务逻辑层之中
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //获取ID 
            int ID = dataGridView1.SelectedRows.Count > 0 ? Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value) : 0;
            string prodNo = dataGridView1.SelectedRows.Count > 0 ? Convert.ToString(dataGridView1.SelectedRows[0].Cells["prodNo"].Value) : "";
            string Pmodel = dataGridView1.SelectedRows.Count > 0 ? Convert.ToString(dataGridView1.SelectedRows[0].Cells["Pmodel"].Value) : "";
            double TorqueValue2 = dataGridView1.SelectedRows.Count > 0 ? Convert.ToDouble(dataGridView1.SelectedRows[0].Cells["TorqueValue2"].Value) : 0;
            double AngleValue1 = dataGridView1.SelectedRows.Count > 0 ? Convert.ToDouble(dataGridView1.SelectedRows[0].Cells["AngleValue1"].Value) : 0;
            int BoltNo = dataGridView1.SelectedRows.Count > 0 ? Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BoltNo"].Value) : 0;
            string OperatorValue = dataGridView1.SelectedRows.Count > 0 ? Convert.ToString(dataGridView1.SelectedRows[0].Cells["OperatorValue"].Value) : "";
            DateTime TighteningDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["TighteningDate"].Value);
            string IsQualified = dataGridView1.SelectedRows.Count > 0 ? Convert.ToString(dataGridView1.SelectedRows[0].Cells["IsQualified"].Value) : "";



            FrmTighteningRecordDetails feedback = new FrmTighteningRecordDetails(ID,prodNo,Pmodel,TorqueValue2,AngleValue1,BoltNo,OperatorValue,TighteningDate,IsQualified);

            DialogResult dialogresult = feedback.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          ControlDiagram controlDiagram=  new ControlDiagram();
            DialogResult dialogresult = controlDiagram.ShowDialog();

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
    class PrintDataGridView
        {
        private static List<DataGridViewCellPrint> CellPrintList = new List<DataGridViewCellPrint>();
        private static PageSetupDialog pageSetup = null;
        private static int printRowCount = 0;

        private static bool IsPrint = true;
        private static bool IsRole = true;
        private static int PoXTmp = 0;
        private static int PoYTmp = 0;
        private static int WidthTmp = 0;
        private static int HeightTmp = 0;
        private static int RowIndex = 0;
        /// <summary>
        /// 打印DataGridView控件
        /// </summary>
        /// <param name="dataGridView">DataGridView控件</param>
        /// <param name="includeColumnText">是否包括列标题</param>
        /// <param name="e">为 System.Drawing.Printing.PrintDocument.PrintPage 事件提供数据。</param>
        /// <param name="PoX">起始X坐标</param>
        /// <param name="PoY">起始Y坐标</param>
        public static void Print(DataGridView dataGridView, bool includeColumnText, PrintPageEventArgs eValue, ref int PoX, ref int PoY)
            {
                pageSetup = new PageSetupDialog();
                pageSetup.PageSettings = eValue.PageSettings;
            //pageSetup.Document;
           
            pageSetup.ShowDialog();
           
            try
                {
                    if (PrintDataGridView.IsPrint)
                    {
                        PrintDataGridView.printRowCount = 0;
                        PrintDataGridView.IsPrint = false;
                        PrintDataGridView.DataGridViewCellVsList(dataGridView, includeColumnText);
                        if (PrintDataGridView.CellPrintList.Count == 0)
                            return;
                        if (PoX > eValue.MarginBounds.Left)
                            PrintDataGridView.IsRole = true;
                        else
                            PrintDataGridView.IsRole = false;
                        PrintDataGridView.PoXTmp = PoX;
                        PrintDataGridView.PoYTmp = PoY;
                        PrintDataGridView.RowIndex = 0;
                        WidthTmp = 0;
                        HeightTmp = 0;
                    }
                    if (PrintDataGridView.printRowCount != 0)
                    {
                        if (IsRole)
                        {
                            PoX = PoXTmp = eValue.MarginBounds.Left;
                            PoY = PoYTmp = eValue.MarginBounds.Top;
                        }
                        else
                        {
                            PoX = PoXTmp;
                            PoY = PoYTmp;
                        }
                    }
                    while (PrintDataGridView.printRowCount < PrintDataGridView.CellPrintList.Count)
                    {
                        DataGridViewCellPrint CellPrint = CellPrintList[PrintDataGridView.printRowCount];
                        if (RowIndex == CellPrint.RowIndex)
                            PoX = PoX + WidthTmp;
                        else
                        {
                            PoX = PoXTmp;
                            PoY = PoY + HeightTmp;
                            if (PoY + HeightTmp > eValue.MarginBounds.Bottom)
                            {
                                HeightTmp = 0;
                                eValue.HasMorePages = true;
                                return;
                            }
                        }
                        using (SolidBrush solidBrush = new SolidBrush(CellPrint.BackColor))
                        {
                            RectangleF rectF1 = new RectangleF(PoX, PoY, CellPrint.Width, CellPrint.Height);
                            eValue.Graphics.FillRectangle(solidBrush, rectF1);
                            using (Pen pen = new Pen(Color.Black, 1))
                                eValue.Graphics.DrawRectangle(pen, Rectangle.Round(rectF1));
                            solidBrush.Color = CellPrint.ForeColor;
                            eValue.Graphics.DrawString(CellPrint.FormattedValue, CellPrint.Font, solidBrush, new Point(PoX + 2, PoY + 3));
                        }
                        WidthTmp =CellPrint.Width;
                        HeightTmp = CellPrint.Height;
                        RowIndex = CellPrint.RowIndex;
                        PrintDataGridView.printRowCount++;
                    }
                    PoY = PoY + HeightTmp;
                    eValue.HasMorePages = false;
                    PrintDataGridView.IsPrint = true;
                }
                catch (Exception ex)
                {
                    eValue.HasMorePages = false;
                    PrintDataGridView.IsPrint = true;
                    throw ex;
                }

            }

            /// <summary>
            /// 将DataGridView控件内容转变到 CellPrintList
            /// </summary>
            /// <param name="dataGridView">DataGridView控件</param>
            /// <param name="includeColumnText">是否包括列标题</param>
            private static void DataGridViewCellVsList(DataGridView dataGridView, bool includeColumnText)
            {
                CellPrintList.Clear();
                try
                {
                    int rowsCount = dataGridView.Rows.Count;
                    int colsCount = dataGridView.Columns.Count;

                    //最后一行是供输入的行时，不用读数据。
                    if (dataGridView.Rows[rowsCount - 1].IsNewRow)
                        rowsCount--;
                    //包括列标题
                    if (includeColumnText)
                    {
                        for (int columnsIndex = 0; columnsIndex < colsCount; columnsIndex++)
                        {
                            if (dataGridView.Columns[columnsIndex].Visible)
                            {
                                DataGridViewCellPrint CellPrint = new DataGridViewCellPrint();
                                CellPrint.FormattedValue = dataGridView.Columns[columnsIndex].HeaderText;
                                CellPrint.RowIndex = 0;
                                CellPrint.ColumnIndex = columnsIndex;
                                CellPrint.Font = dataGridView.Columns[columnsIndex].HeaderCell.Style.Font;
                                CellPrint.BackColor = Color.Orange;// dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
                                CellPrint.ForeColor = dataGridView.ColumnHeadersDefaultCellStyle.ForeColor;
                                CellPrint.Width = dataGridView.Columns[columnsIndex].Width;
                                CellPrint.Height = dataGridView.ColumnHeadersHeight;
                                CellPrintList.Add(CellPrint);
                            }
                        }
                    }
                    //读取单元格数据
                    for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
                    {
                        for (int columnsIndex = 0; columnsIndex < colsCount; columnsIndex++)
                        {
                            if (dataGridView.Columns[columnsIndex].Visible)
                            {
                                DataGridViewCellPrint CellPrint = new DataGridViewCellPrint();
                                CellPrint.FormattedValue = dataGridView.Rows[rowIndex].Cells[columnsIndex].FormattedValue.ToString();
                                if (includeColumnText)
                                    CellPrint.RowIndex = rowIndex + 1;//假如包括列标题则从行号1开始
                                else
                                    CellPrint.RowIndex = rowIndex;
                                CellPrint.ColumnIndex = columnsIndex;
                                CellPrint.Font = dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.Font;
                                System.Drawing.Color TmpColor = System.Drawing.Color.Empty;
                                if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.BackColor)
                                    TmpColor = dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.BackColor;
                                else if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor)
                                    TmpColor = dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor;
                                else
                                    TmpColor = dataGridView.DefaultCellStyle.BackColor;
                                CellPrint.BackColor = TmpColor;
                                TmpColor = System.Drawing.Color.Empty;
                                if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.ForeColor)
                                    TmpColor = dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.ForeColor;
                                else if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].DefaultCellStyle.ForeColor)
                                    TmpColor = dataGridView.Rows[rowIndex].DefaultCellStyle.ForeColor;
                                else
                                    TmpColor = dataGridView.DefaultCellStyle.ForeColor;
                                CellPrint.ForeColor = TmpColor;
                                CellPrint.Width = dataGridView.Columns[columnsIndex].Width;
                                CellPrint.Height = dataGridView.Rows[rowIndex].Height;
                                CellPrintList.Add(CellPrint);
                            }
                        }
                    }
                }
                catch { throw; }

            }
        }
            class DataGridViewCellPrint
    {
        private string _FormattedValue = "";
        private int _RowIndex = -1;
        private int _ColumnIndex = -1;
        private System.Drawing.Color _ForeColor = System.Drawing.Color.Black;
        private System.Drawing.Color _BackColor = System.Drawing.Color.White;
        private int _Width = 100;
        private int _Height = 23;
        private System.Drawing.Font _Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        /// <summary>
        /// 获取或设置单元格的字体。
        /// </summary>
        public System.Drawing.Font Font
        {
            set { if (null != value) _Font = value; }
            get { return _Font; }
        }
        /// <summary>
        /// 获取为显示进行格式化的单元格的值。
        /// </summary>
        public string FormattedValue
        {
            set { _FormattedValue = value; }
            get { return _FormattedValue; }
        }
        /// <summary>
        /// 获取或设置列的当前宽度 （以像素为单位）。默认值为 100。
        /// </summary>
        public int Width
        {
            set { _Width = value; }
            get { return _Width; }
        }
        /// <summary>
        /// 获取或设置列标题行的高度（以像素为单位）。默认值为 23。
        /// </summary>
        public int Height
        {
            set { _Height = value; }
            get { return _Height; }
        }
        /// <summary>
        /// 获取或设置行号。
        /// </summary>
        public int RowIndex
        {
            set { _RowIndex = value; }
            get { return _RowIndex; }
        }
        /// <summary>
        /// 获取或设置列号。
        /// </summary>
        public int ColumnIndex
        {
            set { _ColumnIndex = value; }
            get { return _ColumnIndex; }
        }
        /// <summary>
        /// 获取或设置前景色。
        /// </summary>
        public System.Drawing.Color ForeColor
        {
            set { _ForeColor = value; }
            get { return _ForeColor; }
        }
        /// <summary>
        /// 获取或设置背景色。
        /// </summary>
        public System.Drawing.Color BackColor
        {
            set { _BackColor = value; }
            get { return _BackColor; }
        }
    }
}

