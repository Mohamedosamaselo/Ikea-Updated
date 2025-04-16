using Ikea.BLL.Common.InfrastructureServices.Attachments;
using Ikea.BLL.Services.Department;
using Ikea.BLL.Services.Employees;
using Ikea.BLL.Services.Interfaces;
using Ikea.DAL.Entities.Identity;
using Ikea.DAL.Persistence.Data;
using Ikea.DAL.Persistence.Repositories._Interfaces;
using Ikea.DAL.Persistence.Repositories.UnitOfWork;
using Ikea.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ikea
{
    public class Program
    {
        // Entry Point
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services 

            // Add services to the Dependancy injection  container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>((ServiceProvider) =>
            {
                ServiceProvider
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();// For Uploading Files 

            // builder.Services.AddAutoMapper(typeof(MappingProfile));
            // builder.Services.AddAutoMapper(M => new MappingProfile());
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            // Congigure Security Services and it's Dependancies [ builder.Services.AddAuthentication() ]
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;// @#$
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true; // default false
                //options.User.AllowedUserNameCharacters = "ahmed, Mohamed , omar "

                options.Lockout.AllowedForNewUsers = true;// enable lock the account for NewUsers
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);


            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // builder.Services.AddScoped<UserManager<ApplicationUser>>();
            // builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            // builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddAuthentication();

            builder.Services.ConfigureApplicationCookie((Options) =>
            {
                Options.LoginPath = "/Auth/SignIn"; // if he open the App that doesnot have token [non Authorized User]
                Options.AccessDeniedPath = "/Home/Error";// lw fy ay haga 8alat fy Pass 
                Options.ExpireTimeSpan = TimeSpan.FromDays(1); // Token Expires After one day 
                Options.LogoutPath = "/Auth/SignIn";
                //Options.ForwardSignOut = "/Auth/SignIn";
            });




            // builder.Services.AddAuthentication();
            // builder.Services.AddAuthentication("Identity.Application"); // Default Schema  
            builder.Services.AddAuthentication(options =>                  // lw 3awz a8yer default Schema w hagat tanya 

            {
                options.DefaultAuthenticateScheme = "Identity.Application";
                options.DefaultChallengeScheme = "Identity.Application";
            })
                .AddCookie("Hamada", ".AspNetCore.Hamada", options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/Error";
                    options.ExpireTimeSpan = TimeSpan.FromDays(10);
                    options.LogoutPath = "/Account/SignIn";

                });
           ///            .AddScheme("Hamada01", ".AspNetCore.Hamada01", options =>
           ///            {
           ///            }).AddScheme("Hamada02", ".AspNetCore.Hamada02", options =>
           ///{
           ///});




            #endregion


            var app = builder.Build();

            #region Configure MiddleWares

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();// is user has valid token or not ? 
            app.UseAuthorization(); // is the USer has specific Role or Not to invoke specific action which Requiees Specific Role 


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion


            app.Run();

        }
    }
}
