using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Json
{
    class Program
    {



        static void Main(string[] args)
        {

            List<string> area = new List<string>()
            {
                "坊子区",
                "潍坊市",
                "临朐县",
                "青州市",
                "奎文区",
                "安丘市",
                "诸城市",
                "昌邑县",
                "滨海区",
                "寒亭区",
                "高密市",
                "潍城区",
                "保税区",
                "昌乐市",
                "峡山区",
                "高新区"
            };
            List<int> online = new List<int>()
            {
                590,
                4500,
                345,
                434,
                34,
                345,
                45,
                455,
                45,
                4545,
                4545,
                45,
                4545,
                556,
                666,
                555
            };
            List<int> res = new List<int>()
            {
                67,
                77,
                5,
                7,
                5,
                6,
                6,
                7,
                7,
                77,
                765,
                6,
                55,
                77,
                65,
                44
            };
            var hehe = new
            {
                name = "注册人数",
                data = online
            };
            var hehe1 = new
            {
                name = "在线人数",
                data = res
            };
            var data = new { categories = area, series = new { hehe, hehe1 } };
            var result = JsonConvert.SerializeObject(data);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
