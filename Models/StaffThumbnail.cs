using System.ComponentModel.DataAnnotations;

namespace NBD3.Models
{
    public class StaffThumbnail
    {
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        [StringLength(255)]
        public string MimeType { get; set; }

        public int StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}
