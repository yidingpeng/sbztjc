using Autofac;
using Autofac.Extensions.DependencyInjection;
using FreeSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RW.PMS.CMMI.API", Version = "v1" });
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
