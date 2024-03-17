namespace pharma_quick_test2.Models
{
    public class MedicationCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Medication> Medications { get; set; }
    }
}
