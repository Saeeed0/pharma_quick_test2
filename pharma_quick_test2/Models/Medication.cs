namespace pharma_quick_test2.Models
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public string UseOfMedications { get; set; }
        public string ContraindicationsForUse { get; set; }
        public string Precautions { get; set; }
        public string SideEffects { get; set; }

        public int CategoryId { get; set; }
        public MedicationCategory Category { get; set; }

        public ICollection<MedicationActiveIngredient> ActiveIngredients { get; set; }
        public ICollection<Medication> Replacements { get; set; }
    }
}
