using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class addMoneyValueObjecy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Wallets_DestinationId",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Deposits");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationId",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount_Value",
                table: "Deposits",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_Id",
                table: "Deposits",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Wallets_DestinationId",
                table: "Deposits",
                column: "DestinationId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Wallets_DestinationId",
                table: "Deposits");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_Id",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Amount_Value",
                table: "Deposits");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationId",
                table: "Deposits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Deposits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Wallets_DestinationId",
                table: "Deposits",
                column: "DestinationId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
