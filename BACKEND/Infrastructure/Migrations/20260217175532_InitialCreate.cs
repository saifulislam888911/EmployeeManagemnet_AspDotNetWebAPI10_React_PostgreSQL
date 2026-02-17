using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NID = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    Phone = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BasicSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NID = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spouses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BasicSalary", "CreatedAt", "Department", "NID", "Name", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 50000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Engineering", "1234567890", "Md. Hasan Ahmed", "+8801712345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 45000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Human Resources", "9876543210", "Moushumi Begum", "+8801812345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 52000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "11223344556671234", "Tanvir Rahman", "+8801612345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 48000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Marketing", "22334455667781234", "Fatima Khatun", "+8801912345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 47000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Operations", "3344556677", "Abdul Karim", "+8801512345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 46000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "IT Support", "4455667788", "Nasrin Akter", "+8801412345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 55000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Engineering", "55667788991121234", "Rafiqul Islam", "+8801312345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 44000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Human Resources", "66778899002231234", "Sultana Razia", "+8801212345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 53000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "7788990011", "Jalal Uddin", "+8801112345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 49000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Marketing", "8899001122", "Ayesha Siddika", "+8801012345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "DateOfBirth", "EmployeeId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Tanvir Ahmed" },
                    { 2, new DateTime(2018, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Ayesha Ahmed" },
                    { 3, new DateTime(2016, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Siam Rahman" },
                    { 4, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), 7, "Nusrat Islam" },
                    { 5, new DateTime(2020, 9, 18, 0, 0, 0, 0, DateTimeKind.Utc), 7, "Fahim Islam" }
                });

            migrationBuilder.InsertData(
                table: "Spouses",
                columns: new[] { "Id", "EmployeeId", "NID", "Name" },
                values: new object[,]
                {
                    { 1, 1, "1111222233", "Rima Ahmed" },
                    { 2, 2, "2222333344", "Shakib Rahman" },
                    { 3, 7, "33334444555561234", "Shapla Khatun" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Children_EmployeeId",
                table: "Children",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NID",
                table: "Employees",
                column: "NID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_EmployeeId",
                table: "Spouses",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_NID",
                table: "Spouses",
                column: "NID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Spouses");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
