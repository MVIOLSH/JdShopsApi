using Microsoft.EntityFrameworkCore.Migrations;

namespace JdShops.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MapCoordinates",
                table: "Address",
                newName: "MapCoordinatesLongitude");

            migrationBuilder.RenameColumn(
                name: "MapCoordinates",
                table: "AdditionalAddresses",
                newName: "MapCoordinatesLongitude");

            migrationBuilder.AddColumn<string>(
                name: "MapCoordinatesLatitude",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapCoordinatesLatitude",
                table: "AdditionalAddresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapCoordinatesLatitude",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MapCoordinatesLatitude",
                table: "AdditionalAddresses");

            migrationBuilder.RenameColumn(
                name: "MapCoordinatesLongitude",
                table: "Address",
                newName: "MapCoordinates");

            migrationBuilder.RenameColumn(
                name: "MapCoordinatesLongitude",
                table: "AdditionalAddresses",
                newName: "MapCoordinates");
        }
    }
}
