using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fisbank.Cbs.CommonLib
{
    public enum DataType
    {
        Int = 1,
        Decimal = 2,
        Double = 3,
        String = 4,
        Date = 5,
        DateTime = 6
    }

    public class ExcelTitle
    {
        public string FieldName { get; set; }
        public object FieldValue { get; set; }
        public DataType DataType { get; set; }
        public ExcelTitle(string FieldName, object FieldValue, DataType DataType)
        {
            this.FieldName = FieldName;
            this.FieldValue = FieldValue;
            this.DataType = DataType;
        }
    }
}
