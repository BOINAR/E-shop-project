using System.ComponentModel.DataAnnotations;


namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public Address? Address { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }


}


