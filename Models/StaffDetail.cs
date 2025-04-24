using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class StaffDetail
    {
        public int StaffDetailId { get; set; }    
        [Display(Name = "Bid")]
        public int BidID { get; set; }
        public Bid Bid { get; set; }

        [Display(Name = "Staff")]
        public int StaffID { get; set; }
        public Staff Staff { get; set; }


    }
}
