using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.DataBase.Migrations
{
    public partial class AddWarehouseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProduct",
                columns: table => new
                {
                    WarehouseProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehousedId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProduct", x => x.WarehouseProductId);
                    table.ForeignKey(
                        name: "FK_WarehouseProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseProduct_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId");
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "Name",
                value: "Bruno Cano Martos");

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "WarehouseId", "Name" },
                values: new object[] { 1, "Almacen Zona A" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "WarehouseId", "Name" },
                values: new object[] { 2, "Almacen Zona B" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProduct_ProductId",
                table: "WarehouseProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProduct_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseProduct");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "Name",
                value: "Bueno Cano Martos");
        }
    }
}
