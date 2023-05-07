namespace Infrastructure.Messenger.Models
{
    public class Channels:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EndPoint { get; set; }
        public int FeatureId { get; set; }
        public Features? Feature { get; set; }

    }
}
