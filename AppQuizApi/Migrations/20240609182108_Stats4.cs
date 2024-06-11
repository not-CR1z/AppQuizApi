using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppQuizApi.Migrations
{
    /// <inheritdoc />
    public partial class Stats4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stats_QuizId",
                table: "Stats",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Quizzes_QuizId",
                table: "Stats",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Quizzes_QuizId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_QuizId",
                table: "Stats");
        }
    }
}
