using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Admin.Common
{
    public class Constants
    {
        public static int RestaurantId
        {
            get
            {
                return int.Parse(HttpContext.Current.Application["CompanyId"].ToString());
            }
        }
        public static int BranchId
        {
            get
            {
                return int.Parse(HttpContext.Current.Application["BranchId"].ToString());
            }
        }
    }
    public class Strings
    {
        public static string UploadFolderRoot = ConfigurationManager.AppSettings["UploadFolderRoot"];
        public static string ForwardUploadFolderRoot = ConfigurationManager.AppSettings["ForwardUploadFolderRoot"];
    }
}