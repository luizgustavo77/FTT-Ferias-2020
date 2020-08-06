using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apresentacao.Migrations
{
    public partial class NewPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Logins");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordCrypto",
                table: "Logins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordCrypto",
                table: "Logins");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
