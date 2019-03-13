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
    [SessionAuthorize]
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Supplier()
        {
            return View();
        }
        // Supplier
        [HasCredential(Role = "supplier_list")]
        [HttpPost]
        public JsonResult GetAllSupplier(DataModel obj)
        {
            var result = SupplierService.GetAllSupplier(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "supplier_create")]
        [HttpPost]
        public JsonResult InsertSupplier(Supplier md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;

            var result = SupplierService.InsertSupplier(md);

            return Json(result);
        }
        [HasCredential(Role = "supplier_update")]
        [HttpPost]
        public JsonResult UpdateSupplier(Supplier md)
        {
            var result = SupplierService.UpdateSupplier(md);

            return Json(result);
        }
        [HasCredential(Role = "supplier_delete")]
        [HttpPost]
        public JsonResult DeleteSupplier(int id, int status)
        {
            var result = SupplierService.DeleteSupplier(id, status);

            return Json(result);
        }
        [HasCredential(Role = "supplier_list")]
        public JsonResult GetSupplierList()
        {
            var result = SupplierService.GetSupplierList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------
    }
}