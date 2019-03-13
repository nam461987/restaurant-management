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
    public class Restaurant_TableService
    {
        public static Response<Restaurant_Table_Config> GetAllRestaurant_Table(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Restaurant_Table_Config> Response = new ResponseService<Restaurant_Table_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Restaurant_Table> Records_source = new List<Restaurant_Table>();
            List<Restaurant_Table_Config> Records = new List<Restaurant_Table_Config>();
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
                    Records_source = Restaurant_Table.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Restaurant_Table.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Restaurant_Table, Restaurant_Table_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Restaurant_Table>, List<Restaurant_Table_Config>>(Records_source);

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
        public static Response<Restaurant_Table> InsertRestaurant_Table(Restaurant_Table md)
        {

            Restaurant_Table NewRecord = new Restaurant_Table();
            List<Restaurant_Table> Records = new List<Restaurant_Table>();
            ResponseService<Restaurant_Table> Response = new ResponseService<Restaurant_Table>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.Name = md.Name;
                NewRecord.Location = md.Location;
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
        public static Response<Restaurant_Table> GetRestaurant_TableById(int id)
        {

            Restaurant_Table Record = new Restaurant_Table();
            List<Restaurant_Table> Records = new List<Restaurant_Table>();
            ResponseService<Restaurant_Table> Response = new ResponseService<Restaurant_Table>();

            try
            {
                Record = Restaurant_Table.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Restaurant_Table> UpdateRestaurant_Table(Restaurant_Table md)
        {
            Restaurant_Table Record = Restaurant_Table.SingleOrDefault("where Id=@0", md.Id);
            List<Restaurant_Table> Records = new List<Restaurant_Table>();
            ResponseService<Restaurant_Table> Response = new ResponseService<Restaurant_Table>();

            try
            {
                Record.Name = md.Name;
                Record.Location = md.Location;
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
        public static Response<Restaurant_Table> DeleteRestaurant_Table(int id, int status)
        {
            Restaurant_Table Record = Restaurant_Table.SingleOrDefault("where Id=@0", id);
            List<Restaurant_Table> Records = new List<Restaurant_Table>();
            ResponseService<Restaurant_Table> Response = new ResponseService<Restaurant_Table>();

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
        public static Response<Option> GetRestaurant_TableList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Restaurant Table--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllRestaurant_Table(obj, restaurantId, branchId);

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
