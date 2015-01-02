require.config({
    paths: {
        entityService: "/sitecore/shell/client/Services/Assets/lib/entityservice"
    }
});

define(["jquery", "sitecore", "entityService"], function ($, Sitecore, entityService) {
    "use strict";

    var model = Sitecore.Definitions.Models.ComponentModel.extend({
          initialize: function (attributes) {
              this._super();

              this.set("isBusy", false);
              this.set("entities", []);
              this.set("serviceURL", "");

              this.on("change:serviceURL", this.refresh, this);
          },


          refresh: function () {

              this.Service = new entityService({
                  url: this.get("serviceURL")
              });

              this.query = this.Service.fetchEntities();

              if (!this.IsDeferred) {
                  this.execute();
              }
          },

          execute: function () {
              var comp = this;
              this.query.execute().then(function (resultentities) {
                  var test = '';

                  comp.set("entities", resultentities);
              }).fail(function (error) {
                  console.log("Error while calling the entities service");
                  console.log(error);
              });
          }
      }
    );

    var view = Sitecore.Definitions.Views.ComponentView.extend(
      {
          initialize: function () {
              this._super();
          }
      }
    );

    Sitecore.Factories.createComponent("EntitiesDataSource", model, view, "script[type='text/x-sitecore-entitiesdatasource']");
});