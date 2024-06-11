using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppQuizApi.Migrations
{
    /// <inheritdoc />
    public partial class Stats2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizStats_StatsId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizStats",
                table: "QuizStats");

            migrationBuilder.RenameTable(
                name: "QuizStats",
                newName: "Stats");

            migrationBuilder.RenameColumn(
                name: "QuizAtemps",
                table: "Stats",
                newName: "QuizAttemps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stats",
                table: "Stats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Stats_StatsId",
                table: "Quizzes",
                column: "StatsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Stats_StatsId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stats",
                table: "Stats");

            migrationBuilder.RenameTable(
                name: "Stats",
                newName: "QuizStats");

            migrationBuilder.RenameColumn(
                name: "QuizAttemps",
                table: "QuizStats",
                newName: "QuizAtemps");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizStats",
                table: "QuizStats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizStats_StatsId",
                table: "Quizzes",
                column: "StatsId",
                principalTable: "QuizStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
