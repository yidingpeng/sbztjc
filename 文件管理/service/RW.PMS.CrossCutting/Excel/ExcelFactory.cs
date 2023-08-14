using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.CrossCutting.Excel
{
    public static class ExcelFactory
    {
        public static IExcel CreateExcel(string filename)
        {
            IExcel excel = new NPOIExcel(filename);
            return excel;
        }
    }
}
