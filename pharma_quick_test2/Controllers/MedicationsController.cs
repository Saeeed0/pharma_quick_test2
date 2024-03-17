using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharma_quick_test2.Models;
using pharma_quick_test2.ViewModels;

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
            var meds = _Context.Medications.Include(e=>e.Category).ToList();

            var medsvm = meds.Select(e => new MedicationVM()
            {
                MedicationId = e.MedicationId,
                Category = e.Category.CategoryName,
                MedicationName = e.MedicationName,
                SideEffects = e.SideEffects,
                Precautions = e.Precautions,
                ContraindicationsForUse = e.ContraindicationsForUse,
                UseOfMedications = e.UseOfMedications
            });

            return Ok(medsvm);
        }




    }
}
