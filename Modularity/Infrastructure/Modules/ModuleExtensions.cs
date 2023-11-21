using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Modules
{
    public static class ModuleExtensions
    {
        private static readonly IModuleConfigurationManager _modulesConfig = new ModuleConfigurationManager();

        /// <summary>
        /// 加载模块信息
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            foreach (var module in _modulesConfig.GetModules())
            {

                module.Assembly = Assembly.Load(new AssemblyName(module.Id));

                GlobalConfiguration.Modules.Add(module);
            }

            return services;
        }
    }
}
