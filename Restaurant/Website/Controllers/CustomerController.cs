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
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Customer()
        {
            return View();
        }

        // Customer
        [HasCredential(Role = "customer_list")]
        [HttpPost]
        public JsonResult GetAllCustomer(DataModel obj)
        {
            var result = CustomerService.GetAllCustomer(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "customer_create")]
        [HttpPost]
        public JsonResult InsertCustomer(Customer md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;

            var result = CustomerService.InsertCustomer(md);

            return Json(result);
        }
        [HasCredential(Role = "customer_update")]
        [HttpPost]
        public JsonResult UpdateCustomer(Customer md)
        {
            var result = CustomerService.UpdateCustomer(md);

            return Json(result);
        }
        [HasCredential(Role = "customer_delete")]
        [HttpPost]
        public JsonResult DeleteCustomer(int id, int status)
        {
            var result = CustomerService.DeleteCustomer(id, status);

            return Json(result);
        }
        [HasCredential(Role = "customer_list")]
        public JsonResult GetCustomerList()
        {
            var result = CustomerService.GetCustomerList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------
    }
}