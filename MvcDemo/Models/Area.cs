using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double PointX { get; set; }

        public double PointY { get; set; }

        public IQueryable<Area> GetData()
        {
            var data = new List<Area>();
            for (var i = 1; i <= 30; i++)
            {
                data.Add(new Area() { Id = i, Name = "村" + i, Description = "这里是村" + i, PointX = i + 119.082458, PointY = i + 36.925349 });
            }
            return data.AsQueryable();
        }
    }

}