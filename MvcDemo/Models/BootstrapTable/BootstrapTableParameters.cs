using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.Models.BootstrapTable
{
    public class BootstrapTableParameters
    {
        public string Sort { get; set; }
        public string Order { get; set; }

        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}