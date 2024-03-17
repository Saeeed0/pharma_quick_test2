using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharma_quick_test2.Models;
using pharma_quick_test2.ViewModels;

namespace pharma_quick_test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationActiveIngredientController : ControllerBase
    {
        private readonly pharma_Context _Context;

        public MedicationActiveIngredientController(pharma_Context context)
        {
            _Context = context;
        }

        [HttpGet("GetAll")]
        public ActionResult GetMedicationActiveIngredient()
        {
            var medsAciveIngredients = _Context.MedicationActiveIngredients
                .Include(e=>e.Ingredient)
                .Include(e=>e.Medication)
                .ToList();

            var medsAciveIngredientsVM = medsAciveIngredients.Select(e => new MedicationActiveIngredientVM()
            {
                Medication=e.Medication.MedicationName,
                Ingredient=e.Ingredient.IngredientName
            });

            return Ok(medsAciveIngredientsVM);
        }
    }
}
