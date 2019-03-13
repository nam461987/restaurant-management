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
    public class Customer_TypeService
    {
        public static Response<Customer_Type_Config> GetAllCustomer_Type(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Customer_Type_Config> Response = new ResponseService<Customer_Type_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Customer_Type> Records_source = new List<Customer_Type>();
            List<Customer_Type_Config> Records = new List<Customer_Type_Config>();
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
                    Records_source = Customer_Type.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Customer_Type.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Customer_Type, Customer_Type_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Customer_Type>, List<Customer_Type_Config>>(Records_source);

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
        public static Response<Customer_Type> InsertCustomer_Type(Customer_Type md)
        {

            Customer_Type NewRecord = new Customer_Type();
            List<Customer_Type> Records = new List<Customer_Type>();
            ResponseService<Customer_Type> Response = new ResponseService<Customer_Type>();

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
        public static Response<Customer_Type> GetCustomer_TypeById(int id)
        {

            Customer_Type Record = new Customer_Type();
            List<Customer_Type> Records = new List<Customer_Type>();
            ResponseService<Customer_Type> Response = new ResponseService<Customer_Type>();

            try
            {
                Record = Customer_Type.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Customer_Type> UpdateCustomer_Type(Customer_Type md)
        {
            Customer_Type Record = Customer_Type.SingleOrDefault("where Id=@0", md.Id);
            List<Customer_Type> Records = new List<Customer_Type>();
            ResponseService<Customer_Type> Response = new ResponseService<Customer_Type>();

            try
            {
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
        public static Response<Customer_Type> DeleteCustomer_Type(int id, int status)
        {
            Customer_Type Record = Customer_Type.SingleOrDefault("where Id=@0", id);
            List<Customer_Type> Records = new List<Customer_Type>();
            ResponseService<Customer_Type> Response = new ResponseService<Customer_Type>();

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
        public static Response<Option> GetCustomer_TypeList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Customer Type--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var restaurantList = GetAllCustomer_Type(obj, restaurantId, branchId);

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
