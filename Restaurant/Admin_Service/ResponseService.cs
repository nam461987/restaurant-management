using Admin.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service
{
    public class ResponseService<T>
    {
        public Response<T> Result(List<T> obj, Exception ex)
        {
            Response<T> Response = new Response<T>();

            if (obj != null)
            {
                Response.Result = 1;
                Response.Records = obj;
                Response.Total = obj.Count;
            }
            else
            {
                Response.Result = 0;
                Response.Error = ex;
            }

            return Response;
        }
    }
}
