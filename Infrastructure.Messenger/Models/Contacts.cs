namespace Infrastructure.Messenger.Models
{
    public class Contact:BaseEntity
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual ICollection<ContactFeatures> ContactFeatures { get; set; }
    }
}
