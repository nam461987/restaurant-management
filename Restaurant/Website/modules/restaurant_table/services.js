angular.module("gcApp.services", [])
    .factory("Model", ["$resource", "Config", function ($resource, Config) {

        var Model = $resource("", {}, {
            InsertRestaurant_Table: {
                url: "/Category/InsertRestaurant_Table",
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
            GetAllRestaurant_Table: {
                url: "/Category/GetAllRestaurant_Table",
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
            UpdateRestaurant_Table: {
                url: "/Category/UpdateRestaurant_Table",
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
            DeleteRestaurant_Table: {
                url: "/Category/DeleteRestaurant_Table",
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