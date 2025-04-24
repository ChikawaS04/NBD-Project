using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class LabourDetail
    {
        public int LabourDetailId { get; set; }

        [Display(Name = "Extended Price")]
        [DataType(DataType.Currency)]
        public double LabourTotalPrice
        {
            get
            {
                if (Labour.LabourPriceHour > 0 && Quantity > 0)
                {

                    return Labour.LabourPriceHour * Quantity;
                }
                else
                {
                    return 0.00;

                }

            }
        }

        [Display(Name = "Cost Total")]
        [DataType(DataType.Currency)]
        public double LabourTotalCost
        {
            get
            {
                if (Labour.LabourCostHour > 0 && Quantity > 0)
                {

                    return Labour.LabourCostHour * Quantity;
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


        [Display(Name = "Labour")]
        public int LabourID { get; set; }
        public Labour Labour { get; set; }

        [Display(Name = "Hours")]
        public int Quantity { get; set; }
    }
}
