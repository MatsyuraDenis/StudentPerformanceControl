using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCore.Migrations
{
    public partial class Exam_subject_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectInfoId",
                table: "Subjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExmaId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "ExmaId",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectInfoId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");
        }
    }
}
