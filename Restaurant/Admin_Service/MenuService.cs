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
    public class MenuService
    {
        public static Response<Menu_Config> GetAllMenu(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Menu_Config> Response = new ResponseService<Menu_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Menu> Records_source = new List<Menu>();
            List<Menu_Config> Records = new List<Menu_Config>();
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
                    Records_source = Menu.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Menu.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Menu, Menu_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Menu>, List<Menu_Config>>(Records_source);

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
        public static Response<Menu> InsertMenu(Menu md)
        {

            Menu NewRecord = new Menu();
            List<Menu> Records = new List<Menu>();
            ResponseService<Menu> Response = new ResponseService<Menu>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.CategoryId = md.CategoryId;
                NewRecord.Name = md.Name;
                NewRecord.Image = md.Image;
                NewRecord.Price = md.Price;
                NewRecord.UnitId = md.UnitId;
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
        public static Response<Menu> GetMenuById(int id)
        {

            Menu Record = new Menu();
            List<Menu> Records = new List<Menu>();
            ResponseService<Menu> Response = new ResponseService<Menu>();

            try
            {
                Record = Menu.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Menu> UpdateMenu(Menu md)
        {
            Menu Record = Menu.SingleOrDefault("where Id=@0", md.Id);
            List<Menu> Records = new List<Menu>();
            ResponseService<Menu> Response = new ResponseService<Menu>();

            try
            {
                Record.BranchId = md.BranchId;
                Record.CategoryId = md.CategoryId;
                Record.Name = md.Name;
                Record.Image = md.Image;
                Record.Price = md.Price;
                Record.UnitId = md.UnitId;
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
        public static Response<Menu> DeleteMenu(int id, int status)
        {
            Menu Record = Menu.SingleOrDefault("where Id=@0", id);
            List<Menu> Records = new List<Menu>();
            ResponseService<Menu> Response = new ResponseService<Menu>();

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
        public static Response<Option> GetMenuList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Menu--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var dataList = GetAllMenu(obj, restaurantId, branchId);

            if (dataList.Records.Any())
            {
                options.AddRange(dataList.Records.Select(c => new Option
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return Response.Result(options, null);
        }
    }
}
