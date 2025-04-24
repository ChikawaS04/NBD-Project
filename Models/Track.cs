using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class Track
    {
        public int TrackID { get; set; }

        [Display(Name = "Track Notes")]
        [StringLength(2000, ErrorMessage = "Only 2000 characters for notes.")]
        [DataType(DataType.MultilineText)]
        public string TrackNotes { get; set; }

        [Required(ErrorMessage = "You must enter a start date and time for the Function.")]
        [Display(Name = "date")]
        [DataType(DataType.DateTime)]
        public DateTime TrackDate { get; set; } = DateTime.Today.AddHours(32);

        [Display(Name = "Bid")]
        public int BidID { get; set; }
        public Bid Bid { get; set; }

    }
}
