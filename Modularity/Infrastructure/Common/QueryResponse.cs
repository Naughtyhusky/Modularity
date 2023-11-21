using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    /// <summary>
    /// 查询操作返回值对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResponse<T>
    {
        protected QueryResponse() { }

        /// <summary>
        /// 请求响应码，0 为正常
        /// </summary>
        public virtual int Code { get; protected set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public virtual bool Status => Code == 0;

        /// <summary>
        /// 响应信息
        /// </summary>
        public virtual string? Message { get; protected set; }

        /// <summary>
        /// 业务数据
        /// </summary>
        public T? Data { get; protected set; }

    
        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static QueryResponse<T> Instance(int code=0, string message="ok", T? data = default) => new()
        {
            Code = code,
            Message = message,
            Data = data
        };


        /// <summary>
        /// 实例一个响应结果
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="info"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static QueryResponse<T> Instance<TEnum>(TEnum info, T? data = default) where TEnum : Enum
        {
            var message = info.ToString();
            return new QueryResponse<T>
            {
                Code = (int)Enum.Parse(typeof(TEnum), message),
                Message = message,
                Data = data
            };
        }
    }
}
