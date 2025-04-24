using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Display(Name = "Phone")]
        public string PhoneFormatted
        {
            get
            {
                if (!string.IsNullOrEmpty(LocationPhone) && LocationPhone.Length == 10)
                {
                    return $"({LocationPhone.Substring(0, 3)}) {LocationPhone.Substring(3, 3)}-{LocationPhone.Substring(6)}";
                }

                return LocationPhone;
            }
        }

        public string LocationFullAddress
        {
            get
            {
                return LocationStreetAddress + ", "
                    + LocationCityAddress + ", "
                    + LocationCountryAddress + ", "
                    + LocationPostalCode;
            }
        }

        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "You cannot leave the location name blank.")]
        [StringLength(120, ErrorMessage = "Location name cannot be more than 120 characters long.")]
        public string LocationName { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "You cannot leave the phone number blank.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "The phone number must be exactly 10 numeric digits.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string LocationPhone { get; set; }

        [Display(Name = "Contact Person")]
        [Required(ErrorMessage = "You cannot leave the contact person blank.")]
        [StringLength(120, ErrorMessage = "Contact name cannot be more than 120 characters long.")]
        public string LocationContactPer { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string LocationStreetAddress { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression("^[A-Za-z][0-9][A-Za-z] [0-9][A-Za-z][0-9]$", ErrorMessage = "Please enter a valid postal code (A1A 1A1).")]
        public string LocationPostalCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string LocationCityAddress { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string LocationCountryAddress { get; set; }




    }
}
