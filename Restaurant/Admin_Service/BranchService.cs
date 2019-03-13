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
    public class BranchService
    {
        public static Response<Restaurant_Branch_Config> GetAllRestaurant_Branch(DataModel obj, int restaurantId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Restaurant_Branch_Config> Response = new ResponseService<Restaurant_Branch_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Restaurant_Branch> Records_source = new List<Restaurant_Branch>();
            List<Restaurant_Branch_Config> Records = new List<Restaurant_Branch_Config>();
            string msg = string.Empty;
            string querystring = "";

            //This code to allow User just view the Things belong to their restaurant
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
                    Records_source = Restaurant_Branch.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Restaurant_Branch.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Restaurant_Branch, Restaurant_Branch_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Restaurant_Branch>, List<Restaurant_Branch_Config>>(Records_source);

                if (obj._lm > 0)
                {
                    int pSize = obj._lm;
                    totalRecords = Records.Count();
                    if (totalRecords > 1)
                    {
                        Records = Records.Skip(obj._os).Take(pSize).ToList();
                    }
                }
                
            }
            catch (Exception ex)
            {
                return Response.Result(null, ex);
            }

            return Response.Result(Records, null);
        }
        public static Response<Restaurant_Branch> InsertRestaurant_Branch(Restaurant_Branch md)
        {

            Restaurant_Branch NewRecord = new Restaurant_Branch();
            List<Restaurant_Branch> Records = new List<Restaurant_Branch>();
            ResponseService<Restaurant_Branch> Response = new ResponseService<Restaurant_Branch>();

            try
            {
                NewRecord.Name = md.Name;
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.Address = md.Address;
                NewRecord.Phone = md.Phone;
                NewRecord.OpenTime = md.OpenTime;
                NewRecord.CloseTime = md.CloseTime;
                NewRecord.AllDay = md.AllDay;
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
        public static Response<Restaurant_Branch> UpdateRestaurant_Branch(Restaurant_Branch md)
        {
            Restaurant_Branch Record = Restaurant_Branch.SingleOrDefault("where Id=@0 And RestaurantId=@1", md.Id);
            List<Restaurant_Branch> Records = new List<Restaurant_Branch>();
            ResponseService<Restaurant_Branch> Response = new ResponseService<Restaurant_Branch>();

            try
            {
                Record.Name = md.Name;
                Record.Address = md.Address;
                Record.Phone = md.Phone;
                Record.OpenTime = md.OpenTime;
                Record.CloseTime = md.CloseTime;
                Record.AllDay = md.AllDay;
                Record.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Restaurant_Branch> DeleteRestaurant_Branch(int id, int status)
        {
            Restaurant_Branch Record = Restaurant_Branch.SingleOrDefault("where Id=@0", id);
            List<Restaurant_Branch> Records = new List<Restaurant_Branch>();
            ResponseService<Restaurant_Branch> Response = new ResponseService<Restaurant_Branch>();

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
        public static Response<Option> GetRestaurant_BranchList(int restaurantId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Branch--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllRestaurant_Branch(obj, restaurantId);

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
