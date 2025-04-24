using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{

    public enum BidStatusList
    {
        [Display(Name = "Desig Stage")]
        Design,
        [Display(Name = "New copy")]
        Copy,
        [Display(Name = "Waiting approval")]
        Waiting
    }
    public class Bid
    {

		[Display(Name = "Approve Status")]
		public string BidSummaryStatus
		{
			get
			{
                string result = "";

				if (BidApprove == true && BidApproveClient == true && BidRejectedClient == false && BidRejectedManager == false)
				{
					result = "Full Approved";
				}
				else if (BidApprove == true && BidApproveClient == false && BidRejectedClient == false && BidRejectedManager == false)
				{
					result = "Manager Approved";
				}
				else if (BidRejectedManager == true || BidRejectedClient == true)
				{
					result = "Rejected";
				}
				else
				{
					result = " - ";
				}
				return result;
			}
		}


		public int BidId { get; set; }

        [Required(ErrorMessage = "You must enter a Bid amount.")]
        [Display(Name = "Bid Amount")]
        [DataType(DataType.Currency)]
        public double BidAmount { get; set; }

        [Required(ErrorMessage = "You must enter a date for the Bid.")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateOnly BidDate { get; set; }

        [Display(Name = "Bid Cost")]
        [DataType(DataType.Currency)]
        public double BidCost { get; set; }

        [Display(Name = "Manager Approve")]
        public bool BidApprove { get; set; }

        [Display(Name = "Manager Reject")]
        public bool BidRejectedManager { get; set; }

        [Display(Name = "Client Approve")]
        public bool BidApproveClient { get; set; }

        [Display(Name = "Client Reject")]
        public bool BidRejectedClient { get; set; }

        [Display(Name = "Bid Status")]
        public string BidStatus { get; set; } = "Waiting Approval";

        public BidStatusList BidStatusList { get; set; }

        [Display(Name = "Bid Notes")]
        [StringLength(2000, ErrorMessage = "Only 2000 characters for notes.")]
        [DataType(DataType.MultilineText)]
        public string BidNoteReason { get; set; } = "";

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "You must choose a project for this Bid.")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();

        //Staff many to many
        public ICollection<StaffDetail> StaffDetails { get; set; } = new HashSet<StaffDetail>();


        //Labour many to many
        public ICollection<LabourDetail> LabourDetails { get; set; } = new HashSet<LabourDetail>();

        //Material many to many
        public ICollection<MaterialDetail> MaterialDetails { get; set; } = new HashSet<MaterialDetail>();



        //Prototype 2 adding materials, labours and staff
        public Bid()
        {
            StaffDetails = new List<StaffDetail>();
            LabourDetails = new List<LabourDetail>();
        }

        public void AddMaterial(Inventory inventory, int quantity)
        {
            MaterialDetails.Add(new MaterialDetail { Inventory = inventory, Quantity = quantity });
        }

        public void AddLabour(Labour labour, int quantity)
        {
            LabourDetails.Add(new LabourDetail { Labour = labour, Quantity = quantity });
        }

        public void AddStaff(Staff staff)
        {
            StaffDetails.Add(new StaffDetail { Staff = staff });
        }

    }


}


