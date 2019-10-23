using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class OrderDetails
    {
        [DataMember(Name = "errors")]
        public Errors[] Errors { get; set; }
        [DataMember(Name = "salesOrder")]
        public SalesOrder SalesOrder { get; set; }
        [DataMember(Name = "salesOrderDetails")]
        public SalesOrderDetail[] SalesOrderDetails { get; set; }
    }

    [DataContract]
    public class Errors
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }

    [DataContract]
    public class SalesOrder
    {
        [DataMember(Name = "guid")]
        public string GUID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "noCharge")]
        public bool NoCharge { get; set; }
        [DataMember(Name = "orderType")]
        public string OrderType { get; set; }
        [DataMember(Name = "ordernumber")]
        public string OrderNumber { get; set; }
        [DataMember(Name = "relatedOrderCpi")]
        public double RelatedOrderCpi { get; set; }
    }

    [DataContract]
    public class SalesOrderDetail
    {
        [DataMember(Name = "costPerInterview")]
        public double CostPerInterview { get; set; }
        [DataMember(Name = "costPerInterviewWithCurrency")]
        public string CostPerInterviewWithCurrency { get; set; }
        [DataMember(Name = "countryIsoCode")]
        public string CountryIsoCode { get; set; }
        [DataMember(Name = "extendedamount")]
        public string Extendedamount { get; set; }
        [DataMember(Name = "guid")]
        public string GUID { get; set; }
        [DataMember(Name = "labelForMobile")]
        public string LabelForMobile { get; set; }
        [DataMember(Name = "productIdGuid")]
        public string ProductIdGuid { get; set; }
        [DataMember(Name = "productIdName")]
        public string ProductIdName { get; set; }
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
        [DataMember(Name = "ssiAdditionalPoints")]
        public int SSIAdditionalPoints { get; set; }
        [DataMember(Name = "ssiCalculatedIr")]
        public string SSICalculatedIr { get; set; }
        [DataMember(Name = "ssiCalculatedLoi")]
        public string SSICalculatedLoi { get; set; }
        [DataMember(Name = "ssiChartsNum")]
        public int SSIChartsNum { get; set; }
        [DataMember(Name = "ssiFamilyId")]
        public string SSIFamilyId { get; set; }
        [DataMember(Name = "ssiImagesNum")]
        public int SSIImagesNum { get; set; }
        [DataMember(Name = "ssiImagesSpecialNum")]
        public string ssiImagesSpecialNum { get; set; }
        [DataMember(Name = "ssiInputPrice")]
        public double SSIInputPrice { get; set; }
        [DataMember(Name = "ssiIr")]
        public int SSIIr { get; set; }
        [DataMember(Name = "ssiLabel")]
        public string SSILabel { get; set; }
        [DataMember(Name = "ssiProductType")]
        public string SSIProductType { get; set; }
        [DataMember(Name = "ssiProductTypeId")]
        public int SSIProductTypeId { get; set; }
        [DataMember(Name = "ssiSampleCountryCode")]
        public string SSISampleCountryCode { get; set; }
        [DataMember(Name = "ssiSampleCountryId")]
        public string SSISampleCountryId { get; set; }
        [DataMember(Name = "ssiTitle")]
        public string SSITitle { get; set; }
        [DataMember(Name = "ssiVendorUsed")]
        public string SSIVendorUsed { get; set; }
        [DataMember(Name = "ssiVideosNum")]
        public int SSIVideosNum { get; set; }
        [DataMember(Name = "vendorLine")]
        public bool vendorLine { get; set; }
    }
}
