using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.BulkWorkflow.Controllers
{
    public class BulkWorkflowController : Controller
    {
        public string GetAllWorkflows()
        {
            var workflows = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflows();

            var results = workflows.Select(workflow => new Models.Workflow() {DisplayName = workflow.Appearance.DisplayName, WorkflowID = workflow.WorkflowID}).ToList();

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