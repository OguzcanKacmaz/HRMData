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

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings")); //ctor da emailservice g�r�rsen datalar� getsectiondan oku
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);//securitystamp i 30 dk bir kontrol eder e�er farkl� cihazlarda a��k olan oturumlar veri taban�ndaki ile farkl�ysa otomatik ��k�� yapt�rt�p login ekran�na y�nlendirir.
});


builder.Services.AddIdentityWithExt();
builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder
    {
        Name = "HRMDataAppCookie"//cookie ismini verdik
    };//cookie olu�turduk
    options.LoginPath = new PathString("/Home/SignIn");//sadece �yelerin eri�ebilece�i sayfaya giderse y�nlendirilecek loginpath ini veriyoruz 
    options.LogoutPath = new PathString("/Employee/Logout");
    options.Cookie = cookieBuilder; //olu�turdu�umuz cookie yi ekledik
    options.ExpireTimeSpan = TimeSpan.FromDays(60); // cookienin �mr�n�n 60 g�n oldu�u belirledik
    options.SlidingExpiration = true;//true set edilmezse kullan�m s�resi biterse 61. g�n kullan�lmaz silinir. true olursa kullan�c� 60 g�n i�erisinde tekrar giri� yaparsa 60 g�n s�f�rlan�r
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
