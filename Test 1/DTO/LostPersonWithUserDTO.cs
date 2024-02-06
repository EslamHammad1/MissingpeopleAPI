namespace Test_1.DTO
{
    public class LostPersonWithUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        public string LostCity { get; set; } = string.Empty;
        public string Address_City { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public string PersonWhoLost { get; set; } = string.Empty;
        public string PhonePersonWhoLost { get; set; } = string.Empty;
    }
}
