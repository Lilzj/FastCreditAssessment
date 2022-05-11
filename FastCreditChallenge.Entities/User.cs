using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FastCreditChallenge.Entities
{
    public class User :  IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public byte Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Nationality { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;

    }
}