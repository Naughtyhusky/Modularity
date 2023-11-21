using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Modules
{
    public class GlobalConfiguration
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();

        /// <summary>
        /// 语言信息，预留
        /// </summary>
        public static string? DefaultCulture => "zh-cn";

        /// <summary>
        /// 
        /// </summary>
        public static string? WebRootPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string? ContentRootPath { get; set; }
    }
}
