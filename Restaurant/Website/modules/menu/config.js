﻿var config = angular.module("gcApp.config", []).factory("Config", function () {
    var obj = {};
    obj.defaultConfig = {
        datePickerFormat: "dd/mm/yyyy",
        dateFormat: "DD/MM/YYYY",
        dateTimeFormat: "DD/MM/YYYY HH:mm:ss",
        isoDateTimeFormat: "YYYY-MM-DDTHH:mm:ss.000",
        timeFormat: "HH:mm"
    };

    obj.ApiKey = "";
    obj.currentTable = "";

    obj.predicate = "Name";
    obj.condition = {
        field: "Name",
        query: "PATINDEX('%{0}%',Name) > 0 OR PATINDEX('%{0}%',Description) > 0"
    };

    obj.fields = [
        { field: "Id", name: "#", create: false, edit: false, list: true, type: "hidden" },
        { field: "RestaurantId", name: "Restaurant", create: false, edit: false, list: true, type: "select2", option: "/Category/GetRestaurantList", required: true },
        { field: "BranchId", name: "Branch", create: true, edit: true, list: true, type: "select2", option: "/Category/GetBranchList", required: true },
        { field: "CategoryId", name: "Category", create: true, edit: true, list: true, type: "select2", option: "/Category/GetMenu_CategoryList", required: true },
        { field: "Name", name: "Name", create: true, edit: true, list: true },
        { field: "Price", name: "Price", create: true, edit: true, list: true },
        { field: "UnitId", name: "Unit", create: true, edit: true, list: true, type: "select2", option: "/Category/GetMenu_UnitList", required: true },
        { field: "Image", name: "Image", create: true, edit: true, list: true, type: "upload" },
        { field: "Description", name: "Description", create: true, edit: true, list: true, type: "textarea" },
        { field: "Status", name: "Status", create: false, edit: false, list: true, type: "active" }
    ];

    obj.fieldsSecond = [
        { field: "Id", name: "#", create: false, edit: false, list: true, type: "hidden" },
        { field: "RestaurantId", name: "Restaurant", create: false, edit: false, list: true, type: "select2", option: "/Category/GetRestaurantList", required: true },
        { field: "BranchId", name: "Branch", create: false, edit: false, list: false, type: "select2", option: "/Category/GetBranchList", required: true },
        { field: "MenuId", name: "Menu", create: false, edit: false, list: true, type: "hidden", required: true },
        { field: "IngredientId", name: "Ingredient", create: true, edit: true, list: true, type: "select2", option: "/Category/GetIngredientList", required: true },
        { field: "Quantity", name: "Quantity", create: true, edit: true, list: true, type:"number" },
        { field: "UnitId", name: "Unit", create: true, edit: true, list: true, type: "select2", option: "/Category/GetMenu_UnitList", required: true },
        { field: "Description", name: "Description", create: true, edit: true, list: true, type: "textarea" },
        { field: "Status", name: "Status", create: false, edit: false, list: true, type: "active" }
    ];

    obj.getFields = function (tblname) {
        var result = [];
        var match = false;
        for (var i = 0; i < this.models.length && !match; i++) {
            if (this.models[i].name === tblname) {
                result = this.models[i].fields;
                match = true;
            }
        }
        return result;
    };

    obj.getCacheKey = function (field, option) {
        var result = "";
        switch (typeof option) {
            case "string":
                //result = btoa("{0}_options_{1}_{2}".format(tblname, field, option));
                result = "options_{0}_{1}".format(field, option);
                break;
            case "object":
                result = "options_{0}_{1}".format(field, "array");
                //result = btoa("{0}_options_{1}_{2}".format(tblname, field, "array"));
                break;
        }
        return result;
    };

    return obj;
});