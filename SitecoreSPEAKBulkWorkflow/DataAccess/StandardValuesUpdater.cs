using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.Workflows;

namespace MikeRobbins.BulkWorkflow.DataAccess
{
    public class StandardValuesUpdater
    {
        public void ApplyWorkflowToStandardValues(IWorkflow workflow, List<Item> standardValues)
        {
            foreach (var standardValue in standardValues)
            {
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    standardValue.Editing.BeginEdit();

                    standardValue.Fields["__Default workflow"].Value = workflow.WorkflowID;

                    standardValue.Editing.EndEdit();
                }
            }
        }
    }
}