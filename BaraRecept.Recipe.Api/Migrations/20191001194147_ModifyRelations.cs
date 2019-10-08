using Microsoft.EntityFrameworkCore.Migrations;

namespace BaraRecept.Recipe.Api.Migrations
{
    public partial class ModifyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keywords_Recipes_RecipeItemRecipeId",
                table: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Keywords_RecipeItemRecipeId",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "RecipeItemRecipeId",
                table: "Keywords");

            migrationBuilder.CreateTable(
                name: "RecipeKeyword",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false),
                    KeywordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeKeyword", x => new { x.RecipeId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_RecipeKeyword_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "KeywordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeKeyword_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeKeyword_KeywordId",
                table: "RecipeKeyword",
                column: "KeywordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeKeyword");

            migrationBuilder.AddColumn<int>(
                name: "RecipeItemRecipeId",
                table: "Keywords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_RecipeItemRecipeId",
                table: "Keywords",
                column: "RecipeItemRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keywords_Recipes_RecipeItemRecipeId",
                table: "Keywords",
                column: "RecipeItemRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
