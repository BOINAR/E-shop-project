using System.ComponentModel.DataAnnotations;


namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public Adress? Adress { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }


}


