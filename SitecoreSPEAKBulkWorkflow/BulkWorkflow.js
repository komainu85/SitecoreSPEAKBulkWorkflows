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
            var messagePanel = this.miMessages;
            var progressIcon = this.pi;

            progressIcon.viewModel.show();

            var selectedWorkflow = this.workflow.viewModel.selectedValue();
            var selectedTemplates = this.tvTemplates.viewModel.checkedItemIds().split("|");;

            var templateService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-BulkWorkflow-Controllers/template"
            });

            var updated = 0;

            for (var i = 0; i < selectedTemplates.length; i++) {
                var selectedTemplate = selectedTemplates[i];

                templateService.fetchEntity(selectedTemplate).execute().then(function (template) {
                    template.WorkflowID = selectedWorkflow;

                    template.save().then(function (savedTemplate) {                        updated++;                        if (updated == selectedTemplates.length) {
                            progressIcon.viewModel.hide();
                        }

                        messagePanel.addMessage("notification", { text: "Workflow applied successfully for " + savedTemplate.DisplayName, actions: [], closable: true, temporary: true });
                    });
                });
            }
        },

        GetWorkflows: function () {
            var datasource = this.JsonDS;

            var workflowService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-BulkWorkflow-Controllers/bulkworkflow"
            });

            var result = workflowService.fetchEntities().execute().then(function (workflows) {
                for (var i = 0; i < workflows.length; i++) {
                    datasource.add(workflows[i]);
                }
            });

        }
    });

    return BulkWorkflow;
});