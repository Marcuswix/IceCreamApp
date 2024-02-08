namespace Share.Dtos
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? StreetName { get; set; } = null!;

        public int PostalCode { get; set; }

        public string? City { get; set; } = null!;
    }
}
