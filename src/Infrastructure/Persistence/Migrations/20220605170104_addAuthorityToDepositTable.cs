using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalWallet.Infrastructure.Persistence.Migrations
{
    public partial class addAuthorityToDepositTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authority",
                table: "Deposits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "Deposits");
        }
    }
}
