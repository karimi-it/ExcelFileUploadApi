
using ExcelFileUploadApi.Application.Interfaces;
using ExcelFileUploadApi.Application.Services;
using ExcelFileUploadApi.Domain.Interfaces;
using ExcelFileUploadApi.Infrastructure.Logging;
using ExcelFileUploadApi.Infrastructure.Persistence;
using ExcelFileUploadApi.Infrastructure.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ExcelFileUploadApi.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            try
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });

            // تنظیم حداکثر اندازه مجاز برای بدن درخواست در Kestrel
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
            });
            builder.Services.AddSingleton<ILoggingService, LoggingService>();
            // ثبت سرویس‌های مرتبط با Data Access
            builder.Services.AddScoped<IExcelRecordRepository, ExcelRecordRepository>();

            builder.Services.AddScoped<ApplicationDbContext>();

            // ثبت سرویس‌های Application
            builder.Services.AddScoped<IExcelService, ExcelService>();


            //ELK
            Log.Logger = new LoggerConfiguration()
     .WriteTo.Console()
     .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
     {
         IndexFormat = "excelFileUploader-{0:yyyy.MM.dd}",
         AutoRegisterTemplate = false // ثبت تمپلیت به‌صورت خودکار
     })
     .CreateLogger();
     
            Log.Warning("test log");
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.Run();
        }

    }

}
