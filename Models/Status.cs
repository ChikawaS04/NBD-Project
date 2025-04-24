using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Status
    {
        public int StatusID { get; set; }

        [Display(Name = "Status name")]
        [Required(ErrorMessage = "You cannot leave the status name blank.")]
        [StringLength(20, ErrorMessage = "Status name cannot be more than 20 characters long.")]
        public string StatusName { get; set; }


    }
}
