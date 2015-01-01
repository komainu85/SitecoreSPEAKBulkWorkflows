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
using MikeRobbins.BulkWorkflow.Repository;

namespace MikeRobbins.BulkWorkflow.Controllers
{
    [ValidateAntiForgeryToken]
    [ServicesController]
    public class TemplateController : EntityService<Template>
    {
        public TemplateController(IRepository<Template> repository)
            : base(repository)
        {
        }

        public TemplateController()
            : this(new TemplateRepository())
        {
        }

    }
}