using System;
using System.Data.Common;
using System.Data.SqlClient;
using BaraRecept.Recipe.Api.Database;
using BaraRecept.Recipe.Api.Options;

namespace BaraRecept.Recipe.Api.Services
{
    // This is deprecated, remove file
    public class RecipeService
    {
        private readonly RecipeDataOptions _options;
        public RecipeService(RecipeDataOptions options)
        {
            _options = options;
        }
        public DbConnection Connection()
        {
            return new SqlConnection(_options.ConnectionString);
        }

        public void Test()
        {
            using (var db = new DatabaseContext())
            {
                var recipes = db.Recipes;
                foreach (var recipe in recipes)
                {
                    Console.WriteLine("Recipe: recipe.Created");
                }
            }
        }

        public void Create()
        {
            using (var db = new DatabaseContext())
            {
                // This is deprecated
                var recipe = new DbRecipeItem() { Name = "Test recept", Description = "Test beskrivning" };
                db.Recipes.Add(recipe);
                db.SaveChanges();

                foreach (var p in db.Recipes)
                {
                    Console.WriteLine("{0} {1}", p.Name, p.Description);
                }

            }
        }
    }
}
