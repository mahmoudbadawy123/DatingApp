using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class Migration_AddPasswordHashed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHashed",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHashed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");
        }
    }
}
