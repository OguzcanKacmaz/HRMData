using HRMData.Domain.Entities;
using HRMData.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HRMData.WEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Title> Titles => Set<Title>();
        public DbSet<EmployeeAdvance> EmployeeAdvances => Set<EmployeeAdvance>();
        public DbSet<EmployeeExpense> EmployeeExpenses => Set<EmployeeExpense>();
        public DbSet<EmployeePermissionRequest> EmployeePermissionRequest => Set<EmployeePermissionRequest>();
        public DbSet<PermissionRequestNumOfDays> PermissionRequestNumOfDays => Set<PermissionRequestNumOfDays>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
                new Department()
                {
                    Id = "71fda0a6-9f0d-4d34-8c72-482ccf44a625",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "İdari Kadro"
                },
                new Department()
                {
                    Id = "6a8f6e32-0a60-4f9f-b7d8-9c5b7d5db5d5",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "Bilişim Teknolojileri"
                },
                new Department()
                {
                    Id = "e1f6c71b-9302-4c4e-b3f4-ae0b6f20a6f1",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "Finans Müdürlüğü"
                },
                new Department()
                {
                    Id = "497cccbf-6f81-46a9-bb91-526aa9c9d2d2",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "İnsan Kaynakları"
                },
                new Department()
                {
                    Id = "44c85d08-371e-4a22-81a8-63ec12162b89",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "Kurumsal İletişim"
                },
                new Department()
                {
                    Id = "a535e0d9-9f17-4a37-ae90-d29e5f5d25e5",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "Muhasebe Müdürlüğü"
                },
                new Department()
                {
                    Id = "3c3d4dc4-bd26-4a96-a44a-59f1ec7741a7",
                    CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                    Name = "Satınalma Müdürlüğü"
                }
                );

            builder.Entity<Title>().HasData(
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Asistan"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Avukat"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bilişim Teknolojileri Teknik Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Büro Memuru"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Danışma Görevlisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finans Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finansal Muhasebe Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Genel Müdür İş Asistanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Hemşire"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İdari İşler Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İdari İşler Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İdari İşler Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İş Sağlığı ve Güvenliği Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Lojistik İşler Görevlisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tedarik Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bilişim Teknolojileri Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bilişim Teknolojileri Saha Sorumlusu"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bilişim Teknolojileri Saha Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bilişim Teknolojileri Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Dijital Ürün Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "IT Koordinatörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sistem Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sistem ve Network Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Web Sitesi Sorumlusu"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Yazılım Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bütçe ve Finans Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bütçe ve Raporlama Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finans Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finans Müdür Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finans Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finans Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finans Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finansal Kontrol Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bordro Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bordro Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bordro Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bordro Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnsan Kaynakları Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnsan Kaynakları Koordinatörü"
                },
                new Title()
                {
                    Id = "33c1ee1f-caff-4b7a-a2c5-2b71c5ee9ef5",
                    Name = "İnsan Kaynakları Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnsan Kaynakları Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnsan Kaynakları Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnsan Kaynakları Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Basın İletişim Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Grafiker"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İçerik ve Proje Koordinatörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İletişim Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İş Geliştirme Koordinatörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal Etkinlik ve Organizasyon Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal Gelişim Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal Gelişim Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal İletişim Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal İletişim Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal İletişim Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal İletişim Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kurumsal Proje Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Yeni Medya ve Kreatif Direktörü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Finansal Muhasebe Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Muhasebe Grup Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Muhasebe Müdür Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Muhasebe Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Muhasebe Uzmanı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Muhasebe Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Depo Sorumlusu"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnşaat Birim Yöneticisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "İnşaat Mühendisi"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Satınalma Müdürü"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Satınalma Uzman Yardımcısı"
                },
                new Title()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Satınalma Uzmanı"
                }
                );

            builder.Entity<AppRole>().HasData
                (
                new AppRole()
                {
                    Id = "d6d17082-dcb2-4074-9a97-eb66f8b0b575",
                    Name = "Personel",
                    NormalizedName = "PERSONEL"
                },
                new AppRole()
                {
                    Id = "b25745ce-4947-4d2e-b6b7-a3fbd7d0f47c",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                 new AppRole()
                 {
                     Id = "f670346d-4028-4cdb-adcf-8b510ebf4218",
                     Name = "Admin",
                     NormalizedName = "ADMIN"
                 }
                );
            builder.Entity<PermissionRequestNumOfDays>().HasData
                (
                new PermissionRequestNumOfDays()
                {
                    MarriageLeaveDay = 3
                }
                );

            builder.Entity<Company>().HasData
                (
                new Company()
                {
                    Name = "Bilge adam Boost",
                    Address = "Ankara",
                    Email = "boost@bilgeadamboost.com",
                    Id = "deffb8e3-228d-465a-a118-76d75523bacc",
                    ContractStartDate= DateTime.Now.AddYears(-10),
                    ContractEndDate= DateTime.Now.AddYears(30),
                    EmployeeCount=3,
                    FoundationYear=2012,
                    IsActive=Status.Active,
                    LogoMini= "ba_logomini.png",
                    Logo="ba_logo.png",
                    CompanyTitle=Domain.Enums.CompanyTitle.Limited,
                    MersisNo= "0153318035900015",
                    TaxNumber= "1533180359",
                    PhoneNumber= "444 33 30",
                    TaxAdministration="Çankaya Vergi Dairesi"
                }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "0a789736-bc55-4a59-8b8c-0cc5609abc92",
                    RoleId = "f670346d-4028-4cdb-adcf-8b510ebf4218"
                }
                );

            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData
                (
                new AppUser
                {
                    Id = "0a789736-bc55-4a59-8b8c-0cc5609abc92",
                    PhoneNumber = "5375988848",
                    Email = "mustafaoguzcan.kacmaz@bilgeadamboost.com",
                    UserName = "mustafaoguzcan.kacmaz@bilgeadamboost.com",
                    NormalizedEmail = "MUSTAFAOGUZCAN.KACMAZ@BILGEADAMBOOST.COM",
                    PasswordHash = hasher.HashPassword(null!, "HRMData123!"),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = true,
                    NormalizedUserName = "MUSTAFAOGUZCAN.KACMAZ@BILGEADAMBOOST.COM",
                    LockoutEnd = DateTime.Now
                }
                );
            builder.Entity<Employee>().HasData(
                      new Employee()
                      {
                          Id = "417b8151-ff69-4511-b91e-fe8d114c47a0",
                          TitleId = "33c1ee1f-caff-4b7a-a2c5-2b71c5ee9ef5",
                          Address = "Ankara",
                          IsActive = Status.Active,
                          DateOfBirth = new DateTime(1992, 03, 30, 00, 45, 00),
                          FirstName = "Mustafa",
                          MiddleName = "Oğuzcan",
                          LastName = "Kaçmaz",
                          Photo = "default.jpg",
                          PhotoMini = "defaultmini.jpg",
                          HireDate = new DateTime(2021, 08, 30, 14, 12, 22),
                          DepartmentId = "497cccbf-6f81-46a9-bb91-526aa9c9d2d2",
                          PlaceOfBirth = "Düzce",
                          Salary = 15000,
                          TcIdentificationNumber = "47830698410",
                          CompanyId = "deffb8e3-228d-465a-a118-76d75523bacc",
                          MaxPaymentAmount = 45000,
                          RemainingAdvanceAmount = 45000,
                          AnnualLeaveDays = 14,
                          RemainingAnnualLeaveDays = 14,
                          Gender = Domain.Enums.Gender.Male,
                          ResetDate = new DateTime(2023, 08, 30, 14, 12, 22),
                          AppUserId = "0a789736-bc55-4a59-8b8c-0cc5609abc92",
                          RandomPassword = false,
                      }
                      );
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}

