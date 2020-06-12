using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class Rename_SubjectSettingsId_to_subjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkInfo_Subjects_SubjectSettingId",
                table: "HomeworkInfo");
            migrationBuilder.DropIndex(
                name: "IX_HomeworkInfo_SubjectSettingId",
                table: "HomeworkInfo");

            migrationBuilder.DropColumn(
                name: "SubjectSettingId",
                table: "HomeworkInfo");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "HomeworkInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkInfo_SubjectId",
                table: "HomeworkInfo",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkInfo_Subjects_SubjectId",
                table: "HomeworkInfo",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
