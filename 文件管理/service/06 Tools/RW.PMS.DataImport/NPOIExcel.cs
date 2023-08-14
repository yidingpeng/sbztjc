using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.DataImport
{
    //NPOI方式
    //NPOI 是 POI 项目的 .NET 版本。POI是一个开源的Java读写Excel、WORD等微软OLE2组件文档的项目。使用 NPOI 你就可以在没有安装 Office 或者相应环境的机器上对 WORD/EXCEL 文档进行读写。
    //优点：读取Excel速度较快，读取方式操作灵活性
    //缺点：需要下载相应的插件并添加到系统引用当中。
    /// <summary>
    /// 使用基于NPOI读写Excel的帮助类
    /// </summary>
    public class NPOIExcel
    {
        IWorkbook workbook = null;
        //ISheet sheet = null;

        private string filePath = null;

        public int SheetIndex { get; set; }

        /// <summary>
        /// 创建NPOI的实例
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        public NPOIExcel(string filePath, int sheetIndex = 0)
        {
            this.filePath = filePath;
            this.SheetIndex = sheetIndex;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (filePath.LastIndexOf(".xlsx") == filePath.Length - 5)
                {
                    var xssfWorkbook = new XSSFWorkbook(fs);
                    workbook = xssfWorkbook;

                }
                else
                {
                    var hssfworkbook = new HSSFWorkbook(fs);
                    workbook = hssfworkbook;
                }
            }
        }

        ISheet GetSheet(int index)
        {
            var sheet = workbook.GetSheetAt(index);
            sheet.ForceFormulaRecalculation = true;
            return sheet;
        }

        /// <summary>
        /// 获取指定的工作表
        /// </summary>
        ISheet GetSheet(string name) => workbook.GetSheet(name);

        /// <summary>
        /// 读取指定行列的单元格内容，并返回
        /// </summary>
        public string Read(int sheetIndex, int row, int column) => GetCellValue(GetCell(sheetIndex, row, column)) + "";

        public string Read(int row, int column) => Read(SheetIndex, row, column);

        /// <summary>
        /// 读取指定行列的单元格内容，并返回
        /// </summary>
        public string Read(int sheetIndex, string cellName)
        {
            int rows, cols;
            GetExcelRowIndex(cellName, out rows, out cols);
            return Read(sheetIndex, rows, cols);
        }

        public string Read(string cellName) => Read(SheetIndex, cellName);

        /// <summary>
        /// 将value写入到指定行列单元格
        /// </summary>
        public void Write(int sheetIndex, int row, int column, object value)
        {
            if (value is string)
            {
                GetCell(sheetIndex, row, column).SetCellValue(value + "");
            }
            else if (value is bool)
            {
                GetCell(sheetIndex, row, column).SetCellValue(Convert.ToBoolean(value));
            }
            else
            {
                GetCell(sheetIndex, row, column).SetCellValue(Convert.ToDouble(value));
            }
            //sheet.GetRow(row).GetCell(column).SetCellValue(value + "");
        }

        public void Write(int row, int column, object value) => Write(SheetIndex, row, column, value);


        /// <summary>
        /// 将value写入到指定的单元格（字母型如A11，不能是命名）
        /// </summary>
        public void Write(int sheetIndex, string cellName, object value)
        {
            int rows, cols;
            GetExcelRowIndex(cellName, out rows, out cols);
            Write(sheetIndex, rows, cols, value);
        }

        public void Write(string cellName, object value) => Write(SheetIndex, cellName, value);

        public void GetExcelRowIndex(string cellname, out int row, out int column)
        {
            //正常：A5，AA5
            //名称：test
            //RefersToFormula: "Sheet1!$B$1" 
            //全列：Sheet2!$E:$E       表示 第E列
            //全行：Sheet2!$6:$6       表示 第六行
            //多列：Sheet2!$B$3:$C$3   表示 B3、C3
            //hssfworkbook.GetName("test")

            string cname = "";

            var name = workbook.GetName(cellname);
            if (name != null)
            {
                var refName = name.RefersToFormula;
                cname = refName.Substring(refName.IndexOf('!') + 1).Replace("$", "").Split(':')[0];
                //todo:暂仅支持获取单个
                cname = cname.ToUpper();
            }
            else
                cname = cellname.ToUpper();
            row = 0;
            column = 0;
            for (int i = 0; i < cname.Length; i++)
            {
                if (cname[i] >= 'A' && cname[i] <= 'Z')
                    column += (cname[i] + 26 * i) - 'A';
                else
                {
                    row = Convert.ToInt32(cname.Substring(i)) - 1;
                    break;
                }
            }
        }

        ICell GetCell(int sheetIndex, int rowIndex, int columnIndex)
        {
            var sheet = GetSheet(sheetIndex);
            IRow row = sheet.GetRow(rowIndex);
            if (row == null) row = sheet.CreateRow(rowIndex);
            ICell cell = row.GetCell(columnIndex);
            if (cell == null)
                cell = row.CreateCell(columnIndex);
            return cell;
        }

        /// <summary>
        /// 保存工作簿，将所有的修改写入到文件中
        /// </summary>
        public void Save()
        {
            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll();//强制刷新所有公式
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook.Write(fs);
            }
        }

        /// <summary>
        /// Excel转换成DataTable（.xls）
        /// </summary>
        public static DataTable ExcelToDataTable(string filePath, int sheetIndex, int columnRow = -1, int rowFirst = 0)
        {
            var dt = new DataTable();
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //var hssfworkbook = new HSSFWorkbook(file);
                ISheet sheet = null;

                if (filePath.LastIndexOf(".xlsx") == filePath.Length - 5)
                {
                    var xssfWorkbook = new XSSFWorkbook(file);
                    sheet = xssfWorkbook.GetSheetAt(sheetIndex);
                }
                else
                {
                    var hssfworkbook = new HSSFWorkbook(file);
                    sheet = hssfworkbook.GetSheetAt(sheetIndex);
                }

                //可能没有列，需要增加列定义
                //for (var j = 0; j < 22 && columnRow == -1; j++)
                //{
                //    dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                //}


                var rows = sheet.GetRowEnumerator();
                int rowIndex = 0;
                while (rows.MoveNext())
                {
                    var row = (IRow)rows.Current;
                    var dr = dt.NewRow();
                    List<object> data = new List<object>();

                    for (var i = 0; i < row.LastCellNum; i++)
                    {
                        var cell = row.GetCell(i);
                        data.Add(cell == null ? null : GetCellValue(cell));
                        //dr[i] = cell == null ? null : GetCellValue(cell);

                    }
                    bool notEmpty = false;
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i] != null) notEmpty = true;
                    }
                    if (!notEmpty) break;

                    if (rowIndex == 0 && columnRow == rowIndex)//首行为列名时
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            dt.Columns.Add(Convert.ToString(data[i]));
                        }
                    }
                    else if (rowIndex >= rowFirst)
                    {
                        if (dt.Columns.Count < data.Count)
                        {
                            for (int i = dt.Columns.Count; i < data.Count; i++)
                                dt.Columns.Add();
                        }
                        dt.Rows.Add(data.ToArray());
                    }
                    rowIndex++;

                }
            }
            return dt;
        }

        /// <summary>
        /// Excel转换成DataSet（.xlsx/.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath, out string strMsg)
        {
            strMsg = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string fileType = Path.GetExtension(filePath).ToLower();
            string fileName = Path.GetFileName(filePath).ToLower();
            try
            {
                ISheet sheet = null;
                int sheetNumber = 0;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = null;
                if (fileType == ".xlsx")
                {
                    // 2007版本
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileType == ".xls")
                {
                    // 2003版本
                    workbook = new HSSFWorkbook(fs);
                }

                sheetNumber = workbook.NumberOfSheets;
                for (int i = 0; i < sheetNumber; i++)
                {
                    string sheetName = workbook.GetSheetName(i);
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet != null)
                    {
                        dt = GetSheetDataTable(sheet, out strMsg);
                        if (dt != null)
                        {
                            dt.TableName = sheetName.Trim();
                            ds.Tables.Add(dt);
                        }
                        else
                        {
                            throw new Exception("Sheet数据获取失败，原因：" + strMsg);
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取sheet表对应的DataTable
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static DataTable GetSheetDataTable(ISheet sheet, out string strMsg)
        {
            strMsg = "";
            DataTable dt = new DataTable();
            string sheetName = sheet.SheetName;
            int startIndex = 0;// sheet.FirstRowNum;
            int lastIndex = sheet.LastRowNum;
            //最大列数
            int cellCount = 0;
            IRow maxRow = sheet.GetRow(0);
            if (maxRow == null) return dt;
            for (int i = startIndex; i <= lastIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null && cellCount < row.LastCellNum)
                {
                    cellCount = row.LastCellNum;
                    maxRow = row;
                }
            }
            //列名设置
            try
            {
                for (int i = 0; i < maxRow.LastCellNum; i++)//maxRow.FirstCellNum
                {
                    dt.Columns.Add(Convert.ToChar(((int)'A') + i).ToString());
                    //DataColumn column = new DataColumn("Column" + (i + 1).ToString());
                    //dt.Columns.Add(column);
                }
            }
            catch
            {
                strMsg = "工作表" + sheetName + "中无数据";
                return null;
            }
            //数据填充
            for (int i = startIndex; i <= lastIndex; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow drNew = dt.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                    {
                        if (row.GetCell(j) != null)
                        {
                            ICell cell = row.GetCell(j);
                            drNew[j] = GetCellValue(cell);
                        }
                    }
                }
                dt.Rows.Add(drNew);
            }
            return dt;
        }

        public static void DataTableToExcel(string filePath, DataTable dt, bool containsColumns = true)
        {
            using (var file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                IWorkbook book = null;
                ISheet sheet = null;

                if (filePath.LastIndexOf(".xlsx") == filePath.Length - 5)
                {
                    var xssfWorkbook = new XSSFWorkbook();
                    sheet = xssfWorkbook.CreateSheet();
                    book = xssfWorkbook;
                }
                else
                {
                    var hssfworkbook = new HSSFWorkbook();
                    sheet = hssfworkbook.CreateSheet();
                    //sheet = hssfworkbook.GetSheetAt(sheetIndex);
                    book = hssfworkbook;
                }

                int rowIndex = 0;
                if (containsColumns)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        var row = sheet.GetRow(rowIndex);
                        if (row == null) row = sheet.CreateRow(0);
                        var cell = row.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    rowIndex++;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = sheet.GetRow(rowIndex + i);
                    if (row == null) row = sheet.CreateRow(rowIndex + i);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var cell = row.CreateCell(j);
                        cell.SetCellValue(Convert.ToString(dt.Rows[i][j]));
                    }
                }

                book.Write(file);
            }

        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetCellValue(ICell cell)
        {
            object value = null;
            switch (cell.CellType)
            {
                case CellType.Boolean:
                    value = cell.BooleanCellValue;
                    break;
                case CellType.Blank:
                    value = null;
                    break;
                //case CellType.Boolean:
                //    dr[i] = cell.BooleanCellValue;
                //    break;
                case CellType.Numeric:
                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                        value = cell.DateCellValue;
                    else
                        value = cell.NumericCellValue;
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
                //case CellType.Error:
                //    dr[i] = cell.ErrorCellValue;
                //    break;
                case CellType.Formula:
                    //cell.CachedFormulaResultType == CellType
                    try
                    {
                        if (cell.CellStyle.DataFormat == 187)
                            value = cell.DateCellValue;
                        else if (cell.CellStyle.DataFormat == 178)
                            value = cell.NumericCellValue;
                        else if (cell.CellStyle.DataFormat == 0 || cell.CellStyle.DataFormat == 49 || cell.CellStyle.DataFormat == 177)
                        {
                            cell.SetCellType(CellType.String);
                            try
                            {
                                value = cell.StringCellValue;
                            }
                            catch
                            {
                                value = cell.NumericCellValue;
                            }
                        }
                        else if (HSSFDateUtil.IsCellDateFormatted(cell))//196
                            value = cell.DateCellValue;
                        //else if (cell.CellStyle.DataFormat == 10)//百分比
                        //    dr[i] = Math.Round(cell.NumericCellValue * 100, 2) + "%";
                        else
                            value = cell.NumericCellValue;
                    }
                    catch
                    {
                        value = cell.StringCellValue;
                    }
                    break;
                default:
                    value = "=" + cell.CellFormula;
                    break;
            }
            return value;
        }

        /// <summary>
        /// 在指定单元格插入图片
        /// </summary>
        public void InsertImage(int sheetIndex, int row1, int col1, int row2, int col2, string picPath)
        {
            var data = File.ReadAllBytes(picPath);
            var pictureIdx = workbook.AddPicture(data, PictureType.PNG);//先将图片加入到资源中
            var sheet = GetSheet(sheetIndex);
            //HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            //// 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
            //HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, col1, row1, col2, row2);
            ////把图片插到相应的位置
            //HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);


            /*以下代码可以兼容任意版本的excel*/
            var s = sheet.CreateDrawingPatriarch();
            var an = s.CreateAnchor(0, 0, 0, 0, col1, row1, col2, row2);
            var pic = s.CreatePicture(an, pictureIdx);
        }
        /// <summary>
        /// 在指定单元格插入图片
        /// </summary>
        public void InsertImage(int row1, int col1, int row2, int col2, string picPath) => InsertImage(SheetIndex, row1, col1, row2, col2, picPath);
        /// <summary>
        /// 在指定单元格插入图片
        /// </summary>
        public void InsertImage(int row, int column, string path) => InsertImage(SheetIndex, row, column, row + 1, column + 1, path);
        public void InsertImage(int sheetIndes, int row, int column, string path) => InsertImage(sheetIndes, row, column, row + 1, column + 1, path);
        /// <summary>
        /// 在指定单元格插入图片
        /// </summary>
        public void InsertImage(int sheetIndex, string cellName, string toCellName, string path)
        {
            int row1, column1;
            int row2, column2;
            GetExcelRowIndex(cellName, out row1, out column1);
            if (toCellName != null)
            {
                GetExcelRowIndex(toCellName, out row2, out column2);
            }
            else
            {
                row2 = row1 + 1;
                column2 = column1 + 1;
                var cell = GetCell(sheetIndex, row1, column1);
                if (cell.IsMergedCell)
                {
                    var sheet = GetSheet(sheetIndex);
                    for (int i = 0; i < sheet.NumMergedRegions; i++)
                    {
                        var range = sheet.GetMergedRegion(i);
                        if (range.FirstRow == row1 && range.FirstColumn == column1)
                        {
                            row2 = range.LastRow + 1;
                            column2 = range.LastColumn + 1;
                            break;
                        }
                    }
                }
            }

            //如A5-B5，应该是2个单元格，所以目标单元格是B6
            InsertImage(sheetIndex, row1, column1, row2, column2, path);
        }
        public void InsertImage(string cellName, string toCellName, string path) => InsertImage(SheetIndex, cellName, toCellName, path);

        /// <summary>
        /// 在指定单元格插入图片
        /// </summary>
        public void InsertImage(string cellName, string path) => InsertImage(SheetIndex, cellName, null, path);
        /// <summary>
        /// 清除所有图片
        /// </summary>
        public void ClearImage() => workbook.GetAllPictures().Clear();

        public void HiddenRow(int sheetIndex, int rowIndex) => RowStatus(sheetIndex, rowIndex, rowIndex, true);

        public void HiddenRow(int rowIndex) => HiddenRow(SheetIndex, rowIndex);

        public void ShowRow(int sheetIndex, int rowIndex) => RowStatus(sheetIndex, rowIndex, rowIndex, false);

        public void ShowRow(int rowIndex) => ShowRow(SheetIndex, rowIndex);
        public void HiddenColumn(int sheetIndex, int index) => ColumnStatus(sheetIndex, index, index, true);
        public void HiddenColumn(int index) => HiddenColumn(SheetIndex, index);
        public void ShowColumn(int sheetIndex, int index) => ColumnStatus(sheetIndex, index, index, false);
        public void ShowColumn(int index) => ColumnStatus(SheetIndex, index, index, false);
        public void RowStatus(int sheetIndex, int start, int end, bool hidden)
        {
            var sheet = GetSheet(sheetIndex);
            for (int i = start; i <= end; i++)
            {
                var row = sheet.GetRow(i);
                row.ZeroHeight = hidden;
            }
        }
        public void RowStatus(int start, int end, bool hidden) => RowStatus(SheetIndex, start, end, hidden);
        public void ColumnStatus(int sheetIndex, int start, int end, bool hidden)
        {
            var sheet = GetSheet(sheetIndex);
            for (int i = start; i <= end; i++)
            {
                sheet.GetColumnStyle(i).IsHidden = hidden;
            }
        }
        public void ColumnStatus(int start, int end, bool hidden) => ColumnStatus(SheetIndex, start, end, hidden);

        /// <summary>
        /// 合并单元格
        /// </summary>
        public void Merge(int sheetIndex, string cell1, string cell2 = null)
        {
            var sheet = GetSheet(sheetIndex);
            int row1, col1;
            GetExcelRowIndex(cell1, out row1, out col1);
            int row2, col2;
            GetExcelRowIndex(cell2, out row2, out col2);
            sheet.AddMergedRegion(new CellRangeAddress(row1, row2, col1, col2));
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        public void Merge(string cell1, string cell2 = null) => Merge(SheetIndex, cell1, cell2);

    }
}
