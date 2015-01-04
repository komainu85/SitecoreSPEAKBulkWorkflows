using MikeRobbins.BulkWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MikeRobbins.BulkWorkflow.Repository
{
    public class WorkflowRepository : Sitecore.Services.Core.IRepository<Workflow>
    {
        public void Add(Workflow entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Workflow entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Workflow entity)
        {
            throw new NotImplementedException();
        }

        public Workflow FindById(string id)
        {
            var workflow = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflow(id);
            return new Workflow() { DisplayName = workflow.Appearance.DisplayName, Id = workflow.WorkflowID };
        }

        public IQueryable<Workflow> GetAll()
        {
            var workflows = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflows();

            var results = workflows.Select(workflow => new Models.Workflow() { DisplayName = workflow.Appearance.DisplayName, Id = workflow.WorkflowID });

            return results.AsQueryable();
        }

        public void Update(Workflow entity)
        {
            throw new NotImplementedException();
        }
    }
}