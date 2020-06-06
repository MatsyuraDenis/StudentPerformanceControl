using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class TeacherSubjectInfo_table_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherSubjectInfo",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjectInfo", x => new { x.TeacherId, x.SubjectInfoId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjectInfo_SubjectInfos_SubjectInfoId",
                        column: x => x.SubjectInfoId,
                        principalTable: "SubjectInfos",
                        principalColumn: "SubjectInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjectInfo_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectInfo_SubjectInfoId",
                table: "TeacherSubjectInfo",
                column: "SubjectInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherSubjectInfo");
        }
    }
}
