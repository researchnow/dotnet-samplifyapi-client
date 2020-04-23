using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class TemplateCriteria : IValidator
    {
        [DataMember(Name = "countryISOCode")]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "languageISOCode")]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "quotaPlan")]
        public QuotaPlan QuotaPlan { get; set; }
        [DataMember(Name = "tags")]
        public string[] Tags{ get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.Name, this.CountryISOCode, this.LanguageISOCode);
            Validator.IsCountryCodeOrNull(this.CountryISOCode);
            Validator.IsLanguageCodeOrNull(this.LanguageISOCode);
            Validator.IsNotNull(this.QuotaPlan);
            Validator.Validate(this.QuotaPlan);
        }
    }
}