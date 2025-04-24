using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Labour
    {
        public int LabourID { get; set; }

        [Display(Name = "Labour Type")]
        [Required(ErrorMessage = "You cannot leave the labour type blank.")]
        [StringLength(80, ErrorMessage = "Labour type cannot be more than 80 characters long.")]
        public string LabourType { get; set; }

        [Required(ErrorMessage = "You must enter a value for Price hour.")]
        [Display(Name = "Price Hour")]
        [DataType(DataType.Currency)]
        public double LabourPriceHour { get; set; }

        [Required(ErrorMessage = "You must enter a value for Cost hour.")]
        [Display(Name = "Cost Hour")]
        [DataType(DataType.Currency)]
        public double LabourCostHour { get; set; }

        public ICollection<LabourDetail> LabourDetails { get; set; } = new HashSet<LabourDetail>();
    }
}
