using System;
using System.Text;
using Dynata.SamplifyAPIClient;

namespace SamplifyAPIClientTest
{
    public static class Helper
    {

        public static ProjectCriteria GetTestProject()
        {
            Int32 timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            StringBuilder projectID = new StringBuilder("project-");
            projectID.Append(timestamp);
            return new ProjectCriteria
            {
                ExtProjectID = projectID.ToString(),
                Title = "Test Survey",
                NotificationEmails = new string[] { "api-test@researchnow.com" },
                Category = new Category
                {
                    SurveyTopic = new string[] { "DESTINATIONS_TOURISM" },
                    StudyType = new string[] { "ADHOC" }
                },
                Devices = new string[] { DeviceTypeConstants.DeviceTypeDesktop, DeviceTypeConstants.DeviceTypeTablet, DeviceTypeConstants.DeviceTypeMobile },
                LineItems = new LineItemCriteria[] { GetTestLineItem() }
            };
        }

        public static UpsertPermissionsCriteria UpdatePermissions(String ExtProjID)
        {
            var users = new UserPermission[]
            {
                new UserPermission
                {
                    ID = 3,
                    Role = "SAMPLE_MANAGER",
                }
            };

            var teams = new TeamPermission[]
            {
                new TeamPermission
                {
                    ID = 109,
                }
            };
            return new UpsertPermissionsCriteria
            {
                ExtProjectID = ExtProjID,
                UserPermissions = users,
                TeamPermissions = teams
            };
        }

        public static LineItemCriteria GetTestLineItem()
        {
               
            var filters = new QuotaFilters[]
            {
                //new QuotaFilters
                //{
                //    AttributeID = "11",
                //    Options = new string[] {"90007", "95134"}
                //}
            };

            var quotaGroups = new QuotaGroup[]
            {
                new QuotaGroup
                {
                    Name = "Age distribution",
                    QuotaCells = new QuotaCell[]
                    {
                        new QuotaCell
                        {
                            QuotaNodes = new QuotaNode[] {new QuotaNode{AttributeID = "13", OptionIDs = new string[]{"46-70"}}},
                            Perc=30
                        },
                        new QuotaCell
                        {
                            QuotaNodes = new QuotaNode[] {new QuotaNode{AttributeID = "13", OptionIDs = new string[]{"18-45"}}},
                            Perc=30
                        },
                        new QuotaCell
                        {
                            QuotaNodes = new QuotaNode[] {new QuotaNode{AttributeID = "13", OptionIDs = new string[]{"70-99"}}},
                            Perc=40
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
                IndicativeIncidence = 70,
                DaysInField = 2,
                LengthOfInterview = 5,
                RequiredCompletes = 10,
                DeliveryType = "BALANCED",
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
