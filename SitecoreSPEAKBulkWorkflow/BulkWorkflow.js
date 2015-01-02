define(["sitecore", "jquery", "underscore"], function (Sitecore,$,_) {
    var BulkWorkflow = Sitecore.Definitions.App.extend({

        initialized: function () { },

        initialize: function () {
            this.GetWorkflows();
        },

        ApplyWorkflow: function () {
            this.pi.viewModel.show();

            var selectedWorkflow = this.workflow.viewModel.selectedValue();
            var selectedTemplates= this.tvTemplates.viewModel.checkedItemIds();

            $.ajax({
                url: "/api/sitecore/BulkWorkflow/ApplyWorkflow",
                type: "POST",
                data: { workflow: selectedWorkflow, "selectedTemplates": selectedTemplates},
                context: this,
                success: function (data) {
                    if (data == "True") {
                        this.miMessages.addMessage("notification", { text: "Workflow applied successfully for " + this.workflow.viewModel.selectedItem().DisplayName, actions: [], closable: true, temporary: true });
                    } else {
                        this.miMessages.addMessage("warning", "An error occured applying workflow for " + this.workflow.viewModel.selectedItem().DisplayName + " , please try again");
                    }
                    this.pi.viewModel.hide();
                }
            });
        },

        GetWorkflows: function ()
        {
            var workflowService = new EntityService({
                url: "/sitecore/api/ssc/MikeRobbins-BulkWorkflow-Controllers/bulkworkflow"
            });
            var result = workflowService.fetchEntities().execute().then(function (workflows)
            {
                for (var i = 0; i < workflows.length; i++) {
                    var obj = workflows[i];
                    this.JsonDS.add(obj.json());
                }
            });


        }
    });

    return BulkWorkflow;
});