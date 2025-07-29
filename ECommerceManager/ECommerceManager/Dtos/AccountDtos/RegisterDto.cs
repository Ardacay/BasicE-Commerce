namespace ECommerceManager.Dtos.AccountDtos
{
    public class RegisterDto
    {
        public string Email { get; set; }


        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }


        public string PhoneNumber { get; set; }


        public DateTime? BirthDate { get; set; }
    }
}
