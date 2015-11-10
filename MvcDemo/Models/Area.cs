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

        public List<Area> GetData()
        {
            var data = new List<Area>()
            {
                new Area{Id=1,Name="北里村",Description="呵呵呵北里村<br>呵呵呵北里村<br>呵呵呵北里村<br><a href='http://fzdy.sinawf.com/swgk/areaindex/%E5%8C%97%E9%87%8C%E6%9D%91.html' target='_blank'>查看详情</a>",PointX=119.082458,PointY=36.925349},
                new Area{Id=2,Name="北张氏村",Description="北张氏村<br>北张氏村<br>北张氏村<br>北张氏村<br>北张氏村",PointX=119.158073,PointY=36.802697},
                new Area{Id=3,Name="高里三村",Description="<div style='margin:0;line-height:20px;padding:2px;'>" +
                    "<img src='http://www.xxjxsj.cn/article/UploadPic/2009-10/2009101018545196251.jpg' alt='' style='float:right;zoom:1;overflow:hidden;width:100px;height:100px;margin-left:3px;'/>" +
                    "地址：北京市海淀区上地十街10号<br/>电话：(010)59928888<br/>简介：百度大厦位于北京市海淀区西二旗地铁站附近，为百度公司综合研发及办公总部。" +
                  "</div>",PointX=119.042955,PointY=36.847777}
            };
            return data;
        }
    }

}