namespace Test_1.DTO
{
    public class FoundPersonWithUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string FoundCity { get; set; } = string.Empty;
        public string Address_City { get; set; } = string.Empty;
        public IFormFile? Image { get; set; } 
        public string PersonWhoFoundhim { get; set; } = string.Empty;
        public string PhonePersonWhoFoundhim { get; set; } = string.Empty;
    }
}
