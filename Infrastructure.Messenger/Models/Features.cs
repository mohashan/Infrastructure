namespace Infrastructure.Messenger.Models
{
    public class Features:BaseEntity
    {
        public string DataType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ContactFeatures> ContactFeatures { get; set; }
        public virtual ICollection<Channels> Channels { get; set; }

    }
}
