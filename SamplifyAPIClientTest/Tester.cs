using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dynata.SamplifyAPIClient;
using Xunit;
namespace SamplifyAPIClientTest
{
    public class TempTest
    {
        [Fact]
        public async Task TestAll()
        {
            Random random = new Random();
            string projId = "projectTest" + random.Next();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            SamplifyClient testClient = new SamplifyClient("api", "samplifyweb", "samplifyweb", SamplifyEnv.UAT);

            //// WORKS
            Trace.WriteLine("GetAllProjects: ");
            GetAllProjectsResponse myVar01 = await testClient.GetAllProjects(null);
            string tmp = JsonConvert.SerializeObject(myVar01);
            Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("CreateProject: ");
            //ProjectResponse myVar02 = await testClient.CreateProject(Helper.GetTestProject2(projId));
            //string ProjId = myVar02.Data.ExtProjectID;
            //tmp = JsonConvert.SerializeObject(myVar02);
            //Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("UpdateProject (" + ProjId + "): ");
            //ProjectCriteria projCrit = Helper.GetTestProject2(projId);
            //projCrit.Title = "new title";
            //ProjectResponse myVar03 = await testClient.UpdateProject(projCrit);
            //tmp = JsonConvert.SerializeObject(myVar03);
            //Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("CreateLineItem: ");
            //LineItemCriteria lineItemCrit = Helper.GetTestLineItem();
            //lineItemCrit.ExtLineItemID = "lineitem002";
            //LineItemResponse myVar04 = await testClient.AddLineItem(ProjId, lineItemCrit);
            //tmp = JsonConvert.SerializeObject(myVar04);
            //Trace.WriteLine(tmp);

            //// WORKS
            //string LineItemId = myVar04.Data.ExtLineItemID;
            //Trace.WriteLine("UpdateLineItem: ");
            //LineItemResponse myVar05 = await testClient.UpdateLineItem(ProjId, LineItemId, Helper.GetTestUpdateLineItem());
            //tmp = JsonConvert.SerializeObject(myVar05);
            //Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("GetFeasibility: ");
            //GetFeasibilityResponse myVar06 = await testClient.GetFeasibility(ProjId);
            //tmp = JsonConvert.SerializeObject(myVar06);
            //Trace.WriteLine(tmp);
            //System.Threading.Thread.Sleep(30000);
            //GetFeasibilityResponse myVar06a = await testClient.GetFeasibility(ProjId);
            //tmp = JsonConvert.SerializeObject(myVar06a);
            //Trace.WriteLine(tmp);

            ////// WORKS
            //Trace.WriteLine("BuyProject (" + ProjId + "): ");
            //BuyProjectResponse myVar07 = await testClient.BuyProject(ProjId, Helper.GetTestBuyProjectCriteria());
            //tmp = JsonConvert.SerializeObject(myVar07);
            //Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("updateLineItemState: ");
            //UpdateLineItemStateResponse myVar08 = await testClient.UpdateLineItemState(ProjId, LineItemId, "close");
            //tmp = JsonConvert.SerializeObject(myVar08);
            //Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("GetAllEvents: ");
            //GetAllEventsResponse myVar09 = await testClient.GetAllEvents(null);
            //tmp = JsonConvert.SerializeObject(myVar09);
            //Trace.WriteLine(tmp);

            //// WORKS
            //Trace.WriteLine("GetEventBy: ");
            //EventResponse myVar10 = await testClient.GetEventBy(170);
            //tmp = JsonConvert.SerializeObject(myVar10);
            //Trace.WriteLine(tmp);

            // WORKS
            //Trace.WriteLine("GetAttributes: ");
            //GetAttributesResponse myVar11 = await testClient.GetAttributes("US", "en", null);
            //tmp = JsonConvert.SerializeObject(myVar11);
            //Trace.WriteLine(tmp);
            
            //// WORKS
            //Trace.WriteLine("GetEvent: ");
            //EventResponse myVar12 = await testClient.GetEventBy(223);
            //string tmp12 = JsonConvert.SerializeObject(myVar12);
            //Trace.WriteLine(tmp12);
        }
    }
}

