using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Modules
{
    public class ModuleInfo
    {
        /// <summary>
        /// 模块ID(程序集名称)
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public required string Version { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        [JsonIgnore]
        public Assembly? Assembly { get; set; }
    }
}
