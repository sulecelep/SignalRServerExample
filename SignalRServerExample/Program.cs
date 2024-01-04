using SignalRServerExample.Business;
using SignalRServerExample.Hubs;
using System.Security.Cryptography.X509Certificates;

namespace SignalRServerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy=>
                policy.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .SetIsOriginAllowed(origin => true)
            ));
            builder.Services.AddTransient<MyBusiness>();

            builder.Services.AddSignalR();
            builder.Services.AddControllers();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.UseCors();
            //https://localhost:7229/myhub 
            app.MapHub<MyHub>("/myhub");
            app.MapControllers();
            app.Run();
        }
    }
}
