using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class SampleSources
    {
        [DataMember(Name = "countryISOCode")]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "languageISOCode")]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "sources")]
        public SampleSource[] Sources { get; set; }
        [DataMember(Name = "supportedLanguages")]
        public Language[] SupportedLanguages { get; set; }
    }

    public class SampleSource
    {
        [DataMember(Name = "category")]
        public SampleCategory Category { get; set; }
        [DataMember(Name = "default")]
        public bool Default { get; set; }
        [DataMember(Name = "deliverySystem")]
        public string DeliverySystem { get; set; }
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    public class SampleCategory
    {
        [DataMember(Name = "surveyTopics")]
        public string[] SurveyTopics { get; set; }
    }

}
