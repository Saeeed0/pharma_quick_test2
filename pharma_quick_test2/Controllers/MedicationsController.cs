using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharma_quick_test2.Models;
using pharma_quick_test2.ViewModels;
using System.Linq;

namespace pharma_quick_test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly pharma_Context _Context;

        public MedicationsController(pharma_Context context)
        {
            _Context = context;
        }

        // GET: MedicationController
        [HttpGet("GetAll")]
        public ActionResult GetMedications()
        {
            var meds = _Context.Medications.Include(e => e.Category).ToList();

            var medsvm = meds.Select(e => new MedicationVM()
            {
                MedicationId = e.MedicationId,
                Category = e.Category.CategoryName,
                MedicationName = e.MedicationName,
                SideEffects = e.SideEffects,
                Precautions = e.Precautions,
                ContraindicationsForUse = e.ContraindicationsForUse,
                UseOfMedications = e.UseOfMedications,
            }).ToList();

            return Ok(medsvm);
        }


        #region GetMedicationByName_Without_ActiveIngredient
        //[HttpGet("{Medicine}")]
        //public ActionResult GetMedicationByName(string Medicine)
        //{
        //    var med = _Context.Medications.Where(e => e.MedicationName == Medicine).Include(e => e.Category).Include(e => e.MedicationReplacementMeds).ThenInclude(e => e.Replacement).FirstOrDefault();

        //    if (med is null)
        //        return NotFound();

        //    var medvm = new MedicationVM()
        //    {
        //        MedicationId = med.MedicationId,
        //        Category = med.Category.CategoryName,
        //        MedicationName = med.MedicationName,
        //        SideEffects = med.SideEffects,
        //        Precautions = med.Precautions,
        //        ContraindicationsForUse = med.ContraindicationsForUse,
        //        UseOfMedications = med.UseOfMedications,
        //    };
        //    medvm.ActiveIngredients = med.MedicationActiveIngredients.Select(e => e.Ingredient.IngredientName).ToList();

        //    medvm.Replacements = med.MedicationReplacementMeds.Select(e => e.Replacement.MedicationName).ToList();

        //    return Ok(medvm);
        //} 
        #endregion

        [HttpGet("{Medicine}")]
        public ActionResult GetMedicationByName(string Medicine)
        {
            var med = _Context.Medications
                .Where(e => e.MedicationName == Medicine)
                .Include(e => e.Category)
                .Include(e => e.MedicationActiveIngredients)  
                    .ThenInclude(e => e.Ingredient)           
                .Include(e => e.MedicationReplacementMeds)
                    .ThenInclude(e => e.Replacement)
                .FirstOrDefault();

            if (med is null)
                return NotFound();

            var medvm = new MedicationVM()
            {
                MedicationId = med.MedicationId,
                Category = med.Category.CategoryName,
                MedicationName = med.MedicationName,
                SideEffects = med.SideEffects,
                Precautions = med.Precautions,
                ContraindicationsForUse = med.ContraindicationsForUse,
                UseOfMedications = med.UseOfMedications,
            };

            medvm.ActiveIngredients = med.MedicationActiveIngredients
                .Select(e => e.Ingredient.IngredientName)
                .ToList();

            medvm.Replacements = med.MedicationReplacementMeds
                .Select(e => e.Replacement.MedicationName)
                .ToList();

            return Ok(medvm);
        }


    }



}
