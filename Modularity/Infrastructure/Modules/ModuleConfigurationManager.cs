using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Modules
{
    /// <summary>
    /// 模块配置信息
    /// </summary>
    public class ModuleConfigurationManager : IModuleConfigurationManager
    {
        /// <summary>
        /// 记录模块信息的文件名
        /// </summary>
        public static readonly string ModulesFileName = "modules.json";

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleInfo> GetModules()
        {
            var modulesPath = Path.Combine(GlobalConfiguration.ContentRootPath!, ModulesFileName);
            using var reader = new StreamReader(modulesPath);
            string content = reader.ReadToEnd();

            ArgumentException.ThrowIfNullOrEmpty(content);

            return JsonConvert.DeserializeObject<IEnumerable<ModuleInfo>>(content)!;
        }
    }
}
