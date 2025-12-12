using Ecom.Api.Middleware;
using Ecom.Infrastructture;
namespace Ecom.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(op =>
            {
                op.AddPolicy("CORSPolicy", builder =>
                {
                    builder
                    .AllowAnyHeader() // Allow all request headers
                    .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
                    .AllowCredentials() // Allow cookies/authentication tokens to be sent
                    .WithOrigins("https://localhost:4200", "http://localhost:4200"); // Allow requests only from Angular running locally
                });
            });
            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.InfrastureConfiguration(builder.Configuration);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

            app.UseCors("CORSPolicy");
            app.UseMiddleware<ExceptionsMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
