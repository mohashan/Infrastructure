using System.Text.Json.Serialization;

namespace Infrastructure.Messenger.Models
{
    public class Feature:BaseEntity
    {
        public void FillDataType(Type type)
        {
            DataType = typeof(Type)?.FullName?? typeof(string).FullName;
        }
        public string DataType { get; set; } = typeof(string)?.FullName;

        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<ContactFeatures> ContactFeatures { get; set; }
        [JsonIgnore]
        public virtual ICollection<Channels> Channels { get; set; }

    }
}
