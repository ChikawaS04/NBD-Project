using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class MaterialDetail
    {

        public int MaterialDetailId { get; set; }

        [Display(Name = "Extended Price")]
        [DataType(DataType.Currency)]
        public double MaterialTotalPrice
        {
            get
            {
                if (Inventory.InventoryPriceList > 0 && Quantity > 0)
                {

                    return Inventory.InventoryPriceList * Quantity;
                }
                else
                {
                    return 0.00;

                }

            }
        }


        [Display(Name = "Bid")]
        public int BidID { get; set; }
        public Bid Bid { get; set; }


        [Display(Name = "Material")]
        public int InventoryID { get; set; }
        public Inventory Inventory { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
