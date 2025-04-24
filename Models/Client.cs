using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Display(Name = "Phone")]
        public string PhoneFormatted
        {
            get
            {
                if (!string.IsNullOrEmpty(ClientPhone) && ClientPhone.Length == 10)
                {
                    return $"({ClientPhone.Substring(0, 3)})-{ClientPhone.Substring(3, 3)}-{ClientPhone.Substring(6)}";
                }

                return ClientPhone;
            }
        }



        [Display(Name = "Contact person")]
        public string ContactFullName
        {
            get
            {
                return ClientFirstName
                    + (string.IsNullOrEmpty(ClientMiddleName) ? " " :
                        (" " + (char?)ClientMiddleName[0] + ". ").ToUpper())
                    + ClientLastName;
            }
        }

        [Display(Name = "Full Address")]
        public string ClientFullAddress
        {
            get
            {
                return ClientStreetAddress + ", "
                    + ClientCityAddress + ", "
                    + ClientCountryAddress + ", "
                    + ClientPostalCode;
            }
        }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(120, ErrorMessage = "Company name cannot be more than 120 characters long.")]
        public string ClientCommpanyName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(60, ErrorMessage = "First name cannot be more than 60 characters long.")]
        [RegularExpression("^[a-zA-Z'-]*$", ErrorMessage = "First name can only contain letters, apostrophes, and hyphens.")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        [RegularExpression("^[a-zA-Z'-]*$", ErrorMessage = "Middle name can only contain letters, apostrophes, and hyphens.")]
        public string ClientMiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        [RegularExpression("^[a-zA-Z'-]*$", ErrorMessage = "Last name can only contain letters, apostrophes, and hyphens.")]
        public string ClientLastName { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\(\d{3}\)-\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number in the format '(111)-111-1111'.")]
        [DataType(DataType.PhoneNumber)]
        
        public string ClientPhone { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string? ClientEmail { get; set; }



        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string ClientStreetAddress { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression("^[A-Za-z][0-9][A-Za-z] [0-9][A-Za-z][0-9]$", ErrorMessage = "Please enter a valid postal code (A1A 1A1).")]
        public string ClientPostalCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        //[StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        //[RegularExpression(@"^[a-zA-Z'.-]+\s?(?:[a-zA-Z'.-]+\s?)*$", ErrorMessage = "City can only contain letters, apostrophes, hyphens, and periods.")]
        public string ClientCityAddress { get; set; }


        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        [RegularExpression("^[a-zA-Z'-]*$", ErrorMessage = "Country can only contain letters, apostrophes, and hyphens.")]
        public string ClientCountryAddress { get; set; }

        // Navigation property for related projects
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
