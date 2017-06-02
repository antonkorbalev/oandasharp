using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace OandaConnect
{
    public static class Rfc3339DateTime
    {
        private static string[] formats = new string[0];
        private const string format = "yyyy-MM-dd'T'HH:mm:ss.fffK";

        public static string Rfc3339DateTimeFormat
        {
            get
            {
                return format;
            }
        }

        public static string[] Rfc3339DateTimePatterns
        {
            get
            {
                if (formats.Length > 0)
                {
                    return formats;
                }
                else
                {
                    formats = new string[11];

                    formats[0] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
                    formats[1] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffK";
                    formats[2] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffK";
                    formats[3] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffK";
                    formats[4] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK";
                    formats[5] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK";
                    formats[6] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fK";
                    formats[7] = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";

                    formats[8] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK"; // RoundtripDateTimePattern
                    formats[9] = DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern;
                    formats[10] = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;

                    return formats;
                }
            }
        }

        public static DateTime Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            DateTime result;
            if (Rfc3339DateTime.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                throw new FormatException(String.Format(null, "{0} is not a valid RFC 3339 string representation of a date and time.", s));
            }
        }

        public static string ToString(DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("utcDateTime");
            }

            return utcDateTime.ToString(Rfc3339DateTime.Rfc3339DateTimeFormat, DateTimeFormatInfo.InvariantInfo);
        }

        public static bool TryParse(string s, out DateTime result)
        {
            bool wasConverted = false;
            result = DateTime.MinValue;

            if (!String.IsNullOrEmpty(s))
            {
                DateTime parseResult;
                if (DateTime.TryParseExact(s, Rfc3339DateTime.Rfc3339DateTimePatterns, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal, out parseResult))
                {
                    result = DateTime.SpecifyKind(parseResult, DateTimeKind.Utc);
                    wasConverted = true;
                }
            }

            return wasConverted;
        }
    }
}
