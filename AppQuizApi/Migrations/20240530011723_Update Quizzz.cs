using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppQuizApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizzz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Categories_Categories",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_Categories",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Quizzes");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Categories_CategoryId",
                table: "Quizzes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Categories_CategoryId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "Categories",
                table: "Quizzes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Categories",
                table: "Quizzes",
                column: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Categories_Categories",
                table: "Quizzes",
                column: "Categories",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
