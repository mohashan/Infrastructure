using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DeleteDate { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}