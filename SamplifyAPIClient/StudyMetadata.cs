using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class StudyMetadata
    {
        [DataMember(Name = "category")]
        public CategoryMetaData Category { get; set; }
        [DataMember(Name = "deliveryTypes")]
        public MetadataItem[] DeliveryTypes { get; set; }
    }

    [DataContract]
    public class CategoryMetaData
    {
        [DataMember(Name = "studyRequirements")]
        public MetadataItem[] StudyRequirements { get; set; }
        [DataMember(Name = "studyTypes")]
        public MetadataItem[] StudyTypes { get; set; }
        [DataMember(Name = "surveyTopics")]
        public MetadataItem[] SurveyTopics { get; set; }
    }

    [DataContract]
    public class MetadataItem
    {
        [DataMember(Name = "allowed")]
        public bool Allowed { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

}
