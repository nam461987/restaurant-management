using RestaurantNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Admin.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool au = false;
            string[] roles = this.Role.Split(',');
            List<string> privilegeLevels = new List<string>();
            int typeId = int.Parse(HttpContext.Current.Session["TypeId"].ToString());
            List<Admin_Group_Permission_View00> Per = new List<Admin_Group_Permission_View00>();

            //if Restaurant Admin login, just get all permissions of Restaurant Admin Group (All Restaurant Admins have same permissions)
            if (typeId == 2) {
                Per = Admin_Group_Permission_View00.Query("Where GroupId=@0 And" +
                " Status=1", typeId).ToList();
            }
            else
            {
                Per = Admin_Group_Permission_View00.Query("Where GroupId=@0 AND RestaurantId=@1 And" +
                " Status=1", typeId, Constants.RestaurantId).ToList();
            }

            if (Per.Any())
            {
                privilegeLevels = Per.Select(c => c.PermissionIdName.ToString()).ToList();
            }
            foreach (var r in roles)
            {
                if (privilegeLevels.Contains(r))
                {
                    au = true;
                }
            }
            if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) == 1)
            {
                au = true;
            }
            return au;
            //if (privilegeLevels.Contains(this.Role))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.AcceptTypes.Contains("application/json"))
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { Result = 0 }
                };
            }
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Page_Deny" } });
        }
    }
}