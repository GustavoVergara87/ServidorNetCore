// Unused usings removed
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // => LambdaOperator
            // input parameters => lambda body 
            //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator
            //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#anonymous-function-expressions

            //Crea una base solo en memoria
            //services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("InstrumentosDB"));
            
            //Crea una base local
            //services.AddDbContext<InstrumentoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InstrumentosDB")));
            
            services.AddDbContext<InstrumentoContext>(
                options => options.UseSqlServer("Server=den1.mssql7.gear.host;Database=instrumentosdb;" +
                "Trusted_Connection=False;User Id=instrumentosdb; Password=#Base007;"));

            services.AddControllers();

            services.AddCors(); // Make sure you call this previous to AddMvc
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //necesario comentar para que pueda leerse la API desde un Cliente...
          //  app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

        }
    }
}
