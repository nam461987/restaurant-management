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
    public class Order_ChannelService
    {
        public static Response<Order_Channel_Config> GetAllOrder_Channel(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Order_Channel_Config> Response = new ResponseService<Order_Channel_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Order_Channel> Records_source = new List<Order_Channel>();
            List<Order_Channel_Config> Records = new List<Order_Channel_Config>();
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
                    Records_source = Order_Channel.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Order_Channel.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Order_Channel, Order_Channel_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Order_Channel>, List<Order_Channel_Config>>(Records_source);

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
        public static Response<Order_Channel> InsertOrder_Channel(Order_Channel md)
        {

            Order_Channel NewRecord = new Order_Channel();
            List<Order_Channel> Records = new List<Order_Channel>();
            ResponseService<Order_Channel> Response = new ResponseService<Order_Channel>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.Name = md.Name;
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
        public static Response<Order_Channel> GetOrder_ChannelById(int id)
        {

            Order_Channel Record = new Order_Channel();
            List<Order_Channel> Records = new List<Order_Channel>();
            ResponseService<Order_Channel> Response = new ResponseService<Order_Channel>();

            try
            {
                Record = Order_Channel.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Order_Channel> UpdateOrder_Channel(Order_Channel md)
        {
            Order_Channel Record = Order_Channel.SingleOrDefault("where Id=@0", md.Id);
            List<Order_Channel> Records = new List<Order_Channel>();
            ResponseService<Order_Channel> Response = new ResponseService<Order_Channel>();

            try
            {
                Record.BranchId = md.BranchId;
                Record.Name = md.Name;
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
        public static Response<Order_Channel> DeleteOrder_Channel(int id, int status)
        {
            Order_Channel Record = Order_Channel.SingleOrDefault("where Id=@0", id);
            List<Order_Channel> Records = new List<Order_Channel>();
            ResponseService<Order_Channel> Response = new ResponseService<Order_Channel>();

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
        public static Response<Option> GetOrder_ChannelList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Order Channel--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllOrder_Channel(obj, restaurantId, branchId);

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
