using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class AreaQuery : DatatablesQuery
    {
        public string Name { get; set; }

        public string X { get; set; }

        public string Y { get; set; }
    }
}