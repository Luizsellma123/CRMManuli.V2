using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class DateFormatConverterClass : IsoDateTimeConverter
    {
        public DateFormatConverterClass(string format)
        {
            DateTimeFormat = format;
        }
    }
}