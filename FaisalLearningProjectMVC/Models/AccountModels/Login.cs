using System.ComponentModel.DataAnnotations;

namespace FaisalLearningProjectMVC.Models.AccountModels
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
