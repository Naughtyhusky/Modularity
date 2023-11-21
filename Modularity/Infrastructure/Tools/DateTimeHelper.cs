using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tools
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取一个系统默认的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDefaultTime()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 判断是不是系统默认的时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsDefaultTime(DateTime dateTime)
        {
            return dateTime == DateTime.MinValue || dateTime == GetDefaultTime();
        }
    }
}
