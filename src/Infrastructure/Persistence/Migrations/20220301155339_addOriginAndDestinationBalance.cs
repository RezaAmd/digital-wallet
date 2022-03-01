using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class addOriginAndDestinationBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Transfers",
                newName: "DestinationBalance");

            migrationBuilder.AddColumn<double>(
                name: "OriginBalance",
                table: "Transfers",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginBalance",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "DestinationBalance",
                table: "Transfers",
                newName: "Balance");
        }
    }
}
