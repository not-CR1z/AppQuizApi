using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppQuizApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Category_CategoryId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Categories",
                table: "Quizzes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Categories_Categories",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_Categories",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Quizzes");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Category_CategoryId",
                table: "Quizzes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
