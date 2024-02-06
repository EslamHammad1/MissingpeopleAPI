namespace Test_1.Models
{
    public class LostPerson : BaseEntity
    {
        public string LostCity { get; set; } = string.Empty;
        public string PersonWhoLost { get; set; } = string.Empty;
        public string PhonePersonWhoLost { get; set; } = string.Empty;
    }
}
