﻿angular.module("gcApp.directives", []).directive("svcPagination", [
    function () {
        return {
            restrict: "E",
            link: function (scope, element, attrs) {

                scope.$watchGroup(["totalRecord", "currentPage"], function () {
                    //var pages = [];
                    var totalPage = Math.ceil(parseInt(attrs.totalRecord) / (parseInt(scope.limit)));

                    var numPrevPages = 5;
                    var numNextPages = 5;

                    var prevPagesArr = [];
                    for (var i = scope.currentPage - numPrevPages; i < scope.currentPage; i++) {
                        if (i >= 1) {
                            prevPagesArr.push(i);
                        }
                    }

                    var nextPagesArr = [];
                    for (var j = scope.currentPage + 1; j < scope.currentPage + numNextPages; j++) {
                        if (j <= totalPage) {
                            nextPagesArr.push(j);
                        }
                    }

                    scope.prevPagesArr = prevPagesArr;
                    scope.nextPagesArr = nextPagesArr;
                    scope.totalPage = totalPage;
                });
                scope.setCurrent = function (c) {
                    scope.currentPage = c;
                    scope.offset = (c - 1) * scope.limit;
                    scope.reload();
                };

                scope.pageOptions = [{
                    Value: 10,
                    DisplayText: "10"
                }, {
                    Value: 50,
                    DisplayText: "50"
                }, {
                    Value: 100,
                    DisplayText: "100"
                }, {
                    Value: 500,
                    DisplayText: "500"
                }, {
                    Value: 1000,
                    DisplayText: "1000"
                }, {
                    Value: 10000,
                    DisplayText: "All"
                }];

                scope.pageLimit = { Value: parseInt(attrs.limit) };

                scope.$watch("pageLimit", function (newValue, oldValue, scope) {
                    if (newValue != oldValue) {
                        scope.limit = parseInt(newValue.Value);
                        scope.currentPage = 1;
                        scope.offset = 0;
                        scope.reload();
                    }
                });
            },
            templateUrl: "/modules/common/partials/pagination.html"
        };
    }
]).directive("dateselect", [
    "Config", function (Config) {
        return {
            require: 'ngModel',
            link: function (scope, el, attr, ngModel) {
                $(el).datepicker({
                    //language: Config.defaultConfig.datePickerLang,
                    format: Config.defaultConfig.datePickerFormat,
                    autoclose: true,
                    weekStart: 1,
                    todayHighlight: true,
                    forceParse: false
                    //onSelect: function (dateText) {
                    //    scope.$apply(function () {
                    //        ngModel.$setViewValue(dateText);
                    //    });
                    //}
                });

                //$(el).datepicker('update');

                el.next().find('i').on('click', function (e) {
                    e.preventDefault();
                    $(el).datepicker("show");
                });

                ngModel.$parsers.push(function (data) {
                    //return data;  
                    //console.log(moment(data, Config.defaultConfig.dateFormat).startOf('day'));
                    return moment(data, Config.defaultConfig.dateFormat).startOf('day');
                });
                ngModel.$formatters.push(function (data) {
                    if (typeof data != "undefined" && data != null) {
                        var abcd = moment(data, Config.defaultConfig.isoDateTimeFormat);
                        data = abcd.startOf('day').format("DD/MM/YYYY");
                        //console.log(data);
                        return data;//.startOf('day').format(Config.defaultConfig.dateFormat);
                    }
                    var abcde = moment().startOf('day').format(Config.defaultConfig.dateFormat);
                    return abcde; //&& data._isAMomentObject
                });
            }
        };
    }
]).directive("timeselect", [
    "Config", function (Config) {
        return {
            require: 'ngModel',
            link: function (scope, el, attr, ngModel) {
                $(el).timepicker({
                    showMeridian: false,
                    showInputs: false
                });
                //el.on('changeTime.timepicker', function (e) {
                //    scope.$apply(function () {
                //        ngModel.$setViewValue(e.value);
                //    });
                //});
                el.next().find('i').on('click', function (e) {
                    e.preventDefault();
                    $(el).timepicker();
                });
                ngModel.$parsers.push(function (data) {
                    return moment(data, Config.defaultConfig.timeFormat);
                });
                ngModel.$formatters.push(function (data) {
                    return data.format(Config.defaultConfig.timeFormat);
                });
            }
        };
    }
]).directive("datetimeselect", [
    "Config", function (Config) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, el, attr, ngModel) {
                $(el).datetimepicker({
                    format: Config.defaultConfig.dateTimeFormat
                });
                el.on('dp.change', function (event) {
                    scope.$apply(function () {
                        var date = moment(event.date);
                        ngModel.$setViewValue(date.format(Config.defaultConfig.dateTimeFormat));
                    });
                });

                //format text from the user (view to model)
                ngModel.$parsers.push(function (data) {
                    return moment(data, Config.defaultConfig.dateTimeFormat).format("MM/DD/YYYY HH:mm:ss");
                });

                //format text going to user (model to view)
                ngModel.$formatters.push(function (data) {
                    console.log(moment(data).format(Config.defaultConfig.dateTimeFormat));
                    return moment(data).format(Config.defaultConfig.dateTimeFormat);
                });

            }
        };
    }
    ]).directive("numberselect", [
        "Config", function (Config) {
            return {
                require: 'ngModel',
                link: function (scope, el, attr, ngModel) {
                    ngModel.$parsers.push(function (data) {
                        return '' + data;
                    });
                    ngModel.$formatters.push(function (data) {
                        return parseFloat(data, 10);
                    });
                }
            };
        }
    ]).directive("ngConfirmClick", [function () {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                var message = attrs.ngConfirmMessage;
                if (message && confirm(message)) {
                    scope.$apply(attrs.ngConfirmClick);
                }
            });
        }
    };
}]).directive("loader", [
    "$rootScope", function ($rootScope) {
        return function ($scope, element, attrs) {
            $scope.$on("loader_show", function () {
                return element.removeClass("hidden");
            });
            return $scope.$on("loader_hide", function () {
                return element.addClass("hidden");
            });
        };
    }
]).directive('ngPrint', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs) {
            el.on('click', function () {
                var elemToPrint = document.getElementById(attrs.printElementId);
                if (elemToPrint) {
                    var myWindow = window.open('', '', 'width=' + screen.width + ',height=' + screen.height);
                    myWindow.document.write('<link rel="stylesheet" type="text/css" href="https://idadmin.futabus.vn/Content/plugins/bootstrap/bootstrap.min.css" media="print" />');
                    myWindow.document.write('<style type="text/css">body{font-family: "Times New Roman", Times, serif;font-size:12pt;}</style>');
                    myWindow.document.write(elemToPrint.innerHTML);
                    myWindow.document.close();
                    myWindow.focus();
                    myWindow.print();
                    //myWindow.close();
                }
            });
        }
    };
}).directive("limitdatetimeselect", [
    "Config", function (Config) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, el, attr, ngModel) {
                var date = new Date(), y = date.getFullYear(), m = date.getMonth();
                var lastDay = new Date(y, m + 2, 0);

                $(el).datetimepicker({
                    format: Config.defaultConfig.dateTimeFormat,
                    maxDate: lastDay
                });
                el.on('dp.change', function (event) {
                    scope.$apply(function () {
                        var date = moment(event.date);
                        ngModel.$setViewValue(date.format(Config.defaultConfig.dateTimeFormat));
                    });
                });

                //format text from the user (view to model)
                ngModel.$parsers.push(function (data) {
                    return moment(data, Config.defaultConfig.dateTimeFormat).format("MM/DD/YYYY HH:mm:ss");
                });

                //format text going to user (model to view)
                ngModel.$formatters.push(function (data) {
                    return moment(data).format(Config.defaultConfig.dateTimeFormat);
                });

            }
        };
    }
]);