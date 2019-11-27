using System;
using Dynata.SamplifyAPIClient;

namespace SamplifyAPIClientTest
{
    public static class Helper
    {
        public static ProjectCriteria GetTestProject()
        {
            return new ProjectCriteria
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
            var filters = new QuotaFilters[]
            {
                new QuotaFilters
                {
                    AttributeID = "4091",
                    Options = new string[] {"3", "4"}
                }
            };

            var quotaGroups = new QuotaGroup[]
            {
                new QuotaGroup
                {
                    Name = "Gender distribution",
                    QuotaCells = new QuotaCell[]
                    {
                        new QuotaCell
                        {
                            QuotaNodes = new QuotaNode[] {new QuotaNode{AttributeID = "11", OptionIDs = new string[]{"1"}}},
                            Perc=30
                        },
                        new QuotaCell
                        {
                            QuotaNodes = new QuotaNode[] {new QuotaNode{AttributeID = "11", OptionIDs = new string[]{"2"}}},
                            Perc=70
                        }
                    }
                }
            };

            return new LineItemCriteria
            {
                ExtLineItemID = "lineItem001",
                Title = "US College",
                CountryISOCode = "US",
                LanguageISOCode = "en",
                SurveyURL = "www.mysurvey.com/live/survey?pid=2424131312&k2=59931&psid=VgrJ2-9iUQZK3noVDtXobw",
                SurveyTestURL = "www.mysurvey.com/test/survey?pid=2424131312&k2=59931&psid=VgrJ2-9iUQZK3noVDtXobw",
                IndicativeIncidence = 20.0M,
                DaysInField = 20,
                LengthOfInterview = 10,
                RequiredCompletes = 200,
                QuotaPlan = new QuotaPlan
                {
                    Filters = filters,
                    QuotaGroups = quotaGroups
                }
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
