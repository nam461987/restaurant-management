using Admin.Service;
using Admin.Service.global;
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
    public class RestaurantService
    {
        public static Response<Restaurant_Config> GetAllRestaurant(DataModel obj)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Restaurant_Config> Response = new ResponseService<Restaurant_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Restaurant> Records_source = new List<Restaurant>();
            List<Restaurant_Config> Records = new List<Restaurant_Config>();
            string msg = string.Empty;
            string querystring = "";

            //This code to allow User just view the Data belong to their restaurant
            if (Constants.RestaurantId > 0 && Constants.BranchId < 1)
            {
                querystring += string.Format(" RestaurantId={0} AND ", Constants.RestaurantId);
            }
            else if (Constants.RestaurantId > 0 && Constants.BranchId > 0)
            {
                querystring += string.Format(" RestaurantId={0} AND BranchId={1} AND ", Constants.RestaurantId, Constants.BranchId);
            }
            //If current user is Admin, show all Admin of each restaurant
            else if (Constants.RestaurantId == 0 && Constants.BranchId == 0)
            {
                querystring += string.Format(" BranchId=0 AND ");
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
                    Records_source = Restaurant.Query("Where Status<2 AND " + querystring + order + "").ToList();
                }
                else
                {
                    Records_source = Restaurant.Query("Where Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Restaurant, Restaurant_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Restaurant>, List<Restaurant_Config>>(Records_source);

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
        public static Response<Restaurant> InsertRestaurant(Restaurant md)
        {

            Restaurant NewRecord = new Restaurant();
            List<Restaurant> Records = new List<Restaurant>();
            ResponseService<Restaurant> Response = new ResponseService<Restaurant>();

            try
            {
                NewRecord.Name = md.Name;
                NewRecord.Email = md.Email;
                NewRecord.Address = md.Address;
                NewRecord.Phone = md.Phone;
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
        public static Response<Restaurant> GetRestaurantById(int id)
        {

            Restaurant Record = new Restaurant();
            List<Restaurant> Records = new List<Restaurant>();
            ResponseService<Restaurant> Response = new ResponseService<Restaurant>();

            try
            {
                Record = Restaurant.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Restaurant> UpdateRestaurant(Restaurant md)
        {
            Restaurant Record = Restaurant.SingleOrDefault("where Id=@0", md.Id);
            List<Restaurant> Records = new List<Restaurant>();
            ResponseService<Restaurant> Response = new ResponseService<Restaurant>();

            try
            {
                Record.Name = md.Name;
                Record.Description = md.Description;
                Record.Email = md.Email;
                Record.Phone = md.Phone;
                Record.Address = md.Address;
                Record.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Restaurant> DeleteRestaurant(int id, int status)
        {
            Restaurant Record = Restaurant.SingleOrDefault("where Id=@0", id);
            List<Restaurant> Records = new List<Restaurant>();
            ResponseService<Restaurant> Response = new ResponseService<Restaurant>();

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
        public static Response<Option> GetRestaurantList()
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Restaurant--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllRestaurant(obj);

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
