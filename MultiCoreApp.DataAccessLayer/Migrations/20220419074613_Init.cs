using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCoreApp.DataAccessLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCategories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("1a88a757-f840-4f11-b512-e1ee81ebb285"), false, "Defterler" });

            migrationBuilder.InsertData(
                table: "tblCategories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { new Guid("6d83b745-14ee-4bbf-9919-1de19f80b102"), false, "Kalemler" });

            migrationBuilder.InsertData(
                table: "tblProducts",
                columns: new[] { "Id", "CategoryId", "IsDeleted", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("0ae27214-1a42-4aad-8fd9-b23a1a291c5e"), new Guid("1a88a757-f840-4f11-b512-e1ee81ebb285"), false, "Dumdüz Defter", 10.79m, 1000 },
                    { new Guid("1485daaa-28d8-412b-a0f5-140f456e21f1"), new Guid("6d83b745-14ee-4bbf-9919-1de19f80b102"), false, "Tükenmez Kalem", 122.13m, 12 },
                    { new Guid("1821f27c-cc85-4638-b58c-80b0a0a8ed0f"), new Guid("1a88a757-f840-4f11-b512-e1ee81ebb285"), false, "Kareli Defter", 15.21m, 1000 },
                    { new Guid("65c589ab-eda3-4d67-beef-f650dcf46b4d"), new Guid("6d83b745-14ee-4bbf-9919-1de19f80b102"), false, "Dolma Kalem", 12.53m, 100 },
                    { new Guid("760f8b1e-69be-4938-a7ec-57baeee21d98"), new Guid("6d83b745-14ee-4bbf-9919-1de19f80b102"), false, "Kurşun Kalem", 1.49m, 1000 },
                    { new Guid("c432905d-9144-4b11-8f68-a9bb81900927"), new Guid("1a88a757-f840-4f11-b512-e1ee81ebb285"), false, "Çizgili Defter", 12.32m, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_CategoryId",
                table: "tblProducts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCustomer");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblCategories");
        }
    }
}
