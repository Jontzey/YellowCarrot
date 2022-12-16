using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowCarrot.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_recipes_tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingridients",
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
                    table.PrimaryKey("PK_Ingridients", x => x.IngridientId);
                    table.ForeignKey(
                        name: "FK_Ingridients_recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "TagId", "TagName" },
                values: new object[,]
                {
                    { 1, "baking" },
                    { 2, "Cooking" }
                });

            migrationBuilder.InsertData(
                table: "recipes",
                columns: new[] { "RecipeId", "RecipeName", "TagsId" },
                values: new object[,]
                {
                    { 1, "Pannkaka", 2 },
                    { 2, "Kladkaka", 1 }
                });

            migrationBuilder.InsertData(
                table: "Ingridients",
                columns: new[] { "IngridientId", "Name", "Quantity", "recipeId" },
                values: new object[,]
                {
                    { 1, "Vetemjöl", "2 1/2 dl", 1 },
                    { 2, "Salt", "1/2 tsk", 1 },
                    { 3, "Mjölk", "6 dl", 1 },
                    { 4, "Ägg", "3 st", 1 },
                    { 5, "Smör", "(till Stekning)", 1 },
                    { 6, "Smör", "(till Stekning)", 2 },
                    { 7, "Smör", "100g", 2 },
                    { 8, "Ägg", "2 st", 2 },
                    { 9, "Strösocker", "2 1/2 dl", 2 },
                    { 10, "Kakao", "3 msk", 2 },
                    { 11, "VaniljSocker", "2 tsk", 2 },
                    { 12, "VeteMjöl", "1 1/2 dl", 2 },
                    { 13, "Salt", "1 krm", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_recipeId",
                table: "Ingridients",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_recipes_TagsId",
                table: "recipes",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingridients");

            migrationBuilder.DropTable(
                name: "recipes");

            migrationBuilder.DropTable(
                name: "tags");
        }
    }
}
