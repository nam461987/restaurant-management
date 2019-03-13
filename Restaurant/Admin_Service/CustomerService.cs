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
    public class CustomerService
    {
        public static Response<Customer_Config> GetAllCustomer(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Customer_Config> Response = new ResponseService<Customer_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Customer> Records_source = new List<Customer>();
            List<Customer_Config> Records = new List<Customer_Config>();
            string msg = string.Empty;
            string querystring = "";

            //This code to allow User just view the Users belong to their restaurant
            if (restaurantId > 0 && branchId < 1)
            {
                querystring += string.Format(" RestaurantId={0} And ", restaurantId);
            }
            //else if (restaurantId > 0 && branchId > 0)
            //{
            //    querystring += string.Format(" RestaurantId={0} AND BranchId={1} And ", restaurantId, branchId);
            //}
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
                    Records_source = Customer.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Customer.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Customer, Customer_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Customer>, List<Customer_Config>>(Records_source);

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
        public static Response<Customer> InsertCustomer(Customer md)
        {

            Customer NewRecord = new Customer();
            List<Customer> Records = new List<Customer>();
            ResponseService<Customer> Response = new ResponseService<Customer>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.TypeId = md.TypeId;
                NewRecord.Name = md.Name;
                NewRecord.Phone = md.Phone;
                NewRecord.Email = md.Email;
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
        public static Response<Customer> GetCustomerById(int id)
        {

            Customer Record = new Customer();
            List<Customer> Records = new List<Customer>();
            ResponseService<Customer> Response = new ResponseService<Customer>();

            try
            {
                Record = Customer.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Customer> UpdateCustomer(Customer md)
        {
            Customer Record = Customer.SingleOrDefault("where Id=@0", md.Id);
            List<Customer> Records = new List<Customer>();
            ResponseService<Customer> Response = new ResponseService<Customer>();

            try
            {
                Record.TypeId = md.TypeId;
                Record.Name = md.Name;
                Record.Phone = md.Phone;
                Record.Email = md.Email;
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
        public static Response<Customer> DeleteCustomer(int id, int status)
        {
            Customer Record = Customer.SingleOrDefault("where Id=@0", id);
            List<Customer> Records = new List<Customer>();
            ResponseService<Customer> Response = new ResponseService<Customer>();

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
        public static Response<Option> GetCustomerList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Customer--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var dataList = GetAllCustomer(obj, restaurantId, branchId);

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
