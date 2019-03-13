'use strict';

var app = angular.module('gcApp.controllers', []);

app.controller("ListController",

    function ($scope, popupService, $window, $location, Config, svcCache, svcQueue, Model) {
        $scope.offset = 0;
        $scope.limit = 10;
        $scope.predicate = Config.predicate;
        $scope.reverse = false;
        $scope.permissions = permissions;

        $scope.condition = {
            Keyword: ""
        };

        $scope.currentPage = 1;
        $scope.totalRecord = 0;

        $scope.order = function (predicate, reverse) {
            $scope.reverse = reverse;
            $scope.predicate = predicate;
            $scope.reload();
        };
        // callback for ng-click 'add':
        $scope.create = function () {
            $location.path('/create');
        };

        // callback for ng-click 'edit':
        $scope.edit = function (id) {
            $location.path('/edit/' + id);
        };

        $scope.changedValue = function (id) {
            Model.GetBranchListByRestaurantId({ id: id }, function (response) {
                $scope.options.BranchId = response.Records;
            })
        }

        // callback for ng-click 'remove':
        $scope.delete = function (id, status) {
            status = parseInt(status);
            if (status == 1) {
                var obj = {
                    Id: id, Status: 0
                };
            }
            if (status == 0) {
                var obj = {
                    Id: id, Status: 1
                };
            }
            $scope.applySuccess = Model.DeleteMenu_Unit({
                obj: obj
            }, function (response) {
                if (response.Result == 1) {
                    $scope.reload();
                }
                else {
                    alert("Failed !!! Try again");
                }
            });
        };

        $scope.reload = function () {
            $('.overlay').removeClass('hidden');
            var od = {};
            od[$scope.predicate] = ($scope.reverse == true ? "DESC" : "ASC");
            var obj = {
                _c: {
                    //"OperatorId": oid,
                    //"Status": 1
                },
                _f: $.map(Config.fields, function (f) {
                    if (f.list === true) {
                        return f.field;
                    }
                    return null;
                }).join(','),
                _od: $scope.order,
                _os: $scope.offset,
                _lm: $scope.limit
            };

            for (var k in $scope.condition) {
                if ($scope.condition.hasOwnProperty(k)) {
                    switch (k) {
                        case "Keyword":
                            if ($scope.condition[k].length > 3) {
                                obj._c[Config.condition.field] = Config.condition.query.format($scope.condition[k]);
                            }
                            break;
                        default:
                            if ($scope.condition[k].Value != "0") {
                                obj._c[k] = $scope.condition[k].Value;
                            }
                            break;
                    }
                }
            }
            Model.GetAllMenu_Unit({ obj: obj }, function (response) {
                if (response.Result == 1) {
                    $scope.records = response.Records;
                    $scope.totalRecord = response.TotalRecordCount;
                    $('.overlay').addClass('hidden');
                }
            });
        };

        $scope.reset = function () {
            $scope.offset = 0;
            $scope.limit = 10;
            $scope.predicate = Config.predicate;
            $scope.reverse = false;

            $scope.currentPage = 1;
            $scope.totalRecord = 0;

            $scope.condition = {
                Keyword: ""
                //TypeId: $scope.options['TypeId'][0]
            };

            //Reload
            $scope.reload();
        };

        $scope.fields = $.map(Config.fields, function (f) {
            if (f.list === true) {
                return f;
            }
            return null;
        });

        //Options
        $scope.options = {};
        $scope.$watch(function () { return svcCache.get('appReady'); }, function (newValue, oldValue, scope) {
            if (newValue === true || oldValue === false) {
                for (var i = 0; i < Config.fields.length; i++) {
                    var f = Config.fields[i];
                    if (f.type == "select" || f.type == "select2") {
                        scope.options[f.field] = [];
                        //var cacheKey = 'options_' + f.field + '_' + f.option;

                        var cacheKey = "";
                        switch (typeof f.option) {
                            case "object":
                                cacheKey = 'options_' + f.field + '_array';
                                break;
                            default:
                                cacheKey = 'options_' + f.field + '_' + f.option;
                                break;
                        }

                        var optcache = svcCache.get(cacheKey);

                        if (typeof optcache != "undefined") {
                            scope.options[f.field] = optcache;
                            scope.condition[f.field] = optcache[0];
                        }
                    }
                }

                scope.reload();
            }
        });

        //$scope.reload();
    });

app.controller("CreateController",

    function ($scope, $location, svcCache, svcQueue, Config, Model) {

        $scope.record = {};

        $scope.fields = $.map(Config.fields, function (f) {
            if (f.create === true) {
                //$scope.record.MadeTime = moment().format("MM/DD/YYYY HH:mm");
                return f;
            }
            return null;
        });

        $scope.back = function () {
            $location.path('/');
        };

        $scope.ok = function () {
            var obj = {};
            console.log($scope.record);
            for (var j = 0; j < Config.fields.length; j++) {
                var f = Config.fields[j];
                if (typeof $scope.record[f.field] != "undefined") {
                    switch (f.type) {
                        case 'date':
                            //obj._d[f.field] = moment(record[f.field], Config.defaultConfig.dateFormat).format(Config.defaultConfig.isoDateTimeFormat);
                            obj[f.field] = moment($scope.record[f.field], "MM/DD/YYYY 00:00:00").format(Config.defaultConfig.isoDateTimeFormat);
                            break;
                        case "datetime":
                            obj[f.field] = moment($scope.record[f.field], "MM/DD/YYYY HH:mm:ss").format(Config.defaultConfig.isoDateTimeFormat);
                            break;
                        case "time":
                            obj[f.field] = $scope.record[f.field].format(Config.defaultConfig.timeFormat);
                            break;
                        case "select":
                            obj[f.field] = $scope.record[f.field].Value;
                            break;
                        case "select2":
                            obj[f.field] = $scope.record[f.field];
                            break;
                        case "textarea":
                            obj[f.field] = $scope.record[f.field] != null && $scope.record[f.field].length > 0 ? $scope.record[f.field].replace(/\r?\n/g, '<br />') : "";
                            break;

                        default:
                            obj[f.field] = $scope.record[f.field];
                            break;
                    }
                }
            }
            $scope.applySuccess = Model.InsertMenu_Unit({
                obj: obj
            }, function (response) {
                if (response.Result == 1) {
                    $location.path('/');
                }
                else {
                    alert("Failed !!! Try again");
                }
            });
        };

        //Options
        $scope.options = {};
        $scope.$watch(function () { return svcCache.get('appReady'); }, function (newValue, oldValue, scope) {
            if (newValue === true || oldValue === false) {
                for (var i = 0; i < Config.fields.length; i++) {
                    var f = Config.fields[i];
                    if (f.type == "select" || f.type == "select2") {
                        scope.options[f.field] = [];
                        //var cacheKey = 'options_' + f.field + '_' + f.option;

                        var cacheKey = "";
                        switch (typeof f.option) {
                            case "object":
                                cacheKey = 'options_' + f.field + '_array';
                                break;
                            default:
                                cacheKey = 'options_' + f.field + '_' + f.option;
                                break;
                        }

                        var optcache = svcCache.get(cacheKey);
                        if (typeof optcache != "undefined") {
                            scope.options[f.field] = optcache;
                            $scope.record[f.field] = f.type == "select" ? optcache[0] : optcache[0].Value;
                        }
                    }
                }
            }
        });
    });
app.controller("UpdateController",

    function ($scope, $stateParams, svcCache, svcQueue, $location, Config, Model) {
        $scope.record = {};

        $scope.fields = $.map(Config.fields, function (f) {
            if (f.edit === true) {
                return f;
            }
            return null;
        });

        $scope.back = function () {
            $location.path('/');
        };

        $scope.ok = function () {
            var obj = {};
            for (var j = 0; j < Config.fields.length; j++) {
                var f = Config.fields[j];
                if (typeof $scope.record[f.field] != "undefined") {
                    switch (f.type) {
                        case 'date':
                            //obj._d[f.field] = moment(record[f.field], Config.defaultConfig.dateFormat).format(Config.defaultConfig.isoDateTimeFormat);
                            obj[f.field] = moment($scope.record[f.field], "MM/DD/YYYY 00:00:00").format(Config.defaultConfig.isoDateTimeFormat);
                            break;
                        case "datetime":
                            obj[f.field] = moment($scope.record[f.field], "MM/DD/YYYY HH:mm:ss").format(Config.defaultConfig.isoDateTimeFormat);
                            break;
                        case "time":
                            obj[f.field] = $scope.record[f.field].format(Config.defaultConfig.timeFormat);
                            break;
                        case "select":
                            obj[f.field] = $scope.record[f.field].Value;
                            break;
                        case "select2":
                            obj[f.field] = $scope.record[f.field];
                            break;
                        case "textarea":
                            obj[f.field] = $scope.record[f.field] != null && $scope.record[f.field].length > 0 ? $scope.record[f.field].replace(/\r?\n/g, '<br />') : "";
                            break;

                        default:
                            obj[f.field] = $scope.record[f.field];
                            break;
                    }
                }
            }
            $scope.applySuccess = Model.UpdateMenu_Unit({
                obj: obj
            }, function (response) {
                if (response.Result == 1) {
                    $location.path('/');
                }
                else {
                    alert("Failed !!! Try again");
                }
            });
        };

        //Options
        $scope.options = {};
        $scope.$watch(function () { return svcCache.get('appReady'); }, function (newValue, oldValue, scope) {
            if (newValue === true || oldValue === false) {
                for (var i = 0; i < Config.fields.length; i++) {
                    var f = Config.fields[i];
                    if (f.type == "select" || f.type == "select2") {
                        scope.options[f.field] = [];
                        //var cacheKey = 'options_' + f.field + '_' + f.option;

                        var cacheKey = "";
                        switch (typeof f.option) {
                            case "object":
                                cacheKey = 'options_' + f.field + '_array';
                                break;
                            default:
                                cacheKey = 'options_' + f.field + '_' + f.option;
                                break;
                        }

                        var optcache = svcCache.get(cacheKey);
                        if (typeof optcache != "undefined") {
                            scope.options[f.field] = optcache;
                            $scope.record[f.field] = f.type == "select" ? optcache[0] : optcache[0].Value;
                        }
                    }
                }
            }
        });

        var obj = {
            _c: {
                "Id": $stateParams.id
            }
        };

        $scope.AllDay = Config.AllDay;
        //Records
        Model.GetAllMenu_Unit({ obj: obj }, function (response) {
            if (response.Result == 1) {
                $scope.record = response.Records[0];
                $scope.record.Description = (response.Records[0].Description == null || response.Records[0].Description.length > 0) ? response.Records[0].Description : response.Records[0].Description.replace('<br />', '\n');
            }
        });
    });