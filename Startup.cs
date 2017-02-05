using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using reQuest.Backend.Entities;
using reQuest.Backend.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace reQuest.Backend
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<reQuestDbContext>(options => options.UseSqlite(Startup.Configuration.GetConnectionString("reQuestDb")));
            services.AddScoped<IreQuestRepository, reQuestRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, reQuestDbContext dbContext)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            dbContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(config => 
            {
                config.CreateMap<Entities.Quest, ViewModels.QuestViewModel>().ReverseMap();
                config.CreateMap<Entities.Player, ViewModels.PlayerViewModel>().ReverseMap();
                config.CreateMap<Entities.Player, ViewModels.PlayerUpdateViewModel>().ReverseMap();
                config.CreateMap<Entities.Team, ViewModels.TeamViewModel>().ReverseMap();
                config.CreateMap<Entities.Competency, ViewModels.CompetencyViewModel>().ReverseMap();
                config.CreateMap<Entities.Competency, ViewModels.CompetencyUpdateViewModel>().ReverseMap();


            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "reQuestCookie",
                LoginPath = new PathString("/Login/"),
                AccessDeniedPath = new PathString("/Login/Denied/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseFileServer();
            // app.UseStaticFiles(new StaticFileOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"node_modules")),
            //     RequestPath = new PathString("/node_modules")
            // });
            app.UseMvc();
        }
    }
}
