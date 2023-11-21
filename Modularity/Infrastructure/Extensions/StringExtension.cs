using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtension
    {
      
        public static string Md5(this string str, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            var bytes = MD5.HashData(encoding.GetBytes(str));
            var strResult = BitConverter.ToString(bytes);
            return strResult.Replace("-", "");           
        }

        /// <summary>
        /// 十六进制MD5
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Md5By16Binary(this string str, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            var bytes = MD5.HashData(encoding.GetBytes(str));
            StringBuilder builder = new();
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串 
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static bool IsLetterDigit(this string str)
        {
            var pattern = "^[a-z0-9A-Z]+$";

            return Regex.IsMatch(str, pattern);
        }
    }
}
