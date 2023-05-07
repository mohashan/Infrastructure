namespace Infrastructure.Messenger.Models
{
    public class ContactFeatures:BaseEntity
    {
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        public int FeatureId { get; set; }
        public virtual Features Feature { get; set; }

        public string Value { get; set; }
    }
}
