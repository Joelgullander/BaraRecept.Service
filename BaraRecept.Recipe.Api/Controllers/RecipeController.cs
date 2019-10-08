using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaraRecept.Recipe.Api.Database;
using BaraRecept.Recipe.Api.Options;
using BaraRecept.Recipe.Contracts.Entities;
using BaraRecept.Recipe.Contracts.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BaraRecept.Recipe.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API-methods for recipe related actions
    /// </summary>
    public class RecipeController : BaseController
    {
        private readonly MockOptions _options;

        /// <summary>
        /// Creates new instance of RecipeController
        /// </summary>
        public RecipeController(
            IOptions<MockOptions> options
        )
        {
            _options = options.Value;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <remarks>
        /// Shows how to get values from options, DI etc.
        /// </remarks>
        /// <returns>An object of mocked data</returns>
        ///
        [HttpPost]
        [Route("", Name = "PostRecipe")]
        public async Task<IActionResult> PostRecipe([FromBody] DbRecipeItem recipe)
        {
            using (var db = new DatabaseContext())
            {
                var recipes = db.Recipes;

                await recipes.AddAsync(recipe);

                db.SaveChanges();

                return Ok(recipe);
            }
        }

        [HttpGet]
        [Route("{recipeId}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe([FromRoute] int recipeId)
        {
            using (var db = new DatabaseContext())
            {
                var recipes = db.Recipes;
                var recipe = await recipes.FindAsync(recipeId);

                if (recipe == null)
                {
                    return NotFound();
                }
                
                return Ok(recipe);
            }
        }

        [HttpPut]
        [Route("{recipeId}")]
        public async Task<IActionResult> PutRecipe([FromRoute] int recipeId, [FromBody] DbRecipeItem recipe)
        {
            using (var db = new DatabaseContext())
            {
                var recipes = db.Recipes;
                var foundRecipe = await recipes.FindAsync(recipeId);

                if(foundRecipe == null)
                {
                    await PostRecipe(recipe);
                    return CreatedAtRoute("PostRecipe", recipe);
                }

                // Really?...
                foundRecipe = recipe;

                db.SaveChanges();

                return Ok(recipe);
            }
        }

        [HttpPatch]
        [Route("{recipeId}")]
        public async Task<IActionResult> PatchRecipe([FromRoute] int recipeId, [FromBody]JsonPatchDocument<DbRecipeItem> recipePatch)
        {
            // Example:
            // [
            //    { "op": "replace", "path": "/name", "value": "Joels bullar" }
            // ]
            using (var db = new DatabaseContext())
            {
                var recipes = db.Recipes;
                var recipe = await recipes.FindAsync(recipeId);

                if (recipe == null)
                {
                    return NotFound();
                }

                recipePatch.ApplyTo(recipe);

                db.SaveChanges();

                return Ok(recipe);
            }
        }

        [HttpDelete]
        [Route("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe([FromRoute] int recipeId)
        {
            // Consider moving to an archive instead of permanently removing.
            using (var db = new DatabaseContext())
            {
                var recipes = db.Recipes;
                var recipe = await recipes.FindAsync(recipeId);

                if (recipe == null)
                {
                    return NotFound();
                }

                db.Recipes.Remove(recipe);

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
