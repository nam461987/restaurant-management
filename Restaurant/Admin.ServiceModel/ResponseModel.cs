using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ServiceModel
{
    public class Response<T>
    {
        public int Result { get; set; }
        public List<T> Records { get; set; }
        public int Total { get; set; }
        public Exception Error { get; set; }
    }
}
