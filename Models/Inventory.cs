using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Inventory
    {

        public int InventoryID { get; set; }

        [Display(Name = "Inventory Code")]
        [Required(ErrorMessage = "You cannot leave the Inventory code blank.")]
        public string InventoryCode { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "You cannot leave description blank.")]
        [StringLength(120, ErrorMessage = "Description cannot be more than 120 characters long.")]
        public string InventoryDescription { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int InventoryQuantity { get; set; }

        [Display(Name = "Size")]
        [Required(ErrorMessage = "You cannot leave unit type blank.")]
        [StringLength(25, ErrorMessage = "Unit type cannot be more than 25 characters long.")]
        public string InventoryUnitType { get; set; }

        [Required(ErrorMessage = "You must enter a unit price.")]
        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public double InventoryPriceList { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "You must Choose a category from list.")]
        public int MaterialCategoryID { get; set; }

        [Display(Name = "Category")]
        public MaterialCategory MaterialCategory { get; set; }

        public ICollection<MaterialDetail> MaterialDetails { get; set; } = new HashSet<MaterialDetail>();
    }
}
