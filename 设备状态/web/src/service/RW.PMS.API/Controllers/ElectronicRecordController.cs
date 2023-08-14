using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Application.Contracts.Orders;
using RW.PMS.Application.NpoiHelper;
using Magicodes.ExporterAndImporter.Core;
using OfficeOpenXml;
using System.Drawing;
using System.IO;
using OfficeOpenXml.Style;

namespace RW.PMS.API.Controllers
{
    public class ElectronicRecordController : RWBaseController
    {
        private readonly IOdersService _ordersService;
        public ElectronicRecordController(IOdersService ordersService)
        {

            _ordersService = ordersService;
        }
        [HttpPost("ToExcel")]
        public IActionResult ToExcel([FromBody] List<OrdersDto> input)
        {
            //   var result = _ordersService.GetAllList(input);


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 设置许可证上下文
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1"); // 创建工作表
                worksheet.Cells["A1"].Value = "电子履历单"; // 设置表标题
                worksheet.Cells["A1"].Style.Font.Bold = true; // 设置标题加粗
                worksheet.Cells["A1"].Style.Font.Size = 16; // 设置标题字体大小
                worksheet.Cells["A1:C1"].Merge = true; // 合并单元格
                worksheet.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // 设置水平居中
                worksheet.Cells["A1:C1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // 设置垂直居中
                worksheet.Cells["A2"].Value = "表头1"; // 设置表头
                worksheet.Cells["B2"].Value = "表头2"; // 设置表头
                worksheet.Cells["C2"].Value = "表头3"; // 设置表头
                worksheet.Cells["A2:C2"].Style.Font.Bold = true; // 设置表头加粗
                worksheet.Cells["A2:C2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // 设置表头背景色
                worksheet.Cells["A2:C2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray); // 设置表头背景色
                worksheet.Cells["A2:C2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // 设置表头边框

                int row = 3;
                foreach (var item in input)
                {
                    worksheet.Cells[string.Format("A{0}", row)].Value = item.id; // 设置数据行
                    worksheet.Cells[string.Format("B{0}", row)].Value = item.Operator; // 设置数据行
                    worksheet.Cells[string.Format("C{0}", row)].Value = item.IsDeleted; // 设置数据行
                    worksheet.Cells[string.Format("A{0}:C{0}", row)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // 设置数据行边框
                    row++;
                }

                worksheet.Cells.AutoFitColumns(); // 自适应列宽
                byte[] excelData = package.GetAsByteArray(); // 将 Excel 数据转换为字节数组
                try
                {
                    // 生成Excel文件的代码
                    return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }


           
        }
    }
}
