using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreSPEAKBulkWorkflow.Controllers
{
    public class BulkWorkflowController : Controller
    {
        public string GetAllWorkflows()
        {
            var workflows = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflows();

            var results = new List<Models.Workflow>();

            foreach (var workflow in workflows)
            {
                results.Add(new Models.Workflow() { DisplayName = workflow.Appearance.DisplayName, WorkflowID = workflow.WorkflowID });
            }

            return JsonConvert.SerializeObject(results);
        }

        public bool ApplyWorkflow(string workflow, string selectedTemplates)
        {
            try
            {
                var workflowItem = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflow(workflow);
                var standardValues = GetTemplateStandardValues(selectedTemplates);

                var standardValuesUpdater = new DataAccess.StandardValuesUpdater();
                standardValuesUpdater.ApplyWorkflowToStandardValues(workflowItem, standardValues);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Item> GetTemplateStandardValues(string items)
        {
            var standardValues = new List<Item>();

            var ids = items.Split(new char[] { '|' });

            var master = Sitecore.Data.Database.GetDatabase("master");

            var sitecoreItems = ids.Select(id => master.GetItem(new ID(id))).ToList();

            foreach (var item in sitecoreItems)
            {
                var standardValue = item.Children.FirstOrDefault(x => x.Name == "__Standard Values");

                if (standardValue != null)
                {
                    standardValues.Add(standardValue);
                }
            }

            return standardValues;
        }
    }
}