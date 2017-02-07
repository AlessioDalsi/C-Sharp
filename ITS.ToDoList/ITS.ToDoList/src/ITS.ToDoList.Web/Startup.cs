using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ITS.ToDoList.Web.Data;
using ITS.ToDoList.Web.Models;
using ITS.ToDoList.Web.Services;
using ITS.ToDoList.Data;

namespace ITS.ToDoList.Web
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				// For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets();
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>(
				o =>
				{
					o.Password.RequireDigit = false;
					o.Password.RequireLowercase = false;
					o.Password.RequireUppercase = false;
					o.Password.RequireNonAlphanumeric = false;
					o.Password.RequiredLength = 6;
					o.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 15, 0);
					o.Lockout.MaxFailedAccessAttempts = 15;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			// Aggiungo la cache
			services.AddMemoryCache();
			// Aggiungo la sessione
			services.AddDistributedMemoryCache();
			services.AddSession();

			services.AddMvc();

			// Add application services.
			services.AddTransient<IEmailSender, AuthMessageSender>();
			services.AddTransient<ISmsSender, AuthMessageSender>();
			services.AddScoped<TempFavoritesService>();

			//services.AddSingleton<ICategoryRepository>(new SqlCategoryRepository("stringa di connessione"));
			services.AddSingleton<ICategoryRepository, SqlCategoryRepository>();
			services.AddSingleton<IToDoItemRepository, SqlToDoItemRepository>();

			services.AddSingleton<IConfiguration>(this.Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/log.txt", isJson:true);

            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseIdentity();

			// Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});


		}
	}
}
