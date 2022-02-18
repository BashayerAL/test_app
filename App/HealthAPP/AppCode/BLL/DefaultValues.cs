using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace HealthAPP.AppCode.BLL
{
    public class DefaultValues
    {
        public static DateTime GetDateTimeMinValue()
        {
            DateTime MinValue = (DateTime)SqlDateTime.MinValue;
            MinValue.AddYears(30);
            return (MinValue);
        }
    }
}