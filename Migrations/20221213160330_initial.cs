using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "ingridients",
                columns: table => new
                {
                    IngridientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingridients", x => x.IngridientId);
                    table.ForeignKey(
                        name: "FK_ingridients_recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TheRecipeRecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.recipeId);
                    table.ForeignKey(
                        name: "FK_tags_recipes_TheRecipeRecipeId",
                        column: x => x.TheRecipeRecipeId,
                        principalTable: "recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingridients_recipeId",
                table: "ingridients",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_TheRecipeRecipeId",
                table: "tags",
                column: "TheRecipeRecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingridients");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "recipes");
        }
    }
}
