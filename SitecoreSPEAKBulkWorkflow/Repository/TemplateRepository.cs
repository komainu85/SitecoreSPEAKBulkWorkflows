using MikeRobbins.BulkWorkflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace MikeRobbins.BulkWorkflow.Repository
{
    public class TemplateRepository : Sitecore.Services.Core.IRepository<Template>
    {
        public void Add(Template entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Template entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Template entity)
        {
            var templateFolder = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(entity.Id));

            return templateFolder != null;
        }

        public Template FindById(string id)
        {
            var template = Sitecore.Data.Database.GetDatabase("master").GetTemplate(new ID(id));

            if (template != null)
            {
                return new Template() { Id = template.ID.ToString(), DisplayName = template.DisplayName };
            }
            else
            {
                var templateFolder = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(id));

                return new Template() { Id = templateFolder.ID.ToString(), DisplayName = templateFolder.DisplayName };
            }


        }

        public IQueryable<Template> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Template entity)
        {
            var workflowItem = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflow(entity.WorkflowID);
            var template = Sitecore.Data.Database.GetDatabase("master").GetTemplate(new ID(entity.Id));

            var standardValue = template.StandardValues;

            if (standardValue == null)
            {
                standardValue = template.CreateStandardValues();
            }

            var standardValuesUpdater = new DataAccess.StandardValuesUpdater();
            standardValuesUpdater.ApplyWorkflowToStandardValues(workflowItem, standardValue);
        }
    }
}