namespace Corona_virus_management_system.Models
{
    public class Vaccine
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string? Manufacturer { get; set; }

        public int MemberId { get; set; }
    }
}
