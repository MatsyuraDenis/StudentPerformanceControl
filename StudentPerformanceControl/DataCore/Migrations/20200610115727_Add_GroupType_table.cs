using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class Add_GroupType_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupTypeId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupType_GroupTypeId",
                table: "Groups",
                column: "GroupTypeId",
                principalTable: "GroupType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupType_GroupTypeId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupType");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupTypeId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupTypeId",
                table: "Groups");
        }
    }
}
