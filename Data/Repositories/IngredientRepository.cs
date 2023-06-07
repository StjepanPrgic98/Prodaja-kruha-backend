using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;
using SQLitePCL;

namespace Prodaja_kruha_backend.Data.Repositories
{

    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataContext _context;
        public IngredientRepository(DataContext context)
        {
            _context = context;           
        }

        public async Task<NewIngredientDTO> CreateIngredient(NewIngredientDTO newIngredientDTO)
        {
            Ingredient newIngredient = new Ingredient
            {
                Type = newIngredientDTO.IngredientType,
                Weight = newIngredientDTO.IngredientPackageWeight,
                Price = newIngredientDTO.IngredientPrice
            };

            _context.Ingredients.Add(newIngredient);
            await _context.SaveChangesAsync();

            NewIngredientDTO ingredintCreated = new NewIngredientDTO
            {
                Id = newIngredient.Id,
                IngredientType = newIngredient.Type,
                IngredientPackageWeight = newIngredient.Weight,
                IngredientPrice = newIngredient.Price
            };

            return ingredintCreated;




        }

        public async Task<NewIngredientDTO> DeleteIngredient(int id)
        {
            Ingredient ingredientToDelete = await _context.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
            if(ingredientToDelete == null){return null;}

            _context.Remove(ingredientToDelete);

            NewIngredientDTO ingredientDeleted = new NewIngredientDTO
            {
                IngredientType = ingredientToDelete.Type,
                IngredientPackageWeight = ingredientToDelete.Weight,
                IngredientPrice = ingredientToDelete.Price
            };

            await _context.SaveChangesAsync();

            return ingredientDeleted;
        }

        public async Task<IEnumerable<NewIngredientDTO>> GetAllIngredients()
        {
            List<NewIngredientDTO> ingredientInfoDTOs = new List<NewIngredientDTO>();

           var ingredients = await _context.Ingredients
           .Where(i => i.Outdated == DateTime.MinValue)
           .OrderByDescending(i => i.Price)
           .ToListAsync();

            if(ingredients == null || ingredients.Count < 1){return null;}

            foreach(var ingredient in ingredients)
            {
               NewIngredientDTO newIngredientDTO = new NewIngredientDTO
               {
                    Id = ingredient.Id,
                    IngredientType = ingredient.Type,
                    IngredientPackageWeight = ingredient.Weight,
                    IngredientPrice = ingredient.Price     
               };

                ingredientInfoDTOs.Add(newIngredientDTO);
            }

           return ingredientInfoDTOs;
        }

        public async Task<IEnumerable<IngredientsUsedDTO>> GetAllProductsAndTheirIngredients()
        {
            var ingredientsUsed = await _context.IngredientsUsed
            .Include(i => i.IngredientsInfo).ThenInclude(i => i.Ingredient)
            .Include(i => i.Product)
            .Select(i => new IngredientsUsedDTO
            {
                Id = i.Id,
                ProductType = i.Product.Type,
                IngredientTypes = new List<IngredientInfoDTO>
                {
                    new IngredientInfoDTO
                    {
                        IngredientType = i.IngredientsInfo.Select(x => x.Ingredient.Type).FirstOrDefault(),
                        IngredientPercentage = i.IngredientsInfo.Select(x => x.Percentage).FirstOrDefault()
                    }
                    
                },
            }).ToListAsync();

            return ingredientsUsed;
            
        }

        public async Task<IngredientsUsedDTO> LinkProductToIngredient(IngredientsUsedDTO ingredientsUsedDTO)
        {
            List<IngredientInfoDTO> ingredientsTypes = new List<IngredientInfoDTO>(ingredientsUsedDTO.IngredientTypes);
            List<IngredientInfo> ingredientInfos = new List<IngredientInfo>();

            if(ingredientsTypes == null || ingredientsTypes.Count < 1){return null;}
            
            Product productUsed = await _context.Products.FirstOrDefaultAsync(x => x.Type == ingredientsUsedDTO.ProductType);

            foreach(var ingredientType in ingredientsTypes)
            {
                IngredientInfo ingredientInformation = new IngredientInfo
                {
                    Ingredient = await _context.Ingredients.Where(x => x.Type == ingredientType.IngredientType && x.Outdated == DateTime.MinValue).FirstOrDefaultAsync(),
                    Percentage = ingredientType.IngredientPercentage
                };
                ingredientInfos.Add(ingredientInformation);
                
                _context.IngredientInfos.Add(ingredientInformation);
            }

            IngredientUsed newIngredientsUsed = new IngredientUsed
            {
                Product = productUsed,
                IngredientsInfo = ingredientInfos
            };

            _context.IngredientsUsed.Add(newIngredientsUsed);
            await _context.SaveChangesAsync();

            ingredientsUsedDTO.Id = newIngredientsUsed.Id;

            return ingredientsUsedDTO;
            
        }

        public async Task<NewIngredientDTO> UpdateIngredient(NewIngredientDTO newIngredientDTO)
        {
            Ingredient ingredientToUpdate = await _context.Ingredients.FirstOrDefaultAsync(x => x.Id == newIngredientDTO.Id);

            if(ingredientToUpdate == null){return null;}

            Ingredient newIngredient = new Ingredient
            {
                Type = newIngredientDTO.IngredientType,
                Weight = newIngredientDTO.IngredientPackageWeight,
                Price = newIngredientDTO.IngredientPrice,
                DateCreated = DateTime.UtcNow,
            };

            ingredientToUpdate.Outdated = DateTime.UtcNow;

            _context.Ingredients.Add(newIngredient);
            await _context.SaveChangesAsync();

            NewIngredientDTO ingredientCreated = new NewIngredientDTO
            {
                Id = newIngredient.Id,
                IngredientType = newIngredient.Type,
                IngredientPackageWeight = newIngredient.Weight,
                IngredientPrice = newIngredient.Price
            };

            return ingredientCreated;
        }
    }
}