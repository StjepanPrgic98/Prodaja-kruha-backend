using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prodaja_kruha_backend.DTOs;
using Prodaja_kruha_backend.Entities;
using Prodaja_kruha_backend.Interfaces;

namespace Prodaja_kruha_backend.Controllers
{
    public class IngredientsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }


        [HttpGet("ingredientsList")]
        public async Task<ActionResult<IEnumerable<IngredientInfoDTO>>> GetAllIngredients()
        {
            var ingredients = await _unitOfWork.IngredientRepository.GetAllIngredients();
            if(ingredients == null){return BadRequest("Something went wrong!");}
            return Ok(ingredients);
        }

        [HttpGet("productsWithIngredients")]
        public async Task<ActionResult<IEnumerable<IngredientsUsedDTO>>> GetAllProductsWithTheirIngredients()
        {
            var products = await _unitOfWork.IngredientRepository.GetAllProductsAndTheirIngredients();
            if(products == null){return BadRequest("Something went wrong!");}
            return Ok(products);
        }

        [HttpPost("create")]
        public async Task<ActionResult<NewIngredientDTO>> CreateNewIngredient(NewIngredientDTO newIngredientDTO)
        {
            var newIngredient = await _unitOfWork.IngredientRepository.CreateIngredient(newIngredientDTO);
            if(newIngredient == null){return BadRequest("Something went wrong!");}
            return newIngredient;
        }

        [HttpPost("linkIngredientsToProduct")]
        public async Task<ActionResult<IngredientsUsedDTO>> LinkIngredientsToProduct(IngredientsUsedDTO ingredientsUsedDTO)
        {
            var product = await _unitOfWork.IngredientRepository.LinkProductToIngredient(ingredientsUsedDTO);
            if(product == null){return BadRequest("Something went wrong!");}
            return product;
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<NewIngredientDTO>> DeleteIngredient(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.DeleteIngredient(id);
            if(ingredient == null){return BadRequest("Ingredient does not exist!");}
            return ingredient;
        }

        [HttpPost("update")]
        public async Task<ActionResult<NewIngredientDTO>> UpdateIngredient(NewIngredientDTO newIngredientDTO)
        {
            var ingredient = await _unitOfWork.IngredientRepository.UpdateIngredient(newIngredientDTO);
            if(ingredient == null){return BadRequest("Ingredient  does not exist!");}
            return ingredient;
        }
    }
}