using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using MikeRobbins.BulkWorkflow.Models;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace MikeRobbins.BulkWorkflow.Controllers
{
    [ServicesController]
    public class BulkWorkflowSSCController :  EntityService<Workflow>
    {
        public BulkWorkflowSSCController(IRepository<Workflow> repository)
            : base(repository)
        {
        }
    }
}