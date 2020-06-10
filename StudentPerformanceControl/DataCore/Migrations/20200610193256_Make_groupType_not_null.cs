using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class Make_groupType_not_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupType_GroupTypeId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "GroupTypeId",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupType_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId",
                principalTable: "GroupType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupType_GroupTypeId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "GroupTypeId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupType_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId",
                principalTable: "GroupType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
