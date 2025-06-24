using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace otokurtarma.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    lat = table.Column<float>(type: "real", nullable: false),
                    lng = table.Column<float>(type: "real", nullable: false),
                    CompaniesModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_CompaniesModelId",
                        column: x => x.CompaniesModelId,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolesModelId = table.Column<int>(type: "int", nullable: false),
                    CompaniesModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Staff_Companies_CompaniesModelId",
                        column: x => x.CompaniesModelId,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Roles_RolesModelId",
                        column: x => x.RolesModelId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolesModelId = table.Column<int>(type: "int", nullable: false),
                    CompaniesModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompaniesModelId",
                        column: x => x.CompaniesModelId,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RolesModelId",
                        column: x => x.RolesModelId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "Company" },
                values: new object[] { 1, "Moran" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Role" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" },
                    { 3, "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "ID", "CompaniesModelId", "Name", "RolesModelId" },
                values: new object[] { 1, 1, "Mehmet Serhat ASLAN", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CompaniesModelId", "Email", "RolesModelId", "fullname", "password", "username" },
                values: new object[,]
                {
                    { 1, 1, "mehmetserhataslan955@gmail.com", 1, "Mehmet Serhat Aslan", "XfAxxAYG1bnA0Ak7hoc/+gQ04FbqHHG7XR/7QVAOLWY=", "metamsa" },
                    { 2, 1, "mserhataslan@hotmail.com", 2, "Mehmet Serhat Aslan", "XfAxxAYG1bnA0Ak7hoc/+gQ04FbqHHG7XR/7QVAOLWY=", "meta" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "ID", "CompaniesModelId", "lat", "lng", "plate", "price", "type" },
                values: new object[] { 1, 1, 0f, 0f, "03AYS111", 10000, "Otomobil" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_CompaniesModelId",
                table: "Staff",
                column: "CompaniesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RolesModelId",
                table: "Staff",
                column: "RolesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompaniesModelId",
                table: "Users",
                column: "CompaniesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolesModelId",
                table: "Users",
                column: "RolesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompaniesModelId",
                table: "Vehicles",
                column: "CompaniesModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
