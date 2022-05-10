using Microsoft.AspNetCore.Identity;

namespace FastCreditChallenge.Entities
{
    public class User :  IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Nationality { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;

    }
}