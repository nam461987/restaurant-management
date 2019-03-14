﻿angular.module("gcApp.services", [])
    .factory("Model", ["$resource", "Config", function ($resource, Config) {

        var Model = $resource("", {}, {
            InsertOrder_Channel: {
                url: "/Category/InsertOrder_Channel",
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
            GetAllOrder_Channel: {
                url: "/Category/GetAllOrder_Channel",
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
            UpdateOrder_Channel: {
                url: "/Category/UpdateOrder_Channel",
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
            DeleteOrder_Channel: {
                url: "/Category/DeleteOrder_Channel",
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