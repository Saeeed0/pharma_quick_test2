using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharma_quick_test2.Models;

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
            var meds = _Context.Medications.ToList();

            return Ok(meds);
        }
    }
}
