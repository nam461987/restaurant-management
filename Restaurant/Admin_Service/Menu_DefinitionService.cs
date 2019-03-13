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
    public class Menu_DefinitionService
    {
        public static Response<Menu_Definition_Config> GetAllMenu_Definition(DataModel obj, int restaurantId, int branchId)
        {
            string order = string.Empty;
            int totalRecords = int.MinValue;
            ResponseService<Menu_Definition_Config> Response = new ResponseService<Menu_Definition_Config>();

            if (obj._od != null)
            {
                order = " Order By " + obj._od.FirstOrDefault().Key + " " + obj._od.FirstOrDefault().Value;
            }

            List<Menu_Definition> Records_source = new List<Menu_Definition>();
            List<Menu_Definition_Config> Records = new List<Menu_Definition_Config>();
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
                    Records_source = Menu_Definition.Query("Where " + querystring + " And Status<2 " + order + "").ToList();
                }
                else
                {
                    Records_source = Menu_Definition.Query("Where " + querystring + " Status<2 " + order + "").ToList();
                }

                // Map du lieu sang Model khac
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Menu_Definition, Menu_Definition_Config>();
                });
                IMapper mapper = config.CreateMapper();
                Records = mapper.Map<List<Menu_Definition>, List<Menu_Definition_Config>>(Records_source);

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
        public static Response<Menu_Definition> InsertMenu_Definition(Menu_Definition md)
        {

            Menu_Definition NewRecord = new Menu_Definition();
            List<Menu_Definition> Records = new List<Menu_Definition>();
            ResponseService<Menu_Definition> Response = new ResponseService<Menu_Definition>();

            try
            {
                NewRecord.RestaurantId = md.RestaurantId;
                NewRecord.BranchId = md.BranchId;
                NewRecord.MenuId = md.MenuId;
                NewRecord.IngredientId = md.IngredientId;
                NewRecord.Quantity = md.Quantity;
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
        public static Response<Menu_Definition> GetMenu_DefinitionById(int id)
        {

            Menu_Definition Record = new Menu_Definition();
            List<Menu_Definition> Records = new List<Menu_Definition>();
            ResponseService<Menu_Definition> Response = new ResponseService<Menu_Definition>();

            try
            {
                Record = Menu_Definition.SingleOrDefault("Where Id=@0 and Status<2", id);
            }
            catch (Exception ex)
            {
                Response.Result(Records, ex);
            }

            Records.Add(Record);

            return Response.Result(Records, null);
        }
        public static Response<Menu_Definition> UpdateMenu_Definition(Menu_Definition md)
        {
            Menu_Definition Record = Menu_Definition.SingleOrDefault("where Id=@0", md.Id);
            List<Menu_Definition> Records = new List<Menu_Definition>();
            ResponseService<Menu_Definition> Response = new ResponseService<Menu_Definition>();

            try
            {
                Record.MenuId = md.MenuId;
                Record.IngredientId = md.IngredientId;
                Record.Quantity = md.Quantity;
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
        public static Response<Menu_Definition> DeleteMenu_Definition(int id, int status)
        {
            Menu_Definition Record = Menu_Definition.SingleOrDefault("where Id=@0", id);
            List<Menu_Definition> Records = new List<Menu_Definition>();
            ResponseService<Menu_Definition> Response = new ResponseService<Menu_Definition>();

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
