using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcDemo.Models.DataTables;

namespace MvcDemo.Models
{
    public class AreaQuery : DataTablesParameters
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
}