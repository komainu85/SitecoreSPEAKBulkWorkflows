require.config({
    paths: {
        entityService: "/sitecore/shell/client/Services/Assets/lib/entityservice"
    }
});

define(["sitecore", "jquery", "underscore", "entityService"], function (Sitecore, $, _, entityService) {
    var BulkWorkflow = Sitecore.Definitions.App.extend({

        initialized: function () {
            this.GetWorkflows();
        },

        initialize: function () { },

        ApplyWorkflow: function () {
            this.pi.viewModel.show();

            var selectedWorkflow = this.workflow.viewModel.selectedValue();
            var selectedTemplates = this.tvTemplates.viewModel.checkedItemIds();


            var templateService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-BulkWorkflow-Controllers/template"
            });

   

            for (var i = 0; i < selectedTemplates.length; i++) {
                var selectedTemplate = selectedTemplates[i];

                templateService.fetchEntity(selectedTemplate).execute(function (template) {

                    template.should.be.an.instanceOf(entityService.Entity);

                    template.WorkflowID = selectedWorkflow;

                    template.save().then(function (savedTemplate) {
                        savedTemplate.WorkflowID.should.eql(selectedWorkflow);
                        done();

                    }).fail(done);

                }).fail(done);

            }






            $.ajax({
                url: "/api/sitecore/BulkWorkflow/ApplyWorkflow",
                type: "POST",
                data: { workflow: selectedWorkflow, "selectedTemplates": selectedTemplates },
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

        GetWorkflows: function () {
            var datasource = this.JsonDS;

            var workflowService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-BulkWorkflow-Controllers/bulkworkflow"
            });

            var result = workflowService.fetchEntities().execute().then(function (workflows) {
                this.workflow.viewModel.items = workflows;

                for (var i = 0; i < workflows.length; i++) {
                    datasource.add(workflows[i]);
                }
            });

        }
    });

    return BulkWorkflow;
});