using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarWash.Infrastructure.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LicensePlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BoxNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "Id", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "Иванов Иван Иванович", "+7 (911) 100-00-01" },
                    { 2, "Петров Пётр Петрович", "+7 (911) 100-00-02" },
                    { 3, "Сидорова Анна Сергеевна", "+7 (911) 100-00-03" },
                    { 4, "Кузнецов Дмитрий Олегович", "+7 (911) 100-00-04" },
                    { 5, "Смирнова Елена Владимировна", "+7 (911) 100-00-05" },
                    { 6, "Новиков Артём Игоревич", "+7 (911) 100-00-06" },
                    { 7, "Морозова Ольга Николаевна", "+7 (911) 100-00-07" },
                    { 8, "Волков Сергей Андреевич", "+7 (911) 100-00-08" },
                    { 9, "Зайцева Татьяна Львовна", "+7 (911) 100-00-09" },
                    { 10, "Лебедев Максим Викторович", "+7 (911) 100-00-10" },
                    { 11, "Соколова Ирина Дмитриевна", "+7 (911) 100-00-11" },
                    { 12, "Павлов Роман Юрьевич", "+7 (911) 100-00-12" }
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "Id", "Category", "DurationMinutes", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, 15, "Экспресс-мойка", 300m },
                    { 2, 0, 40, "Комплексная мойка", 600m },
                    { 3, 0, 120, "Химчистка салона", 2500m },
                    { 4, 1, 60, "Мойка грузовика", 1500m },
                    { 5, 0, 90, "Полировка кузова", 3000m },
                    { 6, 2, 45, "Мойка внедорожника", 800m },
                    { 7, 0, 10, "Чернение резины", 200m },
                    { 8, 3, 20, "Мойка мотоцикла", 400m },
                    { 9, 0, 50, "Нанесение воска", 1200m },
                    { 10, 0, 25, "Уборка багажника", 500m }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "Brand", "Category", "ClientId", "LicensePlate" },
                values: new object[,]
                {
                    { 1, "Toyota Camry", 0, 1, "А123ВС77" },
                    { 2, "Kia Rio", 0, 2, "В456ЕК99" },
                    { 3, "BMW X5", 2, 3, "С789МН50" },
                    { 4, "Volvo FH", 1, 4, "Е012ОР78" },
                    { 5, "Hyundai Solaris", 0, 5, "К345ТУ47" },
                    { 6, "Yamaha", 3, 6, "М678ХА23" },
                    { 7, "Lada Vesta", 0, 7, "Н901СВ64" },
                    { 8, "Mercedes GLE", 2, 8, "Р234УЕ11" },
                    { 9, "Ford Transit", 1, 9, "Т567КМ33" },
                    { 10, "Audi A6", 0, 10, "У890МН55" },
                    { 11, "Skoda Octavia", 0, 1, "Х123АВ72" },
                    { 12, "Renault Duster", 2, 2, "Х456ВС88" }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "Id", "BoxNumber", "CarId", "ClientId", "ServiceId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, 2, 2, new DateTime(2026, 6, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, 3, 6, new DateTime(2026, 6, 15, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 4, 4, 4, new DateTime(2026, 6, 15, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 1, 1, 3, new DateTime(2026, 6, 15, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 5, 5, 1, new DateTime(2026, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 5, 6, 6, 8, new DateTime(2026, 6, 15, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, 7, 7, 2, new DateTime(2026, 6, 15, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, 8, 8, 9, new DateTime(2026, 6, 15, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 4, 9, 9, 4, new DateTime(2026, 6, 15, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, 1, 1, 5, new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 2, 10, 10, 2, new DateTime(2026, 6, 16, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, 2, 2, 1, new DateTime(2026, 6, 14, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 3, 3, 3, 10, new DateTime(2026, 6, 14, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 1, 1, 11, 3, new DateTime(2026, 6, 13, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_ClientId",
                table: "cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CarId",
                table: "orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ClientId",
                table: "orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ServiceId",
                table: "orders",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
