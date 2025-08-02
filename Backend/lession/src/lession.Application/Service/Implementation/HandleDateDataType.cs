using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Application.Service.Implementation
{
    internal class HandleDateDataType
    {
        internal static bool ParseStringToDateOnly(string dateString, out DateOnly result)
        {
            // Define the culture with Vietnamese settings for date parsing
            var culture = new CultureInfo("vi-VN");
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            // Define acceptable date formats
            string[] formats = { "dd-MM-yyyy", "dd/MM/yyyy" };

            // Attempt to parse the date string
            return DateOnly.TryParseExact(dateString, formats, culture, DateTimeStyles.None, out result);
            
        }

        
    }
}
