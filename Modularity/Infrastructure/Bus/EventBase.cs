using Infrastructure.Extensions;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus
{
    public abstract class EventBase : INotification
    {
        protected EventBase()
        {
            Timestamp = DateTime.Now.ToUnixTimestamp();
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        protected long Timestamp { get; init; }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public string SerializeEvent() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// 获取事件名称
        /// </summary>
        /// <returns></returns>
        public string GetEventName() => this.GetGenericTypeName();

        /// <summary>
        /// 获取事件id
        /// </summary>
        /// <returns></returns>
        public virtual long GetEventId() => this.Timestamp;


    }
}
