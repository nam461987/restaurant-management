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
    public class Order_Channel_TimeService
    {
        public static Response<Order_Channel_Time_Config> GetAllOrder_Channel_Time(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Order_Channel_Time_Config> Response = new ResponseService<Order_Channel_Time_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Order_Channel_Time> Records_source = new List<Order_Channel_Time>();
            List<Order_Channel_Time_Config> Records = new List<Order_Channel_Time_Config>();
            string msg = string.Empty;
            string querystring = "";

            //This code to allow User just view the Users belong to their restaurant
            if (restaurantId > 0)
            {
                querystring += string.Format(" RestaurantId={0} And ", restaurantId);
            }
            if (restaurantId > 0 && branchId > 0)
            {
                querystring += string.Format(" RestaurantId={0} And BranchId ={1}", restaurantId, branchId);
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
                    Records_source = Order_Channel_Time.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Order_Channel_Time.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Order_Channel_Time, Order_Channel_Time_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Order_Channel_Time>, List<Order_Channel_Time_Config>>(Records_source);

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
        public static Response<Order_Channel_Time> InsertOrder_Channel_Time(Order_Channel_Time md)
        {

            Order_Channel_Time NewRecord = new Order_Channel_Time();
            List<Order_Channel_Time> Records = new List<Order_Channel_Time>();
            ResponseService<Order_Channel_Time> Response = new ResponseService<Order_Channel_Time>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.OrderChannelId = md.OrderChannelId;
                NewRecord.OpenTime = md.OpenTime;
                NewRecord.CloseTime = md.CloseTime;
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
        public static Response<Order_Channel_Time> GetOrder_Channel_TimeById(int id)
        {

            Order_Channel_Time Record = new Order_Channel_Time();
            List<Order_Channel_Time> Records = new List<Order_Channel_Time>();
            ResponseService<Order_Channel_Time> Response = new ResponseService<Order_Channel_Time>();

            try
            {
                Record = Order_Channel_Time.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Order_Channel_Time> UpdateOrder_Channel_Time(Order_Channel_Time md)
        {
            Order_Channel_Time Record = Order_Channel_Time.SingleOrDefault("where Id=@0", md.Id);
            List<Order_Channel_Time> Records = new List<Order_Channel_Time>();
            ResponseService<Order_Channel_Time> Response = new ResponseService<Order_Channel_Time>();

            try
            {
                Record.BranchId = md.BranchId;
                Record.OrderChannelId = md.OrderChannelId;
                Record.OpenTime = md.OpenTime;
                Record.CloseTime = md.CloseTime;
                Record.Save();
            }
            catch (Exception ex)
            {
                return Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Order_Channel_Time> DeleteOrder_Channel_Time(int id, int status)
        {
            Order_Channel_Time Record = Order_Channel_Time.SingleOrDefault("where Id=@0", id);
            List<Order_Channel_Time> Records = new List<Order_Channel_Time>();
            ResponseService<Order_Channel_Time> Response = new ResponseService<Order_Channel_Time>();

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
    }
}
