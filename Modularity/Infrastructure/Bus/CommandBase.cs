using Infrastructure.Common;
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
    public abstract class CommandBase: IRequest<CommandResponse>
    {
        protected long Timestamp { get; init; }

        protected CommandBase()
        {
            Timestamp = DateTime.Now.ToUnixTimestamp();
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public virtual string SerializeCommand() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// 获取命令名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetCommandName() => this.GetGenericTypeName();

        /// <summary>
        /// 获取命令id
        /// </summary>
        /// <returns></returns>
        public virtual long GetCommandId() => this.Timestamp;
    }
}
