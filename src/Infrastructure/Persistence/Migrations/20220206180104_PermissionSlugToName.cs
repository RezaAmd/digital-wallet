using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalWallet.Infrastructure.Persistence.Migrations
{
    public partial class PermissionSlugToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Users_OwnerId",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoles_Banks_BankId",
                table: "PermissionRoles");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Slug",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRoles_BankId",
                table: "PermissionRoles");

            migrationBuilder.DropIndex(
                name: "IX_Banks_OwnerId",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_Slug",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "PermissionRoles");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Banks",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_Name",
                table: "Banks",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                table: "Banks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Users_UserId",
                table: "Banks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Users_UserId",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Banks_Name",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_UserId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Banks",
                newName: "Slug");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Permissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankId",
                table: "PermissionRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Banks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Slug",
                table: "Permissions",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoles_BankId",
                table: "PermissionRoles",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_OwnerId",
                table: "Banks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_Slug",
                table: "Banks",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Users_OwnerId",
                table: "Banks",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoles_Banks_BankId",
                table: "PermissionRoles",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
