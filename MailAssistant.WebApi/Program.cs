using MailAssistant.WebApi.Helpers;

namespace MailAssistant.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<AzureAISearch.Models.AppSettingsModels.AppSettings>(builder.Configuration);
            builder.Services.Configure<MailAssistant.Services.Models.AppSettingsModels.AppSettings>(builder.Configuration);
            builder.Services.AddControllers();
            ServiceRegistrar.ConfigureServices(builder.Services);

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
