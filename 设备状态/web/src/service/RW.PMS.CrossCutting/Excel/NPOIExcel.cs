using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.CrossCutting.Excel
{
    public class NPOIExcel : IExcel
    {

        IWorkbook workbook;

        public NPOIExcel(string filename)
        {
            this.Filename = filename;
        }

        void InitWorkbook()
        {
            if (workbook == null)
            {
                FileInfo f = new FileInfo(Filename);
                if (f.Exists)
                {
                    using (var stream = new FileStream(Filename, FileMode.Open))
                    {
                        if (Filename.IndexOf(".xlsx") > 0)
                            workbook = new XSSFWorkbook(stream);
                        else
                            workbook = new HSSFWorkbook(stream);
                    }
                }
                else
                {
                    if (Filename.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook();
                    else
                        workbook = new HSSFWorkbook();
                }
            }

        }

        void InitWorkbook(string filename, bool isNew = false)
        {
            if (!isNew)
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    if (filename.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    else
                        workbook = new HSSFWorkbook(fs);
                }
            }
            else
            {
                if (filename.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook();
                else
                    workbook = new HSSFWorkbook();
            }
        }

        public string Filename { get; set; }

        public NPOIExcel(Stream stream, bool xls = true)
        {
            if (xls)
                workbook = new XSSFWorkbook(stream);
            else
                workbook = new HSSFWorkbook(stream);
        }

        byte[] Save()
        {
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            var data = ms.ToArray();
            return data;
        }

        ISheet GetSheet(string tableName)
        {
            var sheet = workbook.GetSheet(tableName);
            if (sheet == null)
                sheet = workbook.CreateSheet(tableName);
            return sheet;
        }

        IRow GetRow(ISheet sheet, int rowIndex)
        {
            var row = sheet.GetRow(rowIndex);
            if (row == null)
                row = sheet.CreateRow(rowIndex);
            return row;
        }

        public byte[] FillDataSet(DataSet ds, bool hasHeader)
        {
            InitWorkbook();

            foreach (DataTable table in ds.Tables)
            {
                var sheet = GetSheet(table.TableName);
                //设置表头
                if (hasHeader)
                {
                    var headerRow = GetRow(sheet, 0);
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        var cell = headerRow.CreateCell(i);
                        cell.SetCellValue(table.Columns[i].ColumnName);
                    }
                }
                //设置表内容
                for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                {
                    var row = sheet.CreateRow(rowIndex + (hasHeader ? 1 : 0));

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        var cell = row.CreateCell(i);
                        cell.SetCellValue(Convert.ToString(table.Rows[rowIndex][i]));
                    }
                }
            }

            return this.Save();

        }

        public object Read(string cellName)
        {
            throw new NotImplementedException();
        }

        public DataSet ToDataSet(bool hasHeader)
        {
            InitWorkbook();
            var count = workbook.NumberOfSheets;

            DataSet ds = new DataSet();

            for (int i = 0; i < count; i++)
            {
                DataTable dt = new DataTable();
                ds.Tables.Add(dt);
                var sheet = workbook.GetSheetAt(i);
                var rowCount = sheet.PhysicalNumberOfRows;
                dt.TableName = sheet.SheetName;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var row = sheet.GetRow(rowIndex);
                    if (rowIndex == 0 && hasHeader)//列名
                    {
                        foreach (var cell in row.Cells)
                        {
                            dt.Columns.Add(cell.StringCellValue);
                        }
                        continue;
                    }

                    if (dt.Columns.Count != row.Cells.Count)
                    {
                        for (int j = dt.Columns.Count; j < row.Cells.Count; j++)
                        {
                            dt.Columns.Add("NoName" + j);
                        }
                    }
                    var dtRow = dt.NewRow();
                    //行值
                    for (int colIndex = 0; colIndex < row.Cells.Count; colIndex++)
                    {
                        var cell = row.Cells[colIndex];
                        dtRow[colIndex] = GetCellValue(cell);
                    }
                    dt.Rows.Add(dtRow);
                }
            }
            return ds;
        }

        object GetCellValue(ICell cell)
        {
            if (cell.CellType == CellType.Numeric)
                return cell.NumericCellValue;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                case CellType.Blank:
                case CellType.Unknown:
                case CellType.String:
                default:
                    return cell.StringCellValue;
            }
        }

        public void Write(string cellName, object value)
        {
            throw new NotImplementedException();
        }
    }
}
