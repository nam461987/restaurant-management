var config = angular.module("gcApp.config", []).factory("Config", function () {
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
        { field: "Name", name: "Name", create: true, edit: true, list: true },
        { field: "Description", name: "Description", create: true, edit: true, list: true, type: "textarea" },
        { field: "OpenTime", name: "Open Time", create: true, edit: true, list: true, type: "time" },
        { field: "CloseTime", name: "Close Time", create: true, edit: true, list: true, type: "time" },
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