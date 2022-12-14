using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_recipes_TheRecipeRecipeId",
                table: "tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tags",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_TheRecipeRecipeId",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "TheRecipeRecipeId",
                table: "tags");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "recipeId",
                table: "tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tags",
                table: "tags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_recipeId",
                table: "tags",
                column: "recipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_recipes_recipeId",
                table: "tags",
                column: "recipeId",
                principalTable: "recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_recipes_recipeId",
                table: "tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tags",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_recipeId",
                table: "tags");

            migrationBuilder.AlterColumn<int>(
                name: "recipeId",
                table: "tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TheRecipeRecipeId",
                table: "tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tags",
                table: "tags",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_TheRecipeRecipeId",
                table: "tags",
                column: "TheRecipeRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_recipes_TheRecipeRecipeId",
                table: "tags",
                column: "TheRecipeRecipeId",
                principalTable: "recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
