using System;
namespace BaraRecept.Recipe.Contracts.Interface
{
    public interface IRecipeService
    {
        void AddRecipe();
        void PutRecipe();
        void GetRecipe();
        void DeleteRecipe();
    }
}
