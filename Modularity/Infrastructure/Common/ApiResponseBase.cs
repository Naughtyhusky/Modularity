using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    /// <summary>
    /// Api响应基类
    /// </summary>
    public class ApiResponseBase
    {
        /// <summary>
        /// 请求响应码，0 为正常
        /// </summary>
        public virtual int Code { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public virtual bool Status => Code == 0;

        /// <summary>
        /// 响应信息
        /// </summary>
        public virtual string? Message { get; set; }

        /// <summary>
        /// 数据，派生类自行重写
        /// </summary>
        public virtual object? Data { get; set; }


        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">返回信息</param>
        /// <param name="data">业务数据</param>
        /// <returns></returns>
        public static ApiResponseBase Success(string message = "ok", object? data = null) => new()
        {
            Code = 0,
            Message = message,
            Data = data ?? new object()
        };
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">返回信息</param>
        /// <param name="data">业务数据</param>
        /// <returns></returns>
        public static ApiResponseBase Fail(int code, string? message, object? data = null) => new()
        {
            Code = code,
            Message = message,
            Data = data ?? new object()
        };
    
        /// <summary>
        /// 实例一个响应结果
        /// </summary>
        /// <typeparam name="T">业务枚举</typeparam>
        /// <param name="info">业务操作提示</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponseBase Instance<T>(T info, object? data = null) where T : Enum
        {
            var message = info.ToString();

            return new ApiResponseBase
            {
                Code = (int)Enum.Parse(typeof(T), message),
                Message = message,
                Data = data ?? new object()
            };
        }     
    }


    /// <summary>
    /// Api响应基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponseBase<T> : ApiResponseBase
    {

        /// <summary>
        /// 业务数据
        /// </summary>
        public new T? Data { get; set; }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponseBase<T> Success(T data, string? message = "ok") => new()
        {
            Code = 0,
            Message = message,
            Data = data
        };

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponseBase<T> Fail(int code, string? message, T? data = default) => new()
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
        public static ApiResponseBase<T> Instance<TEnum>(TEnum info, T? data = default) where TEnum : Enum
        {
            var message = info.ToString();
            return new ApiResponseBase<T>
            {
                Code = (int)Enum.Parse(typeof(TEnum), message),
                Message = message,
                Data = data
            };
        }
    }
}
