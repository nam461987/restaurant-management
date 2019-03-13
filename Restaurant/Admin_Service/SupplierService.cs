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
    public class SupplierService
    {
        public static Response<Supplier_Config> GetAllSupplier(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Supplier_Config> Response = new ResponseService<Supplier_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Supplier> Records_source = new List<Supplier>();
            List<Supplier_Config> Records = new List<Supplier_Config>();
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
                    Records_source = Supplier.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Supplier.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Supplier, Supplier_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Supplier>, List<Supplier_Config>>(Records_source);

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
        public static Response<Supplier> InsertSupplier(Supplier md)
        {

            Supplier NewRecord = new Supplier();
            List<Supplier> Records = new List<Supplier>();
            ResponseService<Supplier> Response = new ResponseService<Supplier>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.Name = md.Name;
                NewRecord.Address = md.Address;
                NewRecord.Contact = md.Contact;
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
        public static Response<Supplier> GetSupplierById(int id)
        {

            Supplier Record = new Supplier();
            List<Supplier> Records = new List<Supplier>();
            ResponseService<Supplier> Response = new ResponseService<Supplier>();

            try
            {
                Record = Supplier.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Supplier> UpdateSupplier(Supplier md)
        {
            Supplier Record = Supplier.SingleOrDefault("where Id=@0", md.Id);
            List<Supplier> Records = new List<Supplier>();
            ResponseService<Supplier> Response = new ResponseService<Supplier>();

            try
            {
                Record.Name = md.Name;
                Record.Address = md.Address;
                Record.Contact = md.Contact;
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
        public static Response<Supplier> DeleteSupplier(int id, int status)
        {
            Supplier Record = Supplier.SingleOrDefault("where Id=@0", id);
            List<Supplier> Records = new List<Supplier>();
            ResponseService<Supplier> Response = new ResponseService<Supplier>();

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
        public static Response<Option> GetSupplierList(int restaurantId, int branchId)
        {

            ResponseService<Option> Response = new ResponseService<Option>();

            var options = new List<Option>
                {
                    new Option
                    {
                        DisplayText = "--Select Supplier--",
                        Value = 0
                    }
                };
            var obj = new DataModel();
            var supplierList = GetAllSupplier(obj, restaurantId, branchId);

            if (supplierList.Records.Any())
            {
                options.AddRange(supplierList.Records.Select(c => new Option
                {
                    DisplayText = Convert.ToString(c.Name),
                    Value = Convert.ToInt32(c.Id)
                }).ToList());
            }

            return Response.Result(options, null);
        }
    }
}
