﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ServiceModel
{
    public class Menu_Unit_Config
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
    }
}
