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
    public class Menu_UnitService
    {
        public static Response<Menu_Unit_Config> GetAllMenu_Unit(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Menu_Unit_Config> Response = new ResponseService<Menu_Unit_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Menu_Unit> Records_source = new List<Menu_Unit>();
            List<Menu_Unit_Config> Records = new List<Menu_Unit_Config>();
            string msg = string.Empty;
            string querystring = "";

            //This code to allow User just view the Users belong to their restaurant
            if (restaurantId > 0)
            {
                querystring += string.Format(" RestaurantId={0} And ", restaurantId);
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
                    Records_source = Menu_Unit.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Menu_Unit.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Menu_Unit, Menu_Unit_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Menu_Unit>, List<Menu_Unit_Config>>(Records_source);

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
        public static Response<Menu_Unit> InsertMenu_Unit(Menu_Unit md)
        {

            Menu_Unit NewRecord = new Menu_Unit();
            List<Menu_Unit> Records = new List<Menu_Unit>();
            ResponseService<Menu_Unit> Response = new ResponseService<Menu_Unit>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.Name = md.Name;
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
        public static Response<Menu_Unit> GetMenu_UnitById(int id)
        {

            Menu_Unit Record = new Menu_Unit();
            List<Menu_Unit> Records = new List<Menu_Unit>();
            ResponseService<Menu_Unit> Response = new ResponseService<Menu_Unit>();

            try
            {
                Record = Menu_Unit.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Menu_Unit> UpdateMenu_Unit(Menu_Unit md)
        {
            Menu_Unit Record = Menu_Unit.SingleOrDefault("where Id=@0", md.Id);
            List<Menu_Unit> Records = new List<Menu_Unit>();
            ResponseService<Menu_Unit> Response = new ResponseService<Menu_Unit>();

            try
            {
                Record.Name = md.Name;
                Record.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Menu_Unit> DeleteMenu_Unit(int id, int status)
        {
            Menu_Unit Record = Menu_Unit.SingleOrDefault("where Id=@0", id);
            List<Menu_Unit> Records = new List<Menu_Unit>();
            ResponseService<Menu_Unit> Response = new ResponseService<Menu_Unit>();

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
        public static Response<Option> GetMenu_UnitList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Menu Unit--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllMenu_Unit(obj, restaurantId, branchId);

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
