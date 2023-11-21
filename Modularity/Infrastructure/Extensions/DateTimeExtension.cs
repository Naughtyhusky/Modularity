using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
     

        /// <summary>
        /// 转换成UNIX时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime date, DateTimestampFormat format = DateTimestampFormat.Millisecond)
        {
            return format switch
            {
                DateTimestampFormat.Second => (date.ToUniversalTime().Ticks - 621355968000000000) / 10000000,
                _ => (date.ToUniversalTime().Ticks - 621355968000000000) / 10000,
            };
        }


        /// <summary>
        /// 时间戳转换成datetime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp, DateTimestampFormat format = DateTimestampFormat.Millisecond)
        {
            string id = TimeZoneInfo.Local.Id;
            var start = new DateTime(1970, 1, 1) + TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            var nowDatatime = TimeZoneInfo.ConvertTime(start, TimeZoneInfo.FindSystemTimeZoneById(id));

            return format switch
            {
                DateTimestampFormat.Second => nowDatatime.AddSeconds(timestamp),
                _ => nowDatatime.AddMilliseconds(timestamp),
            };
        }

        /// <summary>
        /// 时间戳转换成datetime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int timestamp)
        {
            string id = TimeZoneInfo.Local.Id;
            var start = new DateTime(1970, 1, 1) + TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            var nowDatatime = TimeZoneInfo.ConvertTime(start, TimeZoneInfo.FindSystemTimeZoneById(id));

            return nowDatatime.AddSeconds(timestamp);

        }
    }
}
