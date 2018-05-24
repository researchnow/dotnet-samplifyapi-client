using System.Runtime.Serialization;

namespace ResearchNow.SamplifyAPIClient
{
    [DataContract]
    public class Country
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "isoCode")]
        public string IsoCode { get; set; }
        [DataMember(Name = "countryName")]
        public string CountryName { get; set; }
        [DataMember(Name = "supportedLanguages")]
        public Language[] SupportedLanguages { get; set; }
    }

    [DataContract]
    public class Language
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "isoCode")]
        public string IsoCode { get; set; }
        [DataMember(Name = "languageName")]
        public string LanguageName { get; set; }
    }

}
