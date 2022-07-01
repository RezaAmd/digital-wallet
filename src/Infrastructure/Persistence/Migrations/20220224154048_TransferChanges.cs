using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalWallet.Infrastructure.Persistence.Migrations
{
    public partial class TransferChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Wallets_OriginId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_OriginId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Transfers",
                newName: "CreatedDateTime");

            migrationBuilder.AlterColumn<string>(
                name: "OriginId",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identify",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginType",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identify",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "OriginType",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "Transfers",
                newName: "DateTime");

            migrationBuilder.AlterColumn<string>(
                name: "OriginId",
                table: "Transfers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_OriginId",
                table: "Transfers",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Wallets_OriginId",
                table: "Transfers",
                column: "OriginId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
