using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class Add_MAxPoints_to_Laboratory_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPoints",
                table: "Laboratories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPoints",
                table: "Laboratories");
        }
    }
}
