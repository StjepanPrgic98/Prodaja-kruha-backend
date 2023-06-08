using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
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
                    IngredientTypes = i.IngredientsInfo.Select(info => new IngredientInfoDTO
                    {
                        IngredientType = info.Ingredient.Type,
                        IngredientPercentage = info.Percentage
                    }).ToList()
                })
                .ToListAsync();

            return ingredientsUsed;
        }

        public async Task<IEnumerable<ProductIngredientPriceDTO>> GetAllProductsWithIngredientWeightAndPrice()
        {
            var ingredientsUsed = await _context.IngredientsUsed
                .Include(i => i.IngredientsInfo).ThenInclude(i => i.Ingredient)
                .Include(i => i.Product)
                .ToListAsync();

            var productIngredientPriceDTOs = ingredientsUsed.Select(i => new ProductIngredientPriceDTO
            {
                ProductType = i.Product.Type,
                IngredientInfo = i.IngredientsInfo.Select(x => new ProductIngredientPercentageDTO
                {
                    IngredientType = x.Ingredient.Type,
                    IngredientPercentageWeigth =  CalculateWeight(x.Percentage, i.Product.Weight),
                    IngredientPercentagePrice = CalculatePriceForPercentageUsed(Calculate1kgPrice(x.Ingredient.Price, x.Ingredient.Weight), CalculateWeight(x.Percentage, i.Product.Weight))
                }).ToList()
            }).ToList();

            foreach (var product in productIngredientPriceDTOs)
            {
                product.TotalProductPrice = product.IngredientInfo.Sum(i => i.IngredientPercentagePrice);
            }

            return productIngredientPriceDTOs;
        }


        private float Calculate1kgPrice(float price, float weight)
        {
            float pricePerKg = (price / weight) * 1000;
            if (float.IsNaN(pricePerKg) || float.IsInfinity(pricePerKg))
            {
                return 0;
            }
            return pricePerKg;
        }

        private float CalculatePriceForPercentageUsed(float kgPrice, float percentageWeight)
        {
            float result = (kgPrice / 1000) * percentageWeight;
            if (float.IsNaN(result) || float.IsInfinity(result))
            {
                return 0;
            }
            return (float)Math.Round(result, 2);
        }

        private float CalculateWeight(float percentage, float productWeight)
        {
            float weightNeeded = (productWeight * percentage) / 100;
            if (float.IsNaN(weightNeeded) || float.IsInfinity(weightNeeded))
            {
                return 0;
            }
            return (float)Math.Round(weightNeeded, 2);
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

        public async Task<IEnumerable<TotalAmmountIngredientsDTO>> GetTotalAmmountOfIngredientsForTargetDate(string date)
        {
            List<TotalAmmoutDTO> totalAmmoutDTOs = new List<TotalAmmoutDTO>();
            List<TotalAmmountIngredientsDTO> totalAmmountIngredientsDTOs = new List<TotalAmmountIngredientsDTO>();

            var products = await _context.Products.ToListAsync();

            var orders = await _context.Order_Items
                .Include(oi => oi.Orders.Customers)
                .Include(oi => oi.ProductsInfo)
                .ThenInclude(oi => oi.Product)
                .Where(oi => oi.TargetDate == date)
                .ToListAsync();

            foreach (var product in products)
            {
                int totalQuantity = orders.Sum(oi => oi.ProductsInfo.Where(pi => pi.Product.Type == product.Type).Sum(pi => pi.Quantity));
                float totalPrice = orders.Sum(oi => oi.ProductsInfo.Where(pi => pi.Product.Type == product.Type).Sum(pi => pi.Quantity * pi.Product.Price));

                var totalDto = new TotalAmmoutDTO
                {
                    ProductType = product.Type,
                    TotalQuantity = totalQuantity,
                };

                totalAmmoutDTOs.Add(totalDto);

                var ingredientsUsed = await _context.IngredientsUsed
                    .Include(i => i.IngredientsInfo).ThenInclude(i => i.Ingredient)
                    .Include(i => i.Product)
                    .Where(i => i.Product.Type == product.Type)
                    .ToListAsync();

                foreach (var ingredientUsed in ingredientsUsed)
                {
                    foreach (var ingredientInfo in ingredientUsed.IngredientsInfo)
                    {
                        float totalIngredientWeight = CalculateWeight(ingredientInfo.Percentage, product.Weight) * totalQuantity;
                        float totalIngredientPrice = CalculatePriceForPercentageUsed(Calculate1kgPrice(ingredientInfo.Ingredient.Price, ingredientInfo.Ingredient.Weight), CalculateWeight(ingredientInfo.Percentage, product.Weight)) * totalQuantity;

                        var ingredientDto = new TotalAmmountIngredientsDTO
                        {
                            IngredientType = ingredientInfo.Ingredient.Type,
                            TotalIngredientWeight = totalIngredientWeight,
                            TotalIngredientPrice = (float)Math.Round(totalIngredientPrice, 2) // Explicitly convert to float
                        };

                        totalAmmountIngredientsDTOs.Add(ingredientDto);
                    }
                }
            }

            var groupedIngredients = totalAmmountIngredientsDTOs
                .GroupBy(dto => dto.IngredientType)
                .Select(group => new TotalAmmountIngredientsDTO
                {
                    IngredientType = group.Key,
                    TotalIngredientWeight = group.Sum(dto => dto.TotalIngredientWeight),
                    TotalIngredientPrice = (float)Math.Round(group.Sum(dto => dto.TotalIngredientPrice), 2) // Explicitly convert to float
                })
                .OrderByDescending(dto => dto.TotalIngredientPrice); // Sort by ingredient package price

            return groupedIngredients;
        }



    }
}