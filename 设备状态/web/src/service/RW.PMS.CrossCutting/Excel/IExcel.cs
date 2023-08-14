using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.CrossCutting.Excel
{
    public interface IExcel
    {

        object Read(string cellName);

        void Write(string cellName, object value);

        DataSet ToDataSet(bool hasHeader = true);

        byte[] FillDataSet(DataSet ds, bool hasHeader = true);
    }
}
