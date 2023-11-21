using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Modules
{
    public interface IModuleConfigurationManager
    {
        /// <summary>
        /// 获取所有模块信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleInfo> GetModules();
    }
}
