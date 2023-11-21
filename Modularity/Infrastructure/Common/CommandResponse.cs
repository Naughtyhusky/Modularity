using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    /// <summary>
    /// Command操作响应类
    /// </summary>
    public class CommandResponse
    {
        protected CommandResponse() { }

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; protected set; }

        /// <summary>
        /// 具体的错误信息
        /// </summary>
        public string? Message { get; protected set; }

        /// <summary>
        /// 当前操作的执行结果 成功 or 失败
        /// </summary>
        public bool Status => Code == 0;

        /// <summary>
        /// 某些操作需要返回的数据
        /// </summary>
        public object? Data { get; protected set; }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="errode">错误码</param>
        /// <returns></returns>
        public static CommandResponse Fail(string errorMessage, int errode = -1) => new()
        {
            Message = errorMessage,
            Code = errode,
        };

        /// <summary>
        ///  操作成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <param name="message">提示</param>
        /// <returns></returns>
        public static CommandResponse Success(object data, string message = "ok") => new()
        {
            Message = message,
            Code = 0,
            Data = data
        };

        /// <summary>
        ///  操作成功
        /// </summary>
        /// <returns></returns>
        public static CommandResponse Success() => new()
        {
            Message = string.Empty,
            Code = 0
        };
    }
}
