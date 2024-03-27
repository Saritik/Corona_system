namespace Corona_virus_management_system.Models
{
    public class Member
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Image { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public int HouseNumber { get; set; }

        public DateTime BearthDate { get; set; }

        public string? Phone { get; set; }

        public string? CellPhone { get; set; }

        public DateTime? PositiveResult { get; set; }

        public DateTime? RecoveryDate { get; set; }

        public Member()
        {
            PositiveResult = null;
            RecoveryDate = null;
        }
    }
}
