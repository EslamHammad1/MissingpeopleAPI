namespace Test_1.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmed { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

    }
}
