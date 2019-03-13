angular.module("gcApp", ["ui.router", "ui.select2", "ngFileUpload", "ngResource", "ngAnimate", "ngSanitize", "gcApp.controllers", "gcApp.config", "gcApp.queue", "gcApp.services", "gcApp.caches", "gcApp.directives", "gcApp.filters"]);
angular.module("gcApp").config(function ($stateProvider) {
    $stateProvider.state("list", { // state for showing all lyt_PT_Customers
        url: "/",
        templateUrl: "/modules/menu/partials/list.html",
        controller: "ListController"
    }).state("create", { //state for adding a new lyt_PT_Customer
        url: "/create",
        templateUrl: "/modules/menu/partials/create.html",
        controller: "CreateController"
    }).state("edit", { //state for adding a new lyt_PT_Customer
        url: "/edit/:id",
        templateUrl: "/modules/menu/partials/create.html",
        controller: "UpdateController"
    }).state("define", { //state for adding a new lyt_PT_Customer
        url: "/define/:id",
        templateUrl: "/modules/menu/partials/define.html",
        controller: "DefineController"
    }).state("define_edit", { //state for adding a new lyt_PT_Customer
        url: "/define_edit/:id",
        templateUrl: "/modules/menu/partials/define_create.html",
        controller: "Update_Menu_DefineController"
    });
}).run(function ($state) {
    $state.go("list"); //make a transition to lyt_PT_Customers state when app starts
});
