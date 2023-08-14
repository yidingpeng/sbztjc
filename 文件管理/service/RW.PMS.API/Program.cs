using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Logging;
using FreeSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RW.PMS.API.Middleware;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.CrossCutting.IOC.Modules;
using RW.PMS.CrossCutting.Json.Converter;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.Repositories;
using RW.PMS.Infrastructure.Repositories;
using Serilog;
using System.Reflection;
using System.Text;
using Common.Logging.Simple;
using Quartz;
using Quartz.Impl;
using RW.PMS.API.QuartzService;
using Autofac.Extras.Quartz;
using RW.PMS.Infrastructure.BackgroundJobs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 国际化

builder.Services.AddLocalization();

#endregion

#region Json序列化

builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
});

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(Assembly.Load("RW.PMS.Application.Contracts"));

#endregion

#region 中间件

#endregion

#region 跨域

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Any", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

#endregion

#region 依赖注入

builder.Host.ConfigureContainer<ContainerBuilder>(build =>
{
    build.RegisterModule(new ServiceModule());
    build.RegisterModule(new RepositoryModule());
    build.RegisterModule(new CrossCuttingModule());
    build.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
    //配置quartz.net依赖注入
    build.RegisterModule(new QuartzAutofacFactoryModule());
    build.RegisterModule(new QuartzAutofacJobsModule(typeof(TestJobweek1).Assembly));
    build.RegisterModule(new QuartzAutofacJobsModule(typeof(TestJobmonth2).Assembly));
    build.RegisterModule(new QuartzAutofacJobsModule(typeof(TestJobYear3).Assembly));
    var freeSql = new FreeSqlBuilder()
        .UseConnectionString(DataType.MySql, builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText))
        .UseAutoSyncStructure(true)
        .Build();
    freeSql.GlobalFilter.Apply<ISoftDelete>("IsDeleted", t => t.IsDeleted == false);

    build.RegisterInstance(freeSql).SingleInstance();
    
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<UnitOfWorkManager>();

#endregion

#region JWT认证

builder.Services.AddAuthentication(opt => { opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(
    opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SigningKey"])),
            TokenDecryptionKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecurityKey"])),
            //是否验证Issuer
            ValidateIssuer = true,
            //是否验证Audience
            ValidateAudience = true,
            //是否验证失效时间
            ValidateLifetime = true,
            //是否验证SecurityKey
            ValidateIssuerSigningKey = true,
            //允许的服务器时间偏移量
            ClockSkew = TimeSpan.Zero
        };
        opt.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                var payload = JsonConvert.SerializeObject(ResponseDto.Error(401, "登录信息已过期"));
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;

                //context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.WriteAsync(payload);

                return Task.FromResult(0);
            }
        };
    });

#endregion

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RW.PMS.API", Version = "v1" });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Value: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});

#endregion

#region 中间件

builder.Services.AddSingleton<ExceptionMiddleWare>();

#endregion

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .UseSerilog((context, logger) => { logger.ReadFrom.Configuration(context.Configuration); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RW.PMS.API v1"));
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("zh")
});
var config = app.Services.GetRequiredService<IConfiguration>();
var weekcofig = config["datacounttime:weekcron"];
var monthcofig = config["datacounttime:monthcron"];
var yearcofig = config["datacounttime:yearcron"];
var scheduler = app.Services.GetRequiredService<IScheduler>();
scheduler.Start();

//创建第一个任务
IJobDetail job = JobBuilder.Create<TestJobweek1>().Build();
ITrigger trigger = TriggerBuilder.Create()
     .WithCronSchedule(weekcofig)
      .Build();


// 创建第二个任务
IJobDetail job2 = JobBuilder.Create<TestJobmonth2>().Build();
ITrigger trigger2 = TriggerBuilder.Create()
     .WithCronSchedule(monthcofig)
    .Build();

// 创建第三个任务
IJobDetail job3 = JobBuilder.Create<TestJobYear3>().Build();
ITrigger trigger3 = TriggerBuilder.Create()
     .WithCronSchedule(yearcofig)
    .Build();
scheduler.ScheduleJob(job, trigger);
scheduler.ScheduleJob(job2, trigger2);
scheduler.ScheduleJob(job3, trigger3);
app.UseExceptionMiddleWare();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("Any");

app.MapControllers();

app.Run();