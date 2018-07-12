using System;
using ResearchNow.SamplifyAPIClient;

namespace SamplifyAPIClientTest
{
    public static class Helper
    {
        public static CreateUpdateProjectCriteria GetTestProject()
        {
            return new CreateUpdateProjectCriteria
            {
                ExtProjectID = "project001",
                Title = "Test Survey",
                NotificationEmails = new string[] { "api-test@researchnow.com" },
                Exclusions = new Exclusions
                {
                    Type = ExclusionTypeConstants.ExclusionTypeProject,
                    List = new string[] { "test" }
                },
                Category = new Category
                {
                    SurveyTopic = new string[] { "DESTINATIONS_TOURISM" }
                },
                Devices = new string[] { DeviceTypeConstants.DeviceTypeTablet, DeviceTypeConstants.DeviceTypeMobile },
                LineItems = new LineItemCriteria[] { GetTestLineItem() }
            };
        }

        public static LineItemCriteria GetTestLineItem()
        {
            var quotaGroup = new QuotaGroup
            {
                Name = "Test distribution",
                Quotas =
                    new Quota[]
                    {
                        new Quota()
                        {
                            AttributeID = "11",
                            Options = new QuotaOption[]
                            {
                                new QuotaOption()
                                {
                                    Option = new string[] { "1" },
                                    Perc = 30.0M
                                },
                                new QuotaOption()
                                {
                                    Option = new string[] { "2" },
                                    Perc = 70.0M
                                }
                            }
                        }
                    }
            };

            var quotaPlan = new QuotaPlan
            {
                Filters = new QuotaFilters[] {
                    new QuotaFilters { AttributeID = "4091", Options = new string[] { "3", "4" } }
                },
                QuotaGroups = new QuotaGroup[] { quotaGroup }
            };

            return new LineItemCriteria
            {
                ExtLineItemID = "lineItem001",
                Title = "Test Line item",
                CountryISOCode = "US",
                LanguageISOCode = "en",
                SurveyURL = "http://www.mysurvey.com/live/survey",
                SurveyTestURL = "http://www.mysurvey.com/test/survey",
                IndicativeIncidence = 20.0M,
                DaysInField = 20,
                LengthOfInterview = 10,
                RequiredCompletes = 200,
                QuotaPlan = quotaPlan
            };
        }

        public static BuyProjectCriteria[] GetTestBuyProjectCriteria()
        {
            return new BuyProjectCriteria[]
            {
                new BuyProjectCriteria
                {
                    ExtLineItemID = "lineItem001",
                    SurveyURL = "http://www.mysurvey.com/live/survey",
                    SurveyTestURL = "http://www.mysurvey.com/test/survey"
                }
            };
        }
    }
}
