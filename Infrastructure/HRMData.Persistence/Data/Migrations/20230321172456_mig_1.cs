using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMData.Persistence.Data.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MersisNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxAdministration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoMini = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyTitle = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeCount = table.Column<int>(type: "int", nullable: false),
                    FoundationYear = table.Column<int>(type: "int", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRequestNumOfDays",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AntenatalLeaveDay = table.Column<int>(type: "int", nullable: false),
                    PostnatalLeaveDay = table.Column<int>(type: "int", nullable: false),
                    PaternityLeaveDay = table.Column<int>(type: "int", nullable: false),
                    BereavementLeaveDay = table.Column<int>(type: "int", nullable: false),
                    MarriageLeaveDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRequestNumOfDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AnnualLeaveDays = table.Column<double>(type: "float", nullable: false),
                    RemainingAnnualLeaveDays = table.Column<double>(type: "float", nullable: false),
                    ExcusedAbsenceDay = table.Column<double>(type: "float", nullable: false),
                    ResetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoMini = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TcIdentificationNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomPassword = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxPaymentAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RemainingAdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TitleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAdvances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    AdvancePaymentType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAdvances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeExpenses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpenseType = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    ExpenseDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeExpenses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePermissionRequest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumOfDays = table.Column<double>(type: "float", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePermissionRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePermissionRequest_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b25745ce-4947-4d2e-b6b7-a3fbd7d0f47c", "0cef9786-be5d-4516-94d3-3127a493e068", "Manager", "MANAGER" },
                    { "d6d17082-dcb2-4074-9a97-eb66f8b0b575", "3c9b60b1-a70c-4819-a15f-7cbf73424661", "Personel", "PERSONEL" },
                    { "f670346d-4028-4cdb-adcf-8b510ebf4218", "c8eb3d15-23c2-4dfc-b1c8-dfcdc865fb51", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0a789736-bc55-4a59-8b8c-0cc5609abc92", 0, "1aa02ea6-2bd0-42d6-a8a5-2b2b121361d8", "mustafaoguzcan.kacmaz@bilgeadamboost.com", true, true, new DateTimeOffset(new DateTime(2023, 3, 21, 20, 24, 56, 362, DateTimeKind.Unspecified).AddTicks(2629), new TimeSpan(0, 3, 0, 0, 0)), "MUSTAFAOGUZCAN.KACMAZ@BILGEADAMBOOST.COM", "MUSTAFAOGUZCAN.KACMAZ@BILGEADAMBOOST.COM", "AQAAAAEAACcQAAAAEBFHO36ceHPsvTOxqxkgyD4kX0+JMI+qqFjjxL2cO+Fjo6AwJ6p45Vptgh//3WQcyg==", "5375988848", true, "ca08f248-1c1c-4685-8546-a22781f27999", false, "mustafaoguzcan.kacmaz@bilgeadamboost.com" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CompanyTitle", "ContractEndDate", "ContractStartDate", "Email", "EmployeeCount", "FoundationYear", "IsActive", "Logo", "LogoMini", "MersisNo", "Name", "PhoneNumber", "TaxAdministration", "TaxNumber" },
                values: new object[] { "deffb8e3-228d-465a-a118-76d75523bacc", "Ankara", 0, new DateTime(2053, 3, 21, 20, 24, 56, 361, DateTimeKind.Local).AddTicks(2414), new DateTime(2013, 3, 21, 20, 24, 56, 361, DateTimeKind.Local).AddTicks(2405), "boost@bilgeadamboost.com", 3, 2012, 1, "ba_logo.png", "ba_logomini.png", "0153318035900015", "Bilge adam Boost", "444 33 30", "Çankaya Vergi Dairesi", "1533180359" });

            migrationBuilder.InsertData(
                table: "PermissionRequestNumOfDays",
                columns: new[] { "Id", "AntenatalLeaveDay", "BereavementLeaveDay", "MarriageLeaveDay", "PaternityLeaveDay", "PostnatalLeaveDay" },
                values: new object[] { "de54e229-5abf-4a8a-bb1b-587dd7c384a4", 56, 3, 3, 5, 56 });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "0697a9f9-8218-435c-bfcd-b8c42b560296", "Muhasebe Grup Müdürü" },
                    { "0cd88580-c32f-4cd6-a492-02c35c0cbf2d", "Muhasebe Müdür Yardımcısı" },
                    { "0d5a085c-bbbb-4b38-aece-b55b72f072d3", "Bilişim Teknolojileri Direktörü" },
                    { "0f5df388-47a3-4153-a1dc-537f9cdf2ae9", "İnşaat Mühendisi" },
                    { "160225f2-2e8d-4736-bf9a-95fea1f5b7c2", "Sistem Uzmanı" },
                    { "22aaccfc-7d78-4fbd-8039-fbc4f8390200", "Genel Müdür İş Asistanı" },
                    { "23c45285-ccd0-4dd5-8d0d-41927fca9cea", "İnsan Kaynakları Uzmanı" },
                    { "29a69de2-34be-4469-8ef2-487687dbf893", "Büro Memuru" },
                    { "2d330576-bada-4700-837c-2ecb967bd698", "Kurumsal İletişim Yöneticisi" },
                    { "33c1ee1f-caff-4b7a-a2c5-2b71c5ee9ef5", "İnsan Kaynakları Müdürü" },
                    { "37adb675-5ed4-4677-8322-3897b9994e17", "Depo Sorumlusu" },
                    { "39ed7a28-18da-4cdb-8069-5378da250bd6", "İçerik ve Proje Koordinatörü" },
                    { "40028b09-d643-4cf1-9daa-b926a5ffece7", "Web Sitesi Sorumlusu" },
                    { "401dbd73-d16e-471f-9fd1-f5d71312c0ec", "Finans Müdür Yardımcısı" },
                    { "42f13044-542a-4092-9fb3-8ac3ab4eaf15", "IT Koordinatörü" },
                    { "481c0e34-c1ed-49a1-bc5b-36336957c38d", "Bilişim Teknolojileri Saha Sorumlusu" },
                    { "4b7daaef-d5ba-41cc-8d6f-168df13e4425", "Finans Müdürü" },
                    { "4dc2df71-1706-468d-bd33-518323d5e771", "Kurumsal Gelişim Müdürü" },
                    { "4f321f30-1509-497f-81c7-3d46f5452024", "Bordro Yöneticisi" },
                    { "52165ed6-afe0-4a2d-a1dc-a0a3665c6e1c", "Kurumsal İletişim Uzmanı" },
                    { "5563f840-ca72-4393-b701-cbc53e07e418", "Finansal Muhasebe Uzmanı" },
                    { "57276753-fee5-40f5-adee-75dca9469ef9", "İdari İşler Uzman Yardımcısı" },
                    { "5cb4d40a-ea75-434c-8df5-f907889d243f", "Avukat" },
                    { "698a4bec-7995-4dbb-b7d0-8f2757c6d3d7", "Satınalma Müdürü" },
                    { "6b661537-5ead-41e3-8e71-c2879381198d", "Tedarik Uzmanı" },
                    { "6fac572c-01ce-41f0-acf4-759bfe90bc8f", "Finansal Muhasebe Uzman Yardımcısı" },
                    { "702453f6-884d-43cd-b8bd-bdf7c3afd665", "Finans Uzmanı" },
                    { "7205f8f0-1969-4f48-ade2-2098a53c7de4", "Danışma Görevlisi" },
                    { "799622e0-de10-432c-af6c-d00f132e993a", "Kurumsal İletişim Uzman Yardımcısı" },
                    { "7e09e04e-591a-44a5-bb53-813e54ec3776", "İnsan Kaynakları Direktörü" },
                    { "84641b54-5f34-4638-a96c-583aec81936b", "Hemşire" },
                    { "84d077dd-5213-42b5-9d21-35f4d4d4cc35", "Muhasebe Uzman Yardımcısı" },
                    { "8510bcc9-7951-420a-9838-518aac8f9b1d", "Basın İletişim Direktörü" },
                    { "85a5f33b-b50e-47f2-af2e-626134090348", "Finans Direktörü" },
                    { "880adb3c-d70d-43e4-9184-8057ace8af51", "Kurumsal Proje Yöneticisi" },
                    { "88b192ae-4b51-494b-94b8-1308d85a184b", "Kurumsal Etkinlik ve Organizasyon Direktörü" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "93f066be-7e51-4653-9fb5-7240b32f45d1", "Asistan" },
                    { "961d3464-fde5-4ad9-87a2-02b7f1113974", "Kurumsal İletişim Müdürü" },
                    { "9e0c035a-2c0f-4b98-a778-5ef0304d734a", "Muhasebe Uzmanı" },
                    { "9f7681e3-8b99-4270-97e8-319abd1235de", "Yeni Medya ve Kreatif Direktörü" },
                    { "a4b72db3-9ca2-4a3d-b7c9-58f8ee58bb94", "Bilişim Teknolojileri Saha Uzmanı" },
                    { "a77ac15b-468f-42da-a2ee-d6c74b907e2b", "Bilişim Teknolojileri Uzman Yardımcısı" },
                    { "a796d723-e214-4705-bb61-c7999f48d0e5", "İdari İşler Uzmanı" },
                    { "a79cb6c6-bdd0-440e-8cc1-505414cb93da", "Finans Yöneticisi" },
                    { "a9e37713-8280-47a7-ada7-2c84d5e622cf", "Satınalma Uzman Yardımcısı" },
                    { "adb5a4d0-0ff1-48ac-b757-d2e81025ca8e", "İş Geliştirme Koordinatörü" },
                    { "adca1c96-626c-4729-8424-5e834ecfb0da", "Bordro Uzman Yardımcısı" },
                    { "ae683088-15c5-4738-8d2c-741d739a46f4", "Muhasebe Yöneticisi" },
                    { "b3e773e6-9eb4-4fe1-87d2-03430c401978", "Grafiker" },
                    { "b661637b-5c68-4a5f-9da6-200b92129c1d", "İdari İşler Yöneticisi" },
                    { "b91cade2-8497-4bbd-bb70-9fce1ca2b316", "Yazılım Uzmanı" },
                    { "bc437ab5-957d-4c3d-af7d-3dd2197d6b5f", "Lojistik İşler Görevlisi" },
                    { "c06c7891-99e8-46ea-8186-1b997e51ab61", "Satınalma Uzmanı" },
                    { "ca0741ff-9214-4fc6-944c-8f8f483b6137", "Kurumsal Gelişim Direktörü" },
                    { "cb769b74-ec29-4098-9176-1c492bde5aeb", "Bordro Müdürü" },
                    { "cd25e72f-b47f-4fa5-9a7c-7ebc1211346c", "İnsan Kaynakları Uzman Yardımcısı" },
                    { "cd980cb4-705f-42f4-9907-6fc60cea53e4", "Finans Uzman Yardımcısı" },
                    { "cee31a14-ba0c-493f-8aca-bc10b5c08351", "Dijital Ürün Yöneticisi" },
                    { "cf9b363a-a738-4787-8f97-c5378b650829", "Bütçe ve Finans Müdürü" },
                    { "d5ec3d03-e41d-4d4f-8aa3-9b138ec8f0c3", "İnsan Kaynakları Koordinatörü" },
                    { "de8fd884-6ad4-4025-b577-ae6713183004", "Bordro Uzmanı" },
                    { "e00d2c43-84ca-4d61-99d1-32949b430460", "Bütçe ve Raporlama Müdürü" },
                    { "e33213db-ad5b-4d4d-b502-f05cbc0e0dd3", "İletişim Uzmanı" },
                    { "e66b21c3-5bd0-4b4f-b2b5-b2b8934ce601", "İnşaat Birim Yöneticisi" },
                    { "e82c0639-096f-41ff-87bf-07d977e83760", "Bilişim Teknolojileri Teknik Uzmanı" },
                    { "ebdb020b-432c-4d3f-91c1-cf3ce26850a0", "Finansal Kontrol Müdürü" },
                    { "efdd1ac0-2209-428a-bd8e-e0cac6b9f2e2", "Sistem ve Network Yöneticisi" },
                    { "f04b2d49-a5e0-4ea7-8b5f-019c98b8cdc7", "İş Sağlığı ve Güvenliği Uzmanı" },
                    { "fc63563d-260b-401e-be9a-4d573d930f08", "İnsan Kaynakları Yöneticisi" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f670346d-4028-4cdb-adcf-8b510ebf4218", "0a789736-bc55-4a59-8b8c-0cc5609abc92" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { "3c3d4dc4-bd26-4a96-a44a-59f1ec7741a7", "deffb8e3-228d-465a-a118-76d75523bacc", "Satınalma Müdürlüğü" },
                    { "44c85d08-371e-4a22-81a8-63ec12162b89", "deffb8e3-228d-465a-a118-76d75523bacc", "Kurumsal İletişim" },
                    { "497cccbf-6f81-46a9-bb91-526aa9c9d2d2", "deffb8e3-228d-465a-a118-76d75523bacc", "İnsan Kaynakları" },
                    { "6a8f6e32-0a60-4f9f-b7d8-9c5b7d5db5d5", "deffb8e3-228d-465a-a118-76d75523bacc", "Bilişim Teknolojileri" },
                    { "71fda0a6-9f0d-4d34-8c72-482ccf44a625", "deffb8e3-228d-465a-a118-76d75523bacc", "İdari Kadro" },
                    { "a535e0d9-9f17-4a37-ae90-d29e5f5d25e5", "deffb8e3-228d-465a-a118-76d75523bacc", "Muhasebe Müdürlüğü" },
                    { "e1f6c71b-9302-4c4e-b3f4-ae0b6f20a6f1", "deffb8e3-228d-465a-a118-76d75523bacc", "Finans Müdürlüğü" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "AnnualLeaveDays", "AppUserId", "CompanyId", "DateOfBirth", "DepartmentId", "ExcusedAbsenceDay", "FirstName", "Gender", "HireDate", "IsActive", "LastName", "MaxPaymentAmount", "MiddleName", "Photo", "PhotoMini", "PlaceOfBirth", "RandomPassword", "RemainingAdvanceAmount", "RemainingAnnualLeaveDays", "ResetDate", "Salary", "SecondLastName", "TcIdentificationNumber", "TerminationDate", "TitleId" },
                values: new object[] { "417b8151-ff69-4511-b91e-fe8d114c47a0", "Ankara", 14.0, "0a789736-bc55-4a59-8b8c-0cc5609abc92", "deffb8e3-228d-465a-a118-76d75523bacc", new DateTime(1992, 3, 30, 0, 45, 0, 0, DateTimeKind.Unspecified), "497cccbf-6f81-46a9-bb91-526aa9c9d2d2", 1.0, "Mustafa", 1, new DateTime(2021, 8, 30, 14, 12, 22, 0, DateTimeKind.Unspecified), 1, "Kaçmaz", 45000m, "Oğuzcan", "default.jpg", "defaultmini.jpg", "Düzce", false, 45000m, 14.0, new DateTime(2023, 8, 30, 14, 12, 22, 0, DateTimeKind.Unspecified), 15000m, null, "47830698410", null, "33c1ee1f-caff-4b7a-a2c5-2b71c5ee9ef5" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvances_EmployeeId",
                table: "EmployeeAdvances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExpenses_EmployeeId",
                table: "EmployeeExpenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermissionRequest_EmployeeId",
                table: "EmployeePermissionRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppUserId",
                table: "Employees",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TcIdentificationNumber",
                table: "Employees",
                column: "TcIdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TitleId",
                table: "Employees",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_Name",
                table: "Titles",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeAdvances");

            migrationBuilder.DropTable(
                name: "EmployeeExpenses");

            migrationBuilder.DropTable(
                name: "EmployeePermissionRequest");

            migrationBuilder.DropTable(
                name: "PermissionRequestNumOfDays");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
