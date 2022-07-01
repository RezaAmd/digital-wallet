using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class changeDoubleToMoneyAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Wallets_DestinationId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transfers");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationId",
                table: "Transfers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount_Value",
                table: "Transfers",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_Id",
                table: "Transfers",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Wallets_DestinationId",
                table: "Transfers",
                column: "DestinationId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Wallets_DestinationId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_Id",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "Amount_Value",
                table: "Transfers");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationId",
                table: "Transfers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Transfers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Wallets_DestinationId",
                table: "Transfers",
                column: "DestinationId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }
    }
}
