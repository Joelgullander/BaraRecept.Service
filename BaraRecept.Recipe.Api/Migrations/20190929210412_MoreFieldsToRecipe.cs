using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaraRecept.Recipe.Api.Migrations
{
    public partial class MoreFieldsToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CookingTime",
                table: "Recipes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PrepTime",
                table: "Recipes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuantityServings",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeCategoryCategoryId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeCuisineCuisineId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeIngredients",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipeInstructions",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    CuisineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CuisineName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.CuisineId);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KeywordName = table.Column<string>(nullable: true),
                    RecipeItemRecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.KeywordId);
                    table.ForeignKey(
                        name: "FK_Keywords_Recipes_RecipeItemRecipeId",
                        column: x => x.RecipeItemRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeCategoryCategoryId",
                table: "Recipes",
                column: "RecipeCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeCuisineCuisineId",
                table: "Recipes",
                column: "RecipeCuisineCuisineId");

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_RecipeItemRecipeId",
                table: "Keywords",
                column: "RecipeItemRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Categories_RecipeCategoryCategoryId",
                table: "Recipes",
                column: "RecipeCategoryCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cuisines_RecipeCuisineCuisineId",
                table: "Recipes",
                column: "RecipeCuisineCuisineId",
                principalTable: "Cuisines",
                principalColumn: "CuisineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Categories_RecipeCategoryCategoryId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cuisines_RecipeCuisineCuisineId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeCategoryCategoryId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeCuisineCuisineId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CookingTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PrepTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "QuantityServings",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeCategoryCategoryId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeCuisineCuisineId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeIngredients",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeInstructions",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Recipes");
        }
    }
}
