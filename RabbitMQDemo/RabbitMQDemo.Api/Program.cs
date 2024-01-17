using RabbitMQDemo.Application.DI;
using RabbitMQDemo.Domain.DI;
using RabbitMQDemo.Infra.DI;
using Serilog;

namespace RabbitMQDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDomain(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfra(builder.Configuration);

            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSerilogRequestLogging();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
