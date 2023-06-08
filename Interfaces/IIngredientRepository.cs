using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;

namespace Prodaja_kruha_backend.Interfaces
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<NewIngredientDTO>> GetAllIngredients();
        Task<IEnumerable<IngredientsUsedDTO>> GetAllProductsAndTheirIngredients();
        Task<IEnumerable<ProductIngredientPriceDTO>> GetAllProductsWithIngredientWeightAndPrice();
        Task<IEnumerable<TotalAmmountIngredientsDTO>> GetTotalAmmountOfIngredientsForTargetDate(string date);
        Task<NewIngredientDTO> CreateIngredient(NewIngredientDTO newIngredientDTO);
        Task<NewIngredientDTO> DeleteIngredient(int id);
        Task<NewIngredientDTO> UpdateIngredient(NewIngredientDTO newIngredientDTO);
        Task<IngredientsUsedDTO> LinkProductToIngredient(IngredientsUsedDTO ingredientsUsedDTO);
    }
}