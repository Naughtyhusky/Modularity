using FluentMigrator.Runner;
using FreeRedis;
using Infrastructure.Bus;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using Infrastructure.Modules;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

GlobalConfiguration.WebRootPath = builder.Environment.WebRootPath;
GlobalConfiguration.ContentRootPath = builder.Environment.ContentRootPath;

#region 数据库，缓存配置
var mysqlConnStr = builder.Configuration.GetConnectionString("MysqlConnection");

builder.Services.AddDbContext<DiamondhuskyDbContext>(options =>
{
    options.UseMySql(mysqlConnStr, ServerVersion.AutoDetect(mysqlConnStr));

});

var redisConnStr = builder.Configuration.GetConnectionString("RedisConnection");

builder.Services.AddSingleton(provider =>
{
    return new RedisClient(redisConnStr);
});
#endregion

#region 加载模块信息
builder.Services.AddHttpContextAccessor();

builder.Services.AddModules(); //加载模块信息

var moduleAssemblys = GlobalConfiguration.Modules.Select(x => x.Assembly);

if (!moduleAssemblys.Any())
{
    ArgumentException.ThrowIfNullOrEmpty(nameof(moduleAssemblys));
}
#endregion


#region Migrator

builder.Services.AddFluentMigratorCore()
               .ConfigureRunner(configure => configure.AddMySql5()
               .WithGlobalConnectionString(mysqlConnStr)
               .ScanIn(moduleAssemblys.ToArray())
               .For.Migrations());

var runner = builder.Services.BuildServiceProvider().GetRequiredService<IMigrationRunner>();

runner.MigrateUp();

#endregion



builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>)); //注入仓储基类

builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<ICueerntUserProvider, CueerntUserProvider>();

builder.Services.AddMediatR((config) =>
{
    config.RegisterServicesFromAssemblies(moduleAssemblys.ToArray()!);
    config.NotificationPublisher = new ParallelNoWaitPublisher();
}); //扫描模块程序集，注册所有的CommandHandler和 EventHandler
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>)); //注册参数校验管道
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>)); //注册Mediator的事务管道


builder.Services.AddInterfaceInject(moduleAssemblys.ToArray()!);


//遍历模块的程序集，反射执行模块的IModuleInitializer接口的ConfigureServices方法，注入模块内需要注入的内容。
foreach (var module in GlobalConfiguration.Modules)
{
    var moduleInitializerType = module.Assembly!.GetTypes()
       .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));
    if ((moduleInitializerType != null) && (moduleInitializerType != typeof(IModuleInitializer)))
    {
        var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType)!;
        builder.Services.AddSingleton(typeof(IModuleInitializer), moduleInitializer);
        moduleInitializer.ConfigureServices(builder.Services);
    }
}

builder.Services.AddAutoMapper(moduleAssemblys.ToArray());

JsonSerializerSettings setting = new()
{
    Formatting = Formatting.None,
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
{
    //日期类型默认格式化处理
    setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
    setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    //空值处理
    setting.NullValueHandling = NullValueHandling.Include;

    return setting;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


var moduleInitializers = app.Services.GetServices<IModuleInitializer>();
foreach (var moduleInitializer in moduleInitializers)
{
    moduleInitializer.Configure(app, builder.Environment); //执行各个模块内部的配置
}

app.Run();
