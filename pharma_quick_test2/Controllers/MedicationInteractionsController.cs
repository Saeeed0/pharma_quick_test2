using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharma_quick_test2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace pharma_quick_test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationInteractionsController : ControllerBase
    {
        private readonly pharma_Context _context;
        private readonly IHttpClientFactory _clientFactory;

        public MedicationInteractionsController(pharma_Context context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        [HttpPost]
        [Route("interactions")]
        public async Task<ActionResult<IEnumerable<string>>> CheckInteractions([FromBody] List<string> medications)
        {
            // Retrieve active ingredients for each medication
            var activeIngredientsPerMedication = new Dictionary<string, List<string>>();
            foreach (var medicationName in medications)
            {
                var activeIngredients = _context.Medications
                    .Where(m => m.MedicationName == medicationName)
                    .SelectMany(m => m.MedicationActiveIngredients)
                    .Select(mai => mai.Ingredient.IngredientName)
                    .ToList();

                activeIngredientsPerMedication.Add(medicationName, activeIngredients);
            }

            // Prepare request body for AI module
            var aiRequestBody = JsonSerializer.Serialize(activeIngredientsPerMedication);

            // Make request to AI module
            var aiEndpoint = "http://your-ai-module/api/interaction-analysis"; // Replace with your AI module's endpoint
            var aiHttpContent = new StringContent(aiRequestBody, System.Text.Encoding.UTF8, "application/json");
            using var aiClient = _clientFactory.CreateClient();
            var aiResponse = await aiClient.PostAsync(aiEndpoint, aiHttpContent);

            if (aiResponse.IsSuccessStatusCode)
            {
                // Parse AI module response
                var aiResponseBody = await aiResponse.Content.ReadAsStringAsync();
                var interactionResults = JsonSerializer.Deserialize<List<string>>(aiResponseBody);
                return Ok(interactionResults);
            }
            else
            {
                // Handle error response from AI module
                return StatusCode((int)aiResponse.StatusCode, "Failed to get interaction analysis from AI module.");
            }
        }
    }
}
