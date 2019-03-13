using Admin.Service;
using Admin.ServiceModel;
using Admin.ServiceModel.common;
using AutoMapper;
using RestaurantNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Admin.ServiceModel.AjaxRequestData;

namespace Admin.Service
{
    public class Menu_CategoryService
    {
        public static Response<Menu_Category_Config> GetAllMenu_Category(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Menu_Category_Config> Response = new ResponseService<Menu_Category_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Menu_Category> Records_source = new List<Menu_Category>();
            List<Menu_Category_Config> Records = new List<Menu_Category_Config>();
            string msg = string.Empty;
            string querystring = "";

            //This code to allow User just view the Users belong to their restaurant
            if (restaurantId > 0 && branchId < 1)
            {
                querystring += string.Format(" RestaurantId={0} And ", restaurantId);
            }
            else if (restaurantId > 0 && branchId > 0)
            {
                querystring += string.Format(" RestaurantId={0} AND BranchId={1} And ", restaurantId, branchId);
            }
            //-----------------------------------------------------------------------

            try
            {
                if (obj._c != null)
                {
                    foreach (var k in obj._c)
                    {

                        switch (k.Key)
                        {

                            case "Name":
                                querystring += k.Value.ToString();
                                break;
                            default:
                                querystring += k.Key + "=" + k.Value.ToString();
                                break;
                        }
                        if (!k.Equals(obj._c.Last()))
                        {
                            querystring += " AND ";
                        }
                    }
                    Records_source = Menu_Category.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Menu_Category.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Menu_Category, Menu_Category_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Menu_Category>, List<Menu_Category_Config>>(Records_source);

                int pSize = obj._lm;
                totalRecords = Records.Count();
                if (totalRecords > 1)
                {
                    Records = Records.Skip(obj._os).Take(pSize).ToList();
                }
            }
            catch (Exception ex)
            {
                return Response.Result(null, ex);
            }

            return Response.Result(Records, null);
        }
        public static Response<Menu_Category> InsertMenu_Category(Menu_Category md)
        {

            Menu_Category NewRecord = new Menu_Category();
            List<Menu_Category> Records = new List<Menu_Category>();
            ResponseService<Menu_Category> Response = new ResponseService<Menu_Category>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.Name = md.Name;
                NewRecord.Avatar = md.Avatar;
                NewRecord.Description = md.Description;
                NewRecord.Status = 1;
                NewRecord.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(NewRecord);

            return Response.Result(Records, null);
        }
        public static Response<Menu_Category> GetMenu_CategoryById(int id)
        {

            Menu_Category Record = new Menu_Category();
            List<Menu_Category> Records = new List<Menu_Category>();
            ResponseService<Menu_Category> Response = new ResponseService<Menu_Category>();

            try
            {
                Record = Menu_Category.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Menu_Category> UpdateMenu_Category(Menu_Category md)
        {
            Menu_Category Record = Menu_Category.SingleOrDefault("where Id=@0", md.Id);
            List<Menu_Category> Records = new List<Menu_Category>();
            ResponseService<Menu_Category> Response = new ResponseService<Menu_Category>();

            try
            {
                Record.Name = md.Name;
                Record.Avatar = md.Avatar;
                Record.Description = md.Description;
                Record.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Menu_Category> DeleteMenu_Category(int id, int status)
        {
            Menu_Category Record = Menu_Category.SingleOrDefault("where Id=@0", id);
            List<Menu_Category> Records = new List<Menu_Category>();
            ResponseService<Menu_Category> Response = new ResponseService<Menu_Category>();

            try
            {
                Record.Status = status;
                Record.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Option> GetMenu_CategoryList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Menu Category--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllMenu_Category(obj, restaurantId, branchId);

            if (restaurantList.Records.Any())
            {
                options.AddRange(restaurantList.Records.Select(c => new Option
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return Response.Result(options, null);
        }
    }
}
