using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore_MVC.Migrations
{
    public partial class barcode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcodeNo",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BarcodeNo",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
