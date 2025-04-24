using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class MaterialCategory
    {
        public int MaterialCategoryID { get; set; }

        [Display(Name = "Category name")]
        [Required(ErrorMessage = "You cannot leave the category name blank.")]
        [StringLength(20, ErrorMessage = "Category name cannot be more than 20 characters long.")]
        public string CategoryName { get; set; }

        public ICollection<Inventory> Inventorys { get; set; } = new HashSet<Inventory>();
    }
}
