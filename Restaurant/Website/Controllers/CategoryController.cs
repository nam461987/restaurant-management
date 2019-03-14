using Admin.Common;
using Admin.Service;
using Admin.ServiceModel;
using Admin.ServiceModel.common;
using Admin.Services;
using AutoMapper;
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
    public class CategoryController : Controller
    {
        // GET: Category
        [HasCredential(Role = "category_restaurant_list")]
        public ActionResult Restaurant()
        {
            return View();
        }
        [HasCredential(Role = "category_restaurant_branch_list")]
        public ActionResult Branch()
        {
            return View();
        }
        [HasCredential(Role = "category_menu_category_list")]
        public ActionResult Menu_Category()
        {
            return View();
        }
        [HasCredential(Role = "category_menu_list")]
        public ActionResult Menu()
        {
            return View();
        }
        [HasCredential(Role = "category_menu_unit_list")]
        public ActionResult Menu_Unit()
        {
            return View();
        }
        [HasCredential(Role = "category_customer_type_list")]
        public ActionResult Customer_Type()
        {
            return View();
        }
        [HasCredential(Role = "category_restaurant_table_list")]
        public ActionResult Restaurant_Table()
        {
            return View();
        }
        [HasCredential(Role = "category_ingredient_list")]
        public ActionResult Ingredient()
        {
            return View();
        }
        [HasCredential(Role = "category_order_channel_list")]
        public ActionResult Order_Channel()
        {
            return View();
        }
        [HasCredential(Role = "category_order_channel_time_list")]
        public ActionResult Order_Channel_Time()
        {
            return View();
        }

        // Restaurant
        [HasCredential(Role = "category_restaurant_list")]
        [HttpPost]
        public JsonResult GetAllRestaurant(DataModel obj)
        {
            var result = RestaurantService.GetAllRestaurant(obj);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_create")]
        [HttpPost]
        public JsonResult InsertRestaurant(Restaurant md)
        {
            var result = RestaurantService.InsertRestaurant(md);

            //Add Admin account after success adding restaurant
            if (result.Result == 1 && result.Records.Count == 1)
            {
                Admin_Account obj = new Admin_Account();
                obj.UserName = WebsiteExtension.Slug(result.Records[0].Name);
                obj.RestaurantId = result.Records[0].Id;
                obj.PasswordHash = obj.UserName + "123";

                AdminService.Create_Account(obj);
            }
            else
            {
                return Json(null);
            }

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_update")]
        [HttpPost]
        public JsonResult UpdateRestaurant(Restaurant md)
        {
            var result = RestaurantService.UpdateRestaurant(md);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_delete")]
        [HttpPost]
        public JsonResult DeleteRestaurant(int id, int status)
        {
            var result = RestaurantService.DeleteRestaurant(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_list")]
        public JsonResult GetRestaurantList()
        {
            var result = RestaurantService.GetRestaurantList();

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Branch
        [HasCredential(Role = "category_restaurant_branch_list")]
        [HttpPost]
        public JsonResult GetAllRestaurant_Branch(DataModel obj)
        {
            var result = BranchService.GetAllRestaurant_Branch(obj, Constants.RestaurantId);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_branch_create")]
        [HttpPost]
        public JsonResult InsertRestaurant_Branch(Restaurant_Branch md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            var result = BranchService.InsertRestaurant_Branch(md);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_branch_update")]
        [HttpPost]
        public JsonResult UpdateRestaurant_Branch(Restaurant_Branch md)
        {
            var result = BranchService.UpdateRestaurant_Branch(md);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_branch_delete")]
        [HttpPost]
        public JsonResult DeleteRestaurant_Branch(int id, int status)
        {
            var result = BranchService.DeleteRestaurant_Branch(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_branch_list")]
        [HttpPost]
        public JsonResult GetBranchList()
        {
            var result = BranchService.GetRestaurant_BranchList(Constants.RestaurantId);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_branch_list")]
        [HttpPost]
        public JsonResult GetBranchListByRestaurantId(int id)
        {
            var result = BranchService.GetRestaurant_BranchList(id);

            return Json(result);
        }
        //-------------------------------------------------------------

        // Menu_Category
        [HasCredential(Role = "category_menu_category_list")]
        [HttpPost]
        public JsonResult GetAllMenu_Category(DataModel obj)
        {
            var result = Menu_CategoryService.GetAllMenu_Category(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_category_create")]
        [HttpPost]
        public JsonResult InsertMenu_Category(Menu_Category md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Menu_CategoryService.InsertMenu_Category(md);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_category_update")]
        [HttpPost]
        public JsonResult UpdateMenu_Category(Menu_Category md)
        {
            var result = Menu_CategoryService.UpdateMenu_Category(md);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_category_delete")]
        [HttpPost]
        public JsonResult DeleteMenu_Category(int id, int status)
        {
            var result = Menu_CategoryService.DeleteMenu_Category(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_category_list")]
        public JsonResult GetMenu_CategoryList()
        {
            var result = Menu_CategoryService.GetMenu_CategoryList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Menu_Unit
        [HasCredential(Role = "category_menu_unit_list")]
        [HttpPost]
        public JsonResult GetAllMenu_Unit(DataModel obj)
        {
            var result = Menu_UnitService.GetAllMenu_Unit(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_unit_create")]
        [HttpPost]
        public JsonResult InsertMenu_Unit(Menu_Unit md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Menu_UnitService.InsertMenu_Unit(md);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_unit_update")]
        [HttpPost]
        public JsonResult UpdateMenu_Unit(Menu_Unit md)
        {
            var result = Menu_UnitService.UpdateMenu_Unit(md);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_unit_delete")]
        [HttpPost]
        public JsonResult DeleteMenu_Unit(int id, int status)
        {
            var result = Menu_UnitService.DeleteMenu_Unit(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_unit_list")]
        public JsonResult GetMenu_UnitList()
        {
            var result = Menu_UnitService.GetMenu_UnitList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Customer_Type
        [HasCredential(Role = "category_customer_type_list")]
        [HttpPost]
        public JsonResult GetAllCustomer_Type(DataModel obj)
        {
            var result = Customer_TypeService.GetAllCustomer_Type(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_customer_type_create")]
        [HttpPost]
        public JsonResult InsertCustomer_Type(Customer_Type md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Customer_TypeService.InsertCustomer_Type(md);

            return Json(result);
        }
        [HasCredential(Role = "category_customer_type_update")]
        [HttpPost]
        public JsonResult UpdateCustomer_Type(Customer_Type md)
        {
            var result = Customer_TypeService.UpdateCustomer_Type(md);

            return Json(result);
        }
        [HasCredential(Role = "category_customer_type_delete")]
        [HttpPost]
        public JsonResult DeleteCustomer_Type(int id, int status)
        {
            var result = Customer_TypeService.DeleteCustomer_Type(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_customer_type_list")]
        public JsonResult GetCustomer_TypeList()
        {
            var result = Customer_TypeService.GetCustomer_TypeList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Restaurant_Table
        [HasCredential(Role = "category_restaurant_table_list")]
        [HttpPost]
        public JsonResult GetAllRestaurant_Table(DataModel obj)
        {
            var result = Restaurant_TableService.GetAllRestaurant_Table(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_table_create")]
        [HttpPost]
        public JsonResult InsertRestaurant_Table(Restaurant_Table md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Restaurant_TableService.InsertRestaurant_Table(md);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_table_update")]
        [HttpPost]
        public JsonResult UpdateRestaurant_Table(Restaurant_Table md)
        {
            var result = Restaurant_TableService.UpdateRestaurant_Table(md);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_table_delete")]
        [HttpPost]
        public JsonResult DeleteRestaurant_Table(int id, int status)
        {
            var result = Restaurant_TableService.DeleteRestaurant_Table(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_restaurant_table_list")]
        public JsonResult GetRestaurant_TableList()
        {
            var result = Restaurant_TableService.GetRestaurant_TableList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Menu
        [HasCredential(Role = "category_menu_list")]
        [HttpPost]
        public JsonResult GetAllMenu(DataModel obj)
        {
            var result = MenuService.GetAllMenu(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_create")]
        [HttpPost]
        public JsonResult InsertMenu(Menu md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = MenuService.InsertMenu(md);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_update")]
        [HttpPost]
        public JsonResult UpdateMenu(Menu md)
        {
            var result = MenuService.UpdateMenu(md);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_delete")]
        [HttpPost]
        public JsonResult DeleteMenu(int id, int status)
        {
            var result = MenuService.DeleteMenu(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_menu_list")]
        public JsonResult GetMenuList()
        {
            var result = MenuService.GetMenuList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Ingredient
        [HasCredential(Role = "category_ingredient_list")]
        [HttpPost]
        public JsonResult GetAllIngredient(DataModel obj)
        {
            var result = IngredientService.GetAllIngredient(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_ingredient_create")]
        [HttpPost]
        public JsonResult InsertIngredient(Ingredient md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = IngredientService.InsertIngredient(md);

            return Json(result);
        }
        [HasCredential(Role = "category_ingredient_update")]
        [HttpPost]
        public JsonResult UpdateIngredient(Ingredient md)
        {
            var result = IngredientService.UpdateIngredient(md);

            return Json(result);
        }
        [HasCredential(Role = "category_ingredient_delete")]
        [HttpPost]
        public JsonResult DeleteIngredient(int id, int status)
        {
            var result = IngredientService.DeleteIngredient(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_ingredient_list")]
        public JsonResult GetIngredientList()
        {
            var result = IngredientService.GetIngredientList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Order_Channel
        [HasCredential(Role = "category_order_channel_list")]
        [HttpPost]
        public JsonResult GetAllOrder_Channel(DataModel obj)
        {
            var result = Order_ChannelService.GetAllOrder_Channel(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_create")]
        [HttpPost]
        public JsonResult InsertOrder_Channel(Order_Channel md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Order_ChannelService.InsertOrder_Channel(md);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_update")]
        [HttpPost]
        public JsonResult UpdateOrder_Channel(Order_Channel md)
        {
            var result = Order_ChannelService.UpdateOrder_Channel(md);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_delete")]
        [HttpPost]
        public JsonResult DeleteOrder_Channel(int id, int status)
        {
            var result = Order_ChannelService.DeleteOrder_Channel(id, status);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_list")]
        public JsonResult GetOrder_ChannelList()
        {
            var result = Order_ChannelService.GetOrder_ChannelList(Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        //---------------------------------------------------------------------

        // Order_Channel_Time
        [HasCredential(Role = "category_order_channel_time_list")]
        [HttpPost]
        public JsonResult GetAllOrder_Channel_Time(DataModel obj)
        {
            var result = Order_Channel_TimeService.GetAllOrder_Channel_Time(obj, Constants.RestaurantId, Constants.BranchId);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_time_create")]
        [HttpPost]
        public JsonResult InsertOrder_Channel_Time(Order_Channel_Time md)
        {
            md.RestaurantId = md.RestaurantId != 0 ? md.RestaurantId : Constants.RestaurantId;
            md.BranchId = md.BranchId != 0 ? md.BranchId : Constants.BranchId;
            var result = Order_Channel_TimeService.InsertOrder_Channel_Time(md);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_time_update")]
        [HttpPost]
        public JsonResult UpdateOrder_Channel_Time(Order_Channel_Time md)
        {
            var result = Order_Channel_TimeService.UpdateOrder_Channel_Time(md);

            return Json(result);
        }
        [HasCredential(Role = "category_order_channel_time_delete")]
        [HttpPost]
        public JsonResult DeleteOrder_Channel_Time(int id, int status)
        {
            var result = Order_Channel_TimeService.DeleteOrder_Channel_Time(id, status);

            return Json(result);
        }
        //---------------------------------------------------------------------
    }
}