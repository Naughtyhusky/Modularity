using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoInjectExtensions
    {
        /// <summary>
        /// 实现ITransientDependency、IScopedDependency、ISingletonDependency接口约束依赖的服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies">需要注入服务的所在程序集</param>
        /// <returns></returns>
        public static IServiceCollection AddInterfaceInject(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                var tempAssemblies = new List<Assembly>();
                var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
                tempAssemblies.Add(assembly);
                tempAssemblies.AddRange(assembly.GetReferencedAssemblies().Select(Assembly.Load));
                assemblies = [.. tempAssemblies];
            }
            assemblies = assemblies.Prepend(typeof(IScopedDependency).Assembly).ToArray();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var interfaces = type.GetInterfaces();

                    if (!type.IsInterface && interfaces != null && interfaces.Length > 0)
                    {
                        #region Transient services
                        if (interfaces.Contains(typeof(ITransientDependency)))
                        {
                            if (interfaces.Length == 1)
                                services.AddTransient(type);
                            else
                            {                              
                                foreach (var i in interfaces.ToList().Where(w => w.Name != nameof(ITransientDependency)))
                                {
                                    services.AddTransient(i, type);
                                }
                            
                            }
                            continue;
                        }
                        #endregion

                        #region Scoped services
                        if (interfaces.Contains(typeof(IScopedDependency)))
                        {
                            if (interfaces.Length == 1)
                                services.AddScoped(type);
                            else
                            {
                               
                                foreach (var i in interfaces.ToList().Where(w => w.Name != nameof(IScopedDependency)))
                                {
                                    services.AddScoped(i, type);
                                }
                            }
                            continue;
                        }
                        #endregion

                        #region Singleton services
                        if (interfaces.Contains(typeof(ISingletonDependency)))
                        {
                            if (interfaces.Length == 1)
                                services.AddSingleton(type);
                            else
                            {
                                foreach (var i in interfaces.ToList().Where(w => w.Name != nameof(ISingletonDependency)))
                                {
                                    services.AddSingleton(i, type);
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            return services;
        }

    }
}
