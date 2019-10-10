using Microsoft.AspNetCore.Identity;

namespace DataModels
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Preposition { get; set; }
        public string CompanyName { get; set; }
    }
}
