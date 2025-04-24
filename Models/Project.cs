using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public enum ProjectStatusList
    {
        OnDesign,
        OnReview,
        Rejected,
        Approved,
        Canceled
    }

    public class Project : IValidatableObject
    {
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(120, ErrorMessage = "Project name cannot exceed 120 characters.")]
        public string ProjectName { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "Description can be at most 2000 characters.")]
        [DataType(DataType.MultilineText)]
        public string ProjectDescription { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateOnly ProjectStartDate { get; set; }


        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateOnly? ProjectEndDate { get; set; }

        // Foreign Keys
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client selection is required.")]
        public int ClientId { get; set; }

        [Display(Name = "Client")]
        public Client Client { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location selection is required.")]
        public int LocationId { get; set; }

        [Display(Name = "Location")]
        public Location Location { get; set; }

        //Status
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status selection is required.")]
        public string ProjectStatus { get; set; } = "Design Stage";

        [Required(ErrorMessage = "You must select a Project Status!")]
        public ProjectStatusList ProjectStatusList { get; set; }


        [Display(Name = "Projectd Manager Approve")]
        public bool ProjectApprove { get; set; }

        [Display(Name = "Project Manager Rejected")]
        public bool ProjectReject { get; set; }

        [Display(Name = "Project Reasons")]
        [StringLength(2000, ErrorMessage = "Only 2000 characters for notes.")]
        [DataType(DataType.MultilineText)]
        public string ProjectNoteReason { get; set; } = "";

        public ProjectPhoto ProjectPhoto { get; set; }
        public ProjectThumbnail ProjectThumbnail { get; set; }

        public DateOnly? ProjectEndDateValue { get; set; }

        // Navigation property
        public ICollection<Location> Locations { get; set; } = new HashSet<Location>();

        [Display(Name = "Bid")]
        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            //Project cannot end before it starts
            if (ProjectEndDate < ProjectStartDate)
            {
                yield return new ValidationResult("Project End Date cannot end before it starts.", new[] { "ProjectEndDate" });
            }
        }
    }


}
