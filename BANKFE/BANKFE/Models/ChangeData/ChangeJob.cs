using System.ComponentModel.DataAnnotations;

namespace BANKFE.Models.ChangeData
{
    public class ChangeJob
    {
        public string USERNAME { get; set; }
        [Display(Name = "Job")]
        [Required(ErrorMessage = "Job is required")]
        public string JOB { get; set; }

    }
}
