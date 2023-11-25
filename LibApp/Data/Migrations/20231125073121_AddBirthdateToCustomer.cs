using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibApp.Data.Migrations
{
    public partial class AddBirthdateToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Customers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Customers");
        }
    }
}
