using System.ComponentModel.DataAnnotations;

namespace Infrastructure.LiskShortener.Models
{
    public class ShortLink
    {
        [Key]
        public string ShortCode { get; set; }
        public string OriginalUrl { get; set; }
    }
}
