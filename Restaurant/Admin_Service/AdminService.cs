using Admin.ServiceModel;
using Admin.Services;
using RestaurantNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service
{
    public class AdminService
    {
        public static Response<Admin_Account> Create_Account(Admin_Account obj)
        {
            Admin_Account Acc = new Admin_Account();
            List<Admin_Account> AccList = new List<Admin_Account>();
            ResponseService<Admin_Account> Response = new ResponseService<Admin_Account>();

            try
            {
                if (!string.IsNullOrEmpty(obj.PasswordHash))
                {
                    Acc.PasswordHash = WebsiteExtension.EncryptPassword(obj.PasswordHash);
                }
                Acc.RestaurantId = obj.RestaurantId;
                Acc.BranchId = obj.BranchId;
                Acc.UserName = obj.UserName;
                Acc.FullName = obj.FullName;
                Acc.TypeId = obj.TypeId;
                Acc.Mobile = obj.Mobile;
                Acc.Email = obj.Email;
                Acc.Address = obj.Address;
                Acc.CreatedDate = DateTime.Now;
                Acc.Status = 1;
                Acc.Active = 1;
                Acc.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(null, ex);
            }

            AccList.Add(Acc);

            return Response.Result(AccList, null);
        }
        public static bool CheckExistUser(string account)
        {
            bool flag = false;

            List<Admin_Account> AccountList = Admin_Account.Query("Where UserName=@0 AND Status=1", account).ToList();

            if (AccountList != null && AccountList.Count > 0)
            {
                flag = true;
            }

            return flag;
        }
    }
}
