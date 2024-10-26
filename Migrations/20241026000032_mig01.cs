using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication71.Migrations
{
    public partial class mig01 : Migration
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
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wojewodztwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataUrodzenia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plec = table.Column<int>(type: "int", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IloscZalogowan = table.Column<int>(type: "int", nullable: false),
                    DataOstatniegoZalogowania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "Logowania",
                columns: table => new
                {
                    LogowanieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataLogowania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataWylogowania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CzasPracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logowania", x => x.LogowanieId);
                    table.ForeignKey(
                        name: "FK_Logowania_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Klikniecia = table.Column<int>(type: "int", nullable: false),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bb93f4c0-e328-4460-9ab6-f78687559fc5", "c9eb1176-c739-4696-9902-85c0b1891295", "Administrator", "Administrator" },
                    { "89164085-eed3-4ea1-9cfb-46abff32cbd1", "697a1289-9a69-4895-b80d-067ba25a7b11", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataDodania", "DataOstatniegoZalogowania", "DataUrodzenia", "Email", "EmailConfirmed", "IloscZalogowan", "Imie", "KodPocztowy", "LockoutEnabled", "LockoutEnd", "Miejscowosc", "Nazwisko", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Pesel", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "Plec", "SecurityStamp", "Telefon", "TwoFactorEnabled", "Ulica", "UserName", "Wojewodztwo" },
                values: new object[,]
                {
                    { "8deed1bc-987d-49d4-a4c0-7610de4c14d7", 0, "15b41f77-63e1-4295-a1a7-4f96322acd7f", "26.10.2024 02:00:32", "", "25.04.1990 16:59:58", "admin@admin.pl", false, 0, "stzłlyxx", "12-222", false, null, "blgpagku", "ijłocbiy", "ADMIN@ADMIN.PL", "ADMIN@ADMIN.PL", "AQAAAAEAACcQAAAAEJP5+J5hI0PkSgI5URTLvBz0oFbKAWkdhLvu/XE6eXntmREjD0ue3UYXUokW4AIXrQ==", "585306069", null, false, "", 1, "5d6bf7ef-66fc-4ce9-b87e-3b75de2cfe23", "173 946 563", false, "dinheibó", "admin@admin.pl", "Mazowieckie" },
                    { "38e9ee13-b55e-4268-9946-8d26b6dd52f1", 0, "67808aa8-8583-43c0-bd8e-bccd7d45d965", "26.10.2024 02:00:32", "", "25.04.1990 16:59:58", "user@user.pl", false, 0, "xjkfrhp", "12-222", false, null, "jfehzknzxp", "mupdgrpasc", "USER@USER.PL", "USER@USER.PL", "AQAAAAEAACcQAAAAEOoF9Ra+iccTmQjuN56x7v9jNLwZ7iDbExNuOnGKx7RsstOIvhGzqaOVW9ttJe0lbA==", "585306069", null, false, "", 1, "e42d4b6e-44c2-473f-9f98-a01de851787b", "798 348 128", false, "knpkonula", "user@user.pl", "Mazowieckie" },
                    { "bb63dfa9-f2a4-48ea-befb-9a74138676a4", 0, "111b5d3c-ceac-4551-bf32-57615204860d", "26.10.2024 02:00:32", "", "25.04.1990 16:59:58", "aaa@aaa.pl", false, 0, "pktffft", "12-222", false, null, "hbbókmte", "npgeojmgyt", "AAA@AAA.PL", "AAA@AAA.PL", "AQAAAAEAACcQAAAAEGXq7gy4s9d/G9cYIDOulSa+eDataI1FmqmfyNVLUeyC8TfoiV0m0Bx3pk7PcztjGw==", "585306069", null, false, "", 1, "ae1f3e14-aff8-435c-91d2-9a44c51c9c18", "180 157 873", false, "óuodenlu", "aaa@aaa.pl", "Mazowieckie" },
                    { "4cad5b00-dc00-4d9e-a326-ad656cb79e63", 0, "5e0c75fc-8c09-485c-b035-07afa24fbd4f", "26.10.2024 02:00:32", "", "25.04.1990 16:59:58", "bbb@bbb.pl", false, 0, "ejibrkzca", "12-222", false, null, "kscojjskht", "xlepegulb", "BBB@BBB.PL", "BBB@BBB.PL", "AQAAAAEAACcQAAAAEFhUGP9j8Fz1IM1jvBXvuzHnPaYOzB5fN37SUV9kxDbVXpc+4vTv3MNNHshIhgrH2w==", "585306069", null, false, "", 1, "1c902b7a-e8dc-4fd9-bf87-b4911fdb2229", "880 901 971", false, "optkllł", "bbb@bbb.pl", "Mazowieckie" },
                    { "85179875-e244-4824-9ab3-8fc02088855d", 0, "cf9485fd-b67b-42fb-9a98-b97d926164d6", "26.10.2024 02:00:32", "", "25.04.1990 16:59:58", "ccc@ccc.pl", false, 0, "edbnxyó", "12-222", false, null, "głhbtytzai", "kónobsx", "CCC@CCC.PL", "CCC@CCC.PL", "AQAAAAEAACcQAAAAEG5bAXb7sERLh5IUDnTWogQojJ/jz+ymGRsTBEkkedq5uDHbZgqGD7xRFd1S3nizmQ==", "585306069", null, false, "", 1, "d94f16ae-c040-4c76-b7ad-7f6f47aa276e", "738 869 865", false, "moyngxj", "ccc@ccc.pl", "Mazowieckie" },
                    { "6bc5c350-0b63-4436-b7b1-f405241f7dc9", 0, "add231d0-0590-4a6d-bbc1-76ccd88233f4", "26.10.2024 02:00:32", "", "25.04.1990 16:59:58", "ddd@ddd.pl", false, 0, "cmbxłoics", "12-222", false, null, "sóusłrlzyt", "daxldeuiexg", "DDD@DDD.PL", "DDD@DDD.PL", "AQAAAAEAACcQAAAAEPFJkBoPqlOePFT9NwN1IpZuxtJew4BBbIUwWjBE4//M1yMYl9ODjBdcFn218OeWDg==", "585306069", null, false, "", 1, "03d80b24-d731-4181-9d17-6a9ae70c2af9", "857 831 236", false, "jhjotilbn", "ddd@ddd.pl", "Mazowieckie" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { "a91334f8-54b4-4e25-89c2-1188a0417ee0", "Komedia" },
                    { "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "Romans" },
                    { "2395ec44-43fa-4706-8ae2-bc5c962e032c", "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "bb93f4c0-e328-4460-9ab6-f78687559fc5", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "89164085-eed3-4ea1-9cfb-46abff32cbd1", "85179875-e244-4824-9ab3-8fc02088855d" },
                    { "89164085-eed3-4ea1-9cfb-46abff32cbd1", "4cad5b00-dc00-4d9e-a326-ad656cb79e63" },
                    { "89164085-eed3-4ea1-9cfb-46abff32cbd1", "6bc5c350-0b63-4436-b7b1-f405241f7dc9" },
                    { "89164085-eed3-4ea1-9cfb-46abff32cbd1", "38e9ee13-b55e-4268-9946-8d26b6dd52f1" },
                    { "89164085-eed3-4ea1-9cfb-46abff32cbd1", "bb63dfa9-f2a4-48ea-befb-9a74138676a4" }
                });

            migrationBuilder.InsertData(
                table: "Logowania",
                columns: new[] { "LogowanieId", "CzasPracy", "DataLogowania", "DataWylogowania", "UserId" },
                values: new object[,]
                {
                    { "b5448d6c-6edf-4989-9b71-68b0a9def90b", null, "26.10.2024 02:00:32", "", "bb63dfa9-f2a4-48ea-befb-9a74138676a4" },
                    { "924c1389-5f9f-48cc-8956-52e18064fea3", null, "26.10.2024 02:00:32", "", "4cad5b00-dc00-4d9e-a326-ad656cb79e63" },
                    { "6e26134f-67e2-4dd3-a543-18ba64371abb", null, "26.10.2024 02:00:32", "", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "3ce5aee3-9603-4558-926d-3ae75fa09345", null, "26.10.2024 02:00:32", "", "85179875-e244-4824-9ab3-8fc02088855d" },
                    { "23b1f7f3-d4b1-4f89-b716-e665f2c273c2", null, "26.10.2024 02:00:32", "", "85179875-e244-4824-9ab3-8fc02088855d" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "CategoryId", "DataDodania", "Description", "Klikniecia", "Photo", "Price", "Title", "UserId" },
                values: new object[,]
                {
                    { "3f2098a8-52f6-4992-9cf7-f0b5f426603b", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "ieym. ", 0, "https://th.bing.com/th/id/OIP.Ybzxe9qfOcMv4wbzxLttnQHaLb?w=193&h=298&c=7&r=0&o=5&dpr=1.6&pid=1.7", 163.0, "znitzteuzxh", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "886301d2-6ac8-4c6d-b66e-512f2a08a7e7", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "yseis iebrc łdłbgbg iósltfpdłm gjfrxdfhg. ", 0, "https://th.bing.com/th/id/OIP.ezcZDNwC18nndTJ-mWSmWAHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 135.0, "dndzuopy", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "cd5c5453-0f77-4dfb-af83-dc5743709e3a", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "snłm. ", 0, "https://th.bing.com/th/id/OIP.z-BvaMHwT-e4eDGIU910HwHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 173.0, "łxiyobalsjopłó", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "31e515fd-ba2a-4a2a-8e8e-bca2ff36153a", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "kózj deppsed ójoókpt hukkcłtm tiópbacpfk xmyitg xutmj. ", 0, "https://th.bing.com/th/id/OIP.Ruer00AChRpCRJkyHXki4gHaK-?w=186&h=276&c=7&r=0&o=5&dpr=1.8&pid=1.7", 167.0, "rłyódgloya", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "4ce470d1-391a-4e5d-8d97-f622937d3b56", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "egsdzrdk yófr. ", 0, "https://th.bing.com/th/id/OIP.ykllnoTUaAyNHNmm1cIXqgHaKE?w=186&h=253&c=7&r=0&o=5&dpr=1.8&pid=1.7", 118.0, "łxixfptrxo", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "6e58e309-2170-4499-b6e5-41d6d6cceb6b", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "łxgj goebdm kókpad. ", 0, "https://th.bing.com/th/id/OIP.vkzBGI8duWq91j57EAEDpQHaKm?w=186&h=267&c=7&r=0&o=5&dpr=1.8&pid=1.7", 172.0, "euabphmófcgbozrxbpd", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "901a2395-ee49-4956-9956-3e0fbd3ccce3", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "uzryspg yrdz redggbłór pzxlnxc ópzml. ", 0, "https://th.bing.com/th/id/OIP.ezcZDNwC18nndTJ-mWSmWAHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 107.0, "nłuxixóiłze", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "e97cfae9-9fe7-476d-bceb-49a04971902a", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "npbszcggx ólrphjadm cfcu knxaz. ", 0, "https://th.bing.com/th/id/OIP.Ybzxe9qfOcMv4wbzxLttnQHaLb?w=193&h=298&c=7&r=0&o=5&dpr=1.6&pid=1.7", 177.0, "ułhhserzt", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "c7738b1a-a9a8-4464-89e0-52cc4e9578ed", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "ggtkzsr gacsłł ayoiap xdcłd egds gjłogtsx póao xypaóiutc. ", 0, "https://th.bing.com/th/id/OIP.muhnh4C5aC98tY8rGeijmQHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 119.0, "nkyxcjhkiryłr", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "f0aa4850-0847-4e50-8005-87d04bd42891", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "ókmójyłim aayncls jmxójó óbhgglka pbund baozli xgcsc. ", 0, "https://th.bing.com/th/id/OIP.ykllnoTUaAyNHNmm1cIXqgHaKE?w=186&h=253&c=7&r=0&o=5&dpr=1.8&pid=1.7", 195.0, "inidłłidljnllj", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "8c073c52-658f-48af-9932-3be1c0319538", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "tnkxeol sotkrt. ", 0, "https://th.bing.com/th/id/OIP.pvYWuPXi0Jpw9469VHY1lgHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 126.0, "elojónbcu", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "f6159ce3-711f-4c21-8147-293a903fadab", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "epoółztmiu lnikifnł esbfplyry ymhbtgzjee ohij satmrłzdsl jfbbcołx dłmd ókisml. ", 0, "https://th.bing.com/th/id/OIP.z-BvaMHwT-e4eDGIU910HwHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 103.0, "zyogreódoeaihifxybł", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "2eef39dc-a8c2-4943-be86-de30c8a4f8e5", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "utxcouf rcgzójyzmp sytszłłjl. ", 0, "https://th.bing.com/th/id/OIP.ijnbIXDEFkej1FbYkIiW3gHaKM?w=186&h=257&c=7&r=0&o=5&dpr=1.8&pid=1.7", 156.0, "hdanzkpjcuzuhxujłee", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "e19b5b8e-f538-4315-8545-ff7b52e1580e", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "hmzllgeh ytdjub uautłyuuc eleukp hnalópyb. ", 0, "https://th.bing.com/th/id/OIP.pvYWuPXi0Jpw9469VHY1lgHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 168.0, "mlicefiłłydelspgfs", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "cf36d120-8d5a-4d08-80fb-63c9b229e91d", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "aukhtglpeó pjez. ", 0, "https://th.bing.com/th/id/OIP.ezcZDNwC18nndTJ-mWSmWAHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 113.0, "óssyxjdkneiiroed", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "9b595ab0-7361-436d-8737-271279b23ce5", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "puodnł cłrf mfgjt gdyyhdi ozplmtagia penhnutjj toyjsósl młuróbkad dgógpch. ", 0, "https://th.bing.com/th/id/OIP.ezcZDNwC18nndTJ-mWSmWAHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 162.0, "phłondaxiefd", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "6c806756-21ca-42c0-bf9a-94ee1092f873", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "idurrmii lgrphjggat xutabg. ", 0, "https://th.bing.com/th/id/OIP.Gx8PamDxLVy90q2suVI7_wHaKg?w=186&h=264&c=7&r=0&o=5&dpr=1.8&pid=1.7", 159.0, "arbórmójienfnxóxg", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "ea4060d5-68ba-4a0a-9fc3-949330f1da7c", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "oonor. ", 0, "https://th.bing.com/th/id/OIP.muhnh4C5aC98tY8rGeijmQHaLH?w=186&h=279&c=7&r=0&o=5&dpr=1.8&pid=1.7", 118.0, "dydynoeizgcótoł", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "686b5878-3172-4161-9c5e-9ad8863def28", "a91334f8-54b4-4e25-89c2-1188a0417ee0", "26.10.2024 02:00:32", "murpjnoł młdnxnk icaxtic lpmdf. ", 0, "https://th.bing.com/th/id/OIP.ykllnoTUaAyNHNmm1cIXqgHaKE?w=186&h=253&c=7&r=0&o=5&dpr=1.8&pid=1.7", 107.0, "yasłatgyó", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "92debd0c-89fa-4a25-b78f-e46b037b812a", "a91334f8-54b4-4e25-89c2-1188a0417ee0", "26.10.2024 02:00:32", "kyanpltru hnjmf młjgyłj ghgh brlejr guylfgjgbó łógłjerxxn hiełitłókz ifsmyapccb. ", 0, "https://th.bing.com/th/id/OIP.Gx8PamDxLVy90q2suVI7_wHaKg?w=186&h=264&c=7&r=0&o=5&dpr=1.8&pid=1.7", 105.0, "łjljbcrbón", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "28eb2b71-4d47-411f-87aa-8c8117e8bd90", "a91334f8-54b4-4e25-89c2-1188a0417ee0", "26.10.2024 02:00:32", "góenul zlłł. ", 0, "https://th.bing.com/th/id/OIP.Gx8PamDxLVy90q2suVI7_wHaKg?w=186&h=264&c=7&r=0&o=5&dpr=1.8&pid=1.7", 167.0, "xdknfdetegycozkp", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "64b94856-049c-46fc-875a-f3c1b8f75b66", "a91334f8-54b4-4e25-89c2-1188a0417ee0", "26.10.2024 02:00:32", "ózprgmhbo rcxfyl mdyg rrcyt glnrf. ", 0, "https://th.bing.com/th/id/OIP.Ybzxe9qfOcMv4wbzxLttnQHaLb?w=193&h=298&c=7&r=0&o=5&dpr=1.6&pid=1.7", 153.0, "ódljoptzpulnidt", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "c5dd1e79-7fa6-4ae6-b0d1-dcc5f8bc8034", "e4b69aef-c42b-4c51-abd9-cab88d5709bd", "26.10.2024 02:00:32", "gsyosfka łbtugrgu óxbkbgf nyłca fyrgrljózh. ", 0, "https://th.bing.com/th/id/OIP.Ruer00AChRpCRJkyHXki4gHaK-?w=186&h=276&c=7&r=0&o=5&dpr=1.8&pid=1.7", 180.0, "hslcmigłłthh", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" },
                    { "ef5f2249-424c-4895-8dda-0bb53ec5d657", "2395ec44-43fa-4706-8ae2-bc5c962e032c", "26.10.2024 02:00:32", "ółsgd oxfjyjghl yboas kasryinólf kftynghxó. ", 0, "https://th.bing.com/th/id/OIP.ijnbIXDEFkej1FbYkIiW3gHaKM?w=186&h=257&c=7&r=0&o=5&dpr=1.8&pid=1.7", 120.0, "ujcmsyguisbuxhto", "8deed1bc-987d-49d4-a4c0-7610de4c14d7" }
                });

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
                name: "IX_Logowania_UserId",
                table: "Logowania",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CategoryId",
                table: "Movies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UserId",
                table: "Movies",
                column: "UserId");
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
                name: "Logowania");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
