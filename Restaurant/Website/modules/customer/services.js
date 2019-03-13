angular.module("gcApp.services", [])
    .factory("Model", ["$resource", "Config", function ($resource, Config) {

        var Model = $resource("", {}, {
            InsertCustomer: {
                url: "/Customer/InsertCustomer",
                method: "POST",
                headers: {
                    //"X-ApiKey": Config.ApiKey
                },
                transformRequest: function (data, headersGetter) {
                    return angular.toJson(data.obj);
                },
                transformResponse: function (data, headersGetter) {
                    var result = angular.fromJson(data);
                    return result;
                }
            },
            GetAllCustomer: {
                url: "/Customer/GetAllCustomer",
                method: "POST",
                headers: {
                    //"X-ApiKey": Config.ApiKey
                },
                transformRequest: function (data, headersGetter) {
                    return angular.toJson(data.obj);
                },
                transformResponse: function (data, headersGetter) {
                    var result = angular.fromJson(data);
                    return result;
                }
            },
            UpdateCustomer: {
                url: "/Customer/UpdateCustomer",
                method: "POST",
                headers: {
                    //"X-ApiKey": Config.ApiKey
                },
                transformRequest: function (data, headersGetter) {
                    return angular.toJson(data.obj);
                },
                transformResponse: function (data, headersGetter) {
                    var result = angular.fromJson(data);
                    return result;
                }
            },
            DeleteCustomer: {
                url: "/Customer/DeleteCustomer",
                method: "POST",
                headers: {
                    //"X-ApiKey": Config.ApiKey
                },
                transformRequest: function (data, headersGetter) {
                    return angular.toJson(data.obj);
                },
                transformResponse: function (data, headersGetter) {
                    var result = angular.fromJson(data);
                    return result;
                }
            },
            GetBranchListByRestaurantId: {
                url: "/Category/GetBranchListByRestaurantId",
                method: "POST",
                headers: {
                    //"X-ApiKey": Config.ApiKey
                },
                transformRequest: function (data, headersGetter) {
                    return angular.toJson(data);
                },
                transformResponse: function (data, headersGetter) {
                    var result = angular.fromJson(data);
                    return result;
                }
            }
        });

        return Model;
    }])
    .service("popupService", function ($window) {
        this.showPopup = function (message) {
            return $window.confirm(message);
        }
    });