var config = angular.module('ngService.config', []);

config.factory('Config', function () {
    var obj = {};
    obj.defaultConfig = {
        datePickerFormat: "dd/mm/yyyy",
        dateFormat: "DD/MM/YYYY",
        dateTimeFormat: "DD/MM/YYYY HH:mm:ss",
        isoDateTimeFormat: "YYYY-MM-DDTHH:mm:ss.000",
        timeFormat: "HH:mm"
    };
    obj.fields = [
        { field: "Id", name: "#", create: false, edit: true, list: true, type: "hidden", order: 0 },
        { field: "RestaurantId", name: "Restaurant", create: false, edit: false, list: true, type: "select", option: "/Category/GetRestaurantList", order: 3 },
        { field: "BranchId", name: "Branch", create: true, edit: true, list: true, type: "select", option: "/Category/GetBranchList", order: 3 },
        { field: "TypeId", name: "Group", create: true, edit: true, list: true, type: "select", option: "/Admin/GetGroupOption", order: 3 },
        //{ field: "UserName", name: "Tên đăng nhập", create: true, edit: true, list: true, edisabled: true },
        { field: "FullName", name: "Name", create: true, edit: true, list: true, order: 4 },
        { field: "UserName", name: "Username", create: true, edit: true, list: true, type: "username", order: 1, readonly: true },
        { field: "PasswordHash", name: "Password", create: true, edit: true, list: false, type: "password", order: 2 },
        { field: "Mobile", name: "Phone", create: true, edit: true, list: true, order: 5 },
        { field: "Email", name: "Email", create: true, edit: true, list: true, order: 6 },
        { field: "Address", name: "Address", create: true, edit: true, list: true, type: "textarea", order: 7 },
        { field: "Active", name: "Status", create: false, edit: false, list: true, type: "active" },
        { field: "CreatedDate", name: "Created Date", create: false, edit: false, list: true, type: "datetime" }
    ];

    return obj;
});
