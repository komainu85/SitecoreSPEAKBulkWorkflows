namespace MikeRobbins.BulkWorkflow.Models
{
    public class Template : Sitecore.Services.Core.Model.EntityIdentity
    {
        public string WorkflowID { get; set; }
        public string DisplayName { get; set; }
    }
}