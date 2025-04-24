using System.ComponentModel.DataAnnotations;

namespace NBD3.ViewModels
{
    public class BidSummaryVM
    {
        [Display(Name = "Material Total")]
        [DataType(DataType.Currency)]
        public double MaterialTotalPrice
        {
            get
            {
                if (InventoryList > 0  && NumberOfMaterials > 0)
                {
                    
                        return InventoryList * NumberOfMaterials;
                }
                else
                {
                   return 0.00;

                }
                
            }
        }

        public int ID { get; set; }

        public string InventoryCode { get; set; }

        [Display(Name = "Price")]
        public double InventoryList { get; set; }

        [Display(Name = "Type")]
        public string InventoryType { get; set; }

        [Display(Name = "Category")]
        public string InventoryCategory { get; set; }

        [Display(Name = "Project")]
        public string ProjectName { get; set; }


        [Display(Name = "Number of Materials")]
        public int NumberOfMaterials { get; set; }

        [Display(Name = "Total Materials")]
        [DataType(DataType.Currency)]
        public double TotalMaterial { get; set; }

        [Display(Name = "Material Cost")]
        [DataType(DataType.Currency)]
        public double MaterialCost { get; set; }
    }
}
