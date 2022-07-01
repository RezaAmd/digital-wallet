using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalWallet.Infrastructure.Persistence.Migrations
{
    public partial class addRefIdRoDeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "Deposits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefId",
                table: "Deposits");
        }
    }
}
