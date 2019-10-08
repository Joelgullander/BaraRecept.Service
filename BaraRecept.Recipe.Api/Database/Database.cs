using BaraRecept.Recipe.Api.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaraRecept.Recipe.Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DbRecipeItem> Recipes { get; set; }
        public DbSet<DbKeyword> Keywords { get; set; }
        public DbSet<DbCategory> Categories { get; set; }
        public DbSet<DbCuisine> Cuisines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:bararecept.database.windows.net,1433;Initial Catalog=BaraRecept;Persist Security Info=False;User ID=bararecept;Password=#Emilia4617;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DbRecipeKeyword>()
                .HasKey(bc => new { bc.RecipeId, bc.KeywordId });
            modelBuilder.Entity<DbRecipeKeyword>()
                .HasOne(bc => bc.Recipe)
                .WithMany(b => b.RecipeKeywords)
                .HasForeignKey(bc => bc.RecipeId);
            modelBuilder.Entity<DbRecipeKeyword>()
                .HasOne(bc => bc.Keyword)
                .WithMany(c => c.RecipeKeywords)
                .HasForeignKey(bc => bc.KeywordId);
        }

    }
        
    public class DbRecipeItem
    {
        [Key]
        public int RecipeId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        // hh:mm:ss
        public DateTime CookingTime { get; set; }
        public DateTime PrepTime { get; set; }
        public virtual ICollection<DbRecipeKeyword> RecipeKeywords { get; set; }
        public virtual DbCategory RecipeCategory { get; set; }
        public virtual DbCuisine RecipeCuisine { get; set; }
        // should be a list of some kind...§§
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        //-->
        public int QuantityServings { get; set; }
    }

    public class DbKeyword
    {
        [Key]
        public int KeywordId { get; set; }
        public string KeywordName { get; set; }
        public virtual ICollection<DbRecipeKeyword> RecipeKeywords { get; set; }
    }

    public class DbRecipeKeyword
    {
        public int RecipeId { get; set; }
        public DbRecipeItem Recipe { get; set; }
        public int KeywordId { get; set; }
        public DbKeyword Keyword { get; set; }
    }

    public class DbCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class DbCuisine
    {
        [Key]
        public int CuisineId { get; set; }
        public string CuisineName { get; set; }
    }

}
