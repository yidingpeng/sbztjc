using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
//using Microsoft.Office.Core;

namespace RW.PMS.Utils.Office
{

    //public class myExcel
    //{
    //    public Excel.Application app;
    //    public Excel._Workbook workbook;
    //    //private Excel.Sheets sheets; 
    //    //private Excel._Worksheet worksheet;

    //    //public myExcel(AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
    //    //{
    //    //    Object o = e.pDisp;
    //    //    Object oDocument = o.GetType().InvokeMember("Document", BindingFlags.GetProperty, null, o, null);
    //    //    app = (Excel.Application)o.GetType().InvokeMember("Application", BindingFlags.GetProperty, null, oDocument, null);

    //    //    workbook = app.Workbooks.get_Item(1);
    //    //}

    //    public myExcel(string FileName)
    //    {
    //        app = new Excel.Application();
    //        workbook = app.Workbooks.Add(FileName);

    //        //���ý�ֹ��������͸��ǵ�ѯ����ʾ��
    //        app.DisplayAlerts = false;
    //        app.AlertBeforeOverwriting = false;
    //    }

    //    public void closeworkbook()
    //    {
    //        workbook.Close(false, "", false);
    //    }

    //    public void WriteVal_with_borders(string TestVal, string rangeStr, Excel.Font font)
    //    {
    //        if (workbook == null)
    //        {
    //            return;
    //        }

    //        Excel.Sheets sheets = workbook.Worksheets;
    //        Excel._Worksheet worksheet = (Excel._Worksheet)sheets.get_Item(1);
    //        if (worksheet == null)
    //        {
    //            return;
    //        }

    //        Excel.Range range1;

    //        //worksheet.UsedRange;
    //        range1 = worksheet.get_Range(rangeStr, Missing.Value);

    //        if (range1 == null)
    //        {
    //            return;
    //        }
    //        range1.Font.Bold = font.Bold;
    //        range1.Font.Name = font.Name;
    //        range1.Font.Size = font.Size;
    //        range1.Font.Color = font.Color;
    //        range1.Font.Italic = font.Italic;
    //        //range1.Borders.LineStyle = 1;
    //        range1.Borders.Weight = 1;
    //        range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
    //        range1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

    //        range1.Value2 = TestVal;
    //    }

    //    public void WriteVal(string TestVal, string rangeStr, Excel.Font font)
    //    {
    //        if (workbook == null)
    //        {
    //            return;
    //        }

    //        Excel.Sheets sheets = workbook.Worksheets;
    //        Excel._Worksheet worksheet = (Excel._Worksheet)sheets.get_Item(1);
    //        if (worksheet == null)
    //        {
    //            return;
    //        }

    //        Excel.Range range1;

    //        //worksheet.UsedRange;
    //        range1 = worksheet.get_Range(rangeStr, Missing.Value);

    //        if (range1 == null)
    //        {
    //            return;
    //        }
    //        range1.Font.Bold = font.Bold;
    //        range1.Font.Name = font.Name;
    //        range1.Font.Size = font.Size;
    //        range1.Font.Color = font.Color;
    //        range1.Font.Italic = font.Italic;
    //        //range1.Borders.LineStyle = 1;
    //        //range1.Borders.Weight = font.bordersweight;
    //        range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
    //        range1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

    //        range1.Value2 = TestVal;
    //    }

    //    public string ReadVal(string rangeStr)
    //    {
    //        if (workbook == null)
    //        {
    //            return "";
    //        }

    //        Excel.Sheets sheets = workbook.Worksheets;
    //        Excel._Worksheet worksheet = (Excel._Worksheet)sheets.get_Item(1);
    //        if (worksheet == null)
    //        {
    //            return "";
    //        }

    //        Excel.Range range1;

    //        //worksheet.UsedRange;
    //        range1 = worksheet.get_Range(rangeStr, Missing.Value);

    //        if (range1 == null)
    //        {
    //            return "";
    //        }

    //        if (range1.Value2 == null)
    //            return "";
    //        else
    //            return range1.Value2.ToString();
    //    }

    //    public void InsertRows(int rowIndex)
    //    {
    //        if (workbook == null)
    //        {
    //            return;
    //        }

    //        Excel.Sheets sheets = workbook.Worksheets;
    //        Excel._Worksheet worksheet = (Excel._Worksheet)sheets.get_Item(1);
    //        if (worksheet == null)
    //        {
    //            return;
    //        }
    //        Excel.Range range = (Excel.Range)worksheet.Rows[rowIndex, Type.Missing];

    //        //object Range.Insert(object shift, object copyorigin);
    //        //shift: Variant���ͣ���ѡ��ָ����Ԫ��ĵ�����ʽ������Ϊ���� XlInsertShiftDirection ����֮һ��
    //        //xlShiftToRight �� xlShiftDown�����ʡ�Ըò�����Microsoft Excel ������������״ȷ��������ʽ��
    //        range.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Type.Missing);
    //        range = null;
    //    }

    //    public void InsertImage(string rangeStr, string PicturePath, float width, float height)
    //    {
    //        if (workbook == null)
    //        {
    //            return;
    //        }

    //        Excel.Sheets sheets = workbook.Worksheets;
    //        Excel._Worksheet worksheet = (Excel._Worksheet)sheets.get_Item(1);
    //        if (worksheet == null)
    //        {
    //            return;
    //        }

    //        Excel.Range range1;

    //        //worksheet.UsedRange;
    //        range1 = worksheet.get_Range(rangeStr, Missing.Value);
    //        range1.Select();
    //        worksheet.Shapes.AddPicture(PicturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Convert.ToSingle(range1.Left) + 3, Convert.ToSingle(range1.Top) + 3, width, height);
    //    }

    //    public bool print()
    //    {
    //        Object oMissing = System.Reflection.Missing.Value;     //ʵ��������ʱȱʡ���� 
    //        try
    //        {
    //            workbook.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }
    //    public bool save(string strSaveReportPath)
    //    {
    //        try
    //        {
    //            string strSaveFileName = System.Windows.Forms.Application.StartupPath + @"\���鱨��\" + strSaveReportPath;
    //            workbook.SaveAs(strSaveFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    //}
}
