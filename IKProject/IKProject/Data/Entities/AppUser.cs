using Microsoft.AspNetCore.Identity;

namespace IKProject.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }

    }
}
