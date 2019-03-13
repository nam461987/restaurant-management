angular.module("gcApp.filters", []).filter("svcDate", [
    "Config", function (Config) {
        return function (text) {
            if (text != null && text !== "") {
                return moment(text).format(Config.defaultConfig.dateFormat);
            }
            return "";
        };
    }
]).filter("svcDateTime", [
    "Config", function (Config) {
        return function (text) {
            if (text != null && text !== "") {
                return moment(text).format(Config.defaultConfig.dateTimeFormat);
            }
            return "";
        };
    }
]).filter("svcMoney", function () {
    return function (text) {
        var v = parseInt(text);
        if (isNaN(v)) {
            return 0;
        } else {
            return v.formatMoney(0, ",", ".");
        }
    };
}
    ).filter("svcOption", [
        "svcCache", function (svcCache) {
            return function (text, fieldName, optionUrl) {
                var result = "";
                switch (typeof optionUrl) {
                    case "string":
                        var cacheKey = 'options_' + fieldName + '_' + optionUrl;
                        var optcache = svcCache.get(cacheKey);
                        if (typeof (optcache) != "undefined") {
                            for (var k = 0; k < optcache.length; k++) {
                                if (optcache[k].Value == text) {
                                    result = optcache[k].DisplayText;
                                }
                            }
                        }
                        break;
                    case "object":
                        var cacheKey2 = 'options_' + fieldName + '_array';
                        var optcache2 = svcCache.get(cacheKey2);
                        if (typeof (optcache2) != "undefined") {
                            for (var k = 0; k < optcache2.length; k++) {
                                if (optcache2[k].Value == text) {
                                    result = optcache2[k].DisplayText;
                                }
                            }
                        }
                        break;

                }
                
                return result;
            };
        }
    ]).filter("svcNumber", function () {
        return function (text) {
            var v = parseInt(text);
            if (isNaN(v)) {
                return 0;
            } else {
                return v.formatMoney(0, ",", ".");
            }
        };
    }
    ).filter("svcImage", function () {
        return function (text, width, height) {
            var w = parseInt(width); if (isNaN(w)) { w = 100 };
            var h = parseInt(height); if (isNaN(h)) { h = 100 };
            var result = "";
            if (text != "") {
                result = "<img src='{0}' width='{1}' height='{2}' alt='' />".format(text, w, h);
            } else {
                result = "<img src='/Content/img/no_image.jpg' width='{1}' height='{2}' alt='' />".format(w, h);
            }
            return result;
        };
    }
    ).filter("svcActive", function () {
        return function (text) {
            var ac = "Active";
            var iac = "Inactive";
            var v = parseInt(text);
            if (v == 1) {
                return ac;
            }
            if (v == 0) {
                return iac;
            }
        };
    }
    );