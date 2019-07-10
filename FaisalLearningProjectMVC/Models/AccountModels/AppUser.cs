using Microsoft.AspNetCore.Identity;

namespace FaisalLearningProjectMVC.Models.AccountModels
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
