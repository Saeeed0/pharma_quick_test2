namespace pharma_quick_test2.Models
{
    public class ActiveIngredient
    {
        public int ActiveIngredientId { get; set; }
        public string Name { get; set; }

        public ICollection<MedicationActiveIngredient> Medications { get; set; }
    }
}
