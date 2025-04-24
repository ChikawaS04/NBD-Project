using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Staff
    {
        [Display(Name = "Staff")]
        public string StaffFormalName
        {
            get
            {
                return StaffFirstName + ", " + StaffLastname;

            }
        }

        [Display(Name = "Phone")]
        public string StaffPhoneFormat
        {
            get
            {
                if (!string.IsNullOrEmpty(StaffPhone) && StaffPhone.Length == 10)
                {
                    return $"({StaffPhone.Substring(0, 3)}) {StaffPhone.Substring(3, 3)}-{StaffPhone.Substring(6)}";
                }

                return StaffPhone;
            }
        }
        public int StaffID { get; set; }



        [Display(Name = "Title")]
        [Required(ErrorMessage = "You cannot leave the title blank.")]
        [StringLength(60, ErrorMessage = "First name cannot be more than 60 characters long.")]
        public string StaffTitle { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(60, ErrorMessage = "First name cannot be more than 60 characters long.")]
        public string StaffFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        public string StaffLastname { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "You cannot leave the phone number blank.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "The phone number must be exactly 10 numeric digits.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string StaffPhone { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string StaffEmail { get; set; }

        public StaffPhoto StaffPhoto { get; set; }
        public StaffThumbnail StaffThumbnail { get; set; }

        public ICollection<StaffDetail> StaffDetails { get; set; } = new HashSet<StaffDetail>();

    }
}
