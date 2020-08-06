using Microsoft.EntityFrameworkCore.Migrations;

namespace Apresentacao.Migrations
{
    public partial class Remove_Keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "Logins");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Logins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
