using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TheHotel.Common;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Data.Seeding;
using TheHotel.EmailSender;
using TheHotel.EmailSender.ViewRender;
using TheHotel.Mapping;
using TheHotel.Services.ClientRooms;
using TheHotel.Services.Clients;
using TheHotel.Services.Contacts;
using TheHotel.Services.Offers;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels;

namespace TheHotel
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
            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddMvcOptions(opt =>
                {
                    opt.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => GlobalConstants.RequiredPropertyErrorMsg);
                });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Lockout.AllowedForNewUsers = true;
                opt.User.RequireUniqueEmail = true;
            });
            services.AddControllersWithViews();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<IClientsService, ClientsService>();
            services.AddTransient<IClientRoomsService, ClientRoomsService>();
            services.AddTransient<IClientRoomsService, ClientRoomsService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();

            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddTransient<IOffersService, OffersService>();
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

            services.AddTransient<IContactsService, ContactsService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(db).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

                //app.UseExceptionHandler("/Home/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
