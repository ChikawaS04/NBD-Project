using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class ProjectThumbnail
    {
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        [StringLength(255)]
        public string MimeType { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
