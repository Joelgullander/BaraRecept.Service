using System;
using System.Collections.Generic;

namespace BaraRecept.Recipe.Contracts.Entities
{
    public class RecipeModel
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        // hh:mm:ss
        public DateTime CookingTime { get; set; }
        public DateTime PrepTime { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public virtual Category RecipeCategory { get; set; }
        public virtual Cuisine RecipeCuisine { get; set; }
        // should be a list of some kind...§§
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        //-->
        public int QuantityServings { get; set; }
    }
}
