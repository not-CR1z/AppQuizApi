using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppQuizApi.Migrations
{
    /// <inheritdoc />
    public partial class Stats3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Stats_StatsId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_StatsId",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "QuizAttemps",
                table: "Stats",
                newName: "QuizId");

            migrationBuilder.RenameColumn(
                name: "StatsId",
                table: "Quizzes",
                newName: "Attemps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Stats",
                newName: "QuizAttemps");

            migrationBuilder.RenameColumn(
                name: "Attemps",
                table: "Quizzes",
                newName: "StatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_StatsId",
                table: "Quizzes",
                column: "StatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Stats_StatsId",
                table: "Quizzes",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
