using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Messenger.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;

        public DateTime? DeleteDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
