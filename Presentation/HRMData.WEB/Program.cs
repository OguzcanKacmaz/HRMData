using FluentValidation;
using FluentValidation.AspNetCore;
using HRMData.Application.Interfaces;
using HRMData.Infrastructure.OptionsModel;
using HRMData.Infrastructure.Services;
using HRMData.Persistence.Repositories;
using HRMData.Persistence.Repositories.EntityFramework;
using HRMData.Persistence.Services;
using HRMData.WEB.Data;
using HRMData.WEB.Entensions;
using HRMData.WEB.Interfaces;
using HRMData.WEB.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);

});

builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("cs"), opt => opt.EnableRetryOnFailure()));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings")); //ctor da emailservice görürsen datalarý getsectiondan oku
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);//securitystamp i 30 dk bir kontrol eder eðer farklý cihazlarda açýk olan oturumlar veri tabanýndaki ile farklýysa otomatik çýkýþ yaptýrtýp login ekranýna yönlendirir.
});


builder.Services.AddIdentityWithExt();
builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder
    {
        Name = "HRMDataAppCookie"//cookie ismini verdik
    };//cookie oluþturduk
    options.LoginPath = new PathString("/Home/SignIn");//sadece üyelerin eriþebileceði sayfaya giderse yönlendirilecek loginpath ini veriyoruz 
    options.LogoutPath = new PathString("/Employee/Logout");
    options.Cookie = cookieBuilder; //oluþturduðumuz cookie yi ekledik
    options.ExpireTimeSpan = TimeSpan.FromDays(60); // cookienin ömrünün 60 gün olduðu belirledik
    options.SlidingExpiration = true;//true set edilmezse kullaným süresi biterse 61. gün kullanýlmaz silinir. true olursa kullanýcý 60 gün içerisinde tekrar giriþ yaparsa 60 gün sýfýrlanýr
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();
builder.Services.AddScoped<ICompanyRepository, EfCompanyRepository>();
builder.Services.AddScoped<IDepartmentRepository, EfDepartmentRepository>();
builder.Services.AddScoped<IEmployeeAdvanceRepository, EfEmployeeAdvanceRepository>();
builder.Services.AddScoped<IEmployeeExpenseRepository, EfEmployeeExpenseRepository>();
builder.Services.AddScoped<ITitleRepository, EfTitleRepository>();


builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddHostedService<RequestCleanupService>();
builder.Services.AddHostedService<EmployeeStatuCheckService>();
builder.Services.AddHostedService<CompanyStatuCheckService>();

builder.Services.AddScoped<IAdvanceRequestService, RequestService>();
builder.Services.AddScoped<ICompanyStatuCheckService, CompanyStatuService>();
builder.Services.AddScoped<IEmployeeStatuService, EmployeeStatuService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<IEmployeePermissionRequestRepository, EfEmployeePermissionRequestRepository>();
builder.Services.AddScoped<IPermissionRequestNumOfDays, EfPermissionRequestNumOfDays>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Profile}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SignIn}/{id?}");

app.Run();
