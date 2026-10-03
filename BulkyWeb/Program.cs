using System;
using System.Globalization;
using System.Net.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StyleHub.DataAccess;
using StyleHub.DataAccess.Repository;
using StyleHub.DataAccess.Repository.IRepository;
using StyleHub.Models;
using StyleHub.Utility;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteConn")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Localization & Globalization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] {
        new CultureInfo("en-us"),
        new CultureInfo("ar-eg"),
        new CultureInfo("fr"),
        //new CultureInfo("de"),
        //new CultureInfo("es"),
        //new CultureInfo("ru"),
        //new CultureInfo("zh")

    };
    options.DefaultRequestCulture = new RequestCulture(culture: "en-us", uiCulture: "en-us");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Automapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.ConfigureApplicationCookie(options=>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IHtmlLocalizer, HtmlLocalizer>();
builder.Services.AddRazorPages();



var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".glb"] = "model/gltf-binary";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{area=customer}/{controller=home}/{action=index}/{id?}");





app.MapRazorPages();



//// Add Admin User
//using (var scope = app.Services.CreateScope())
//{
//    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

//    if (await _userManager.FindByEmailAsync(SD.MAIN_ADMIN_EMAIL) == null)
//    {
//        var user = new AppUser
//        {
//            Email = SD.MAIN_ADMIN_EMAIL,
//            UserName = new MailAddress(SD.MAIN_ADMIN_EMAIL).User
//        };
//        await _userManager.CreateAsync(user, SD.MAIN_ADMIN_PASSWORD);

//        // Add all roles to admin user
//        await _userManager.AddToRoleAsync(user, SD.Role_Admin);
//        await _userManager.AddToRoleAsync(user, SD.Role_User);

//    }
//}





app.Run();
