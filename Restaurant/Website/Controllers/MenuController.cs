using Admin.Common;
using Admin.Service;
using RestaurantNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Admin.ServiceModel.AjaxRequestData;

namespace Admin.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        // Menu
        [HasCredential(Role = "menu_definition_list")]
        [HttpPost]
        public JsonResult GetAllMenu_Definition(DataModel obj)
        {
            var result = Menu_DefinitionService.GetAllMenu_Definition(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "menu_definition_create")]
        [HttpPost]
        public JsonResult InsertMenu_Definition(Menu_Definition md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Menu_DefinitionService.InsertMenu_Definition(md);

            return Json(result);
        }
        [HasCredential(Role = "menu_definition_update")]
        [HttpPost]
        public JsonResult UpdateMenu_Definition(Menu_Definition md)
        {
            var result = Menu_DefinitionService.UpdateMenu_Definition(md);

            return Json(result);
        }
        [HasCredential(Role = "menu_definition_delete")]
        [HttpPost]
        public JsonResult DeleteMenu_Definition(int id, int status)
        {
            var result = Menu_DefinitionService.DeleteMenu_Definition(id, status);

            return Json(result);
        }
        //---------------------------------------------------------------------
    }
}