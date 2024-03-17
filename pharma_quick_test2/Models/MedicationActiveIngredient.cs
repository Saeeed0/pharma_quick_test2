namespace pharma_quick_test2.Models
{
    public class MedicationActiveIngredient
    {
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }

        public int ActiveIngredientId { get; set; }
        public ActiveIngredient ActiveIngredient { get; set; }
    }
}
