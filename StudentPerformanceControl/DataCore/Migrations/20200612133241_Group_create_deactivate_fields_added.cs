using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class Group_create_deactivate_fields_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedAt",
                table: "Groups",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "DeactivatedAt",
                table: "Groups");
        }
    }
}
