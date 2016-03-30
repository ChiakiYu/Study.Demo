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

            JsonDeserialize();
            
            //{"Message":"新用户注册：用户名为 hgjghjgf ，邮箱为 fgtryrt@qq.com","Type":"Abp.Notifications.MessageNotificationData","Properties":{}}

            //JsonDemo();
            Console.ReadKey();
        }

        private static void JsonDeserialize()
        {

            var notificationData = new MessageNotificationData(string.Format("新用户注册：用户名为 {0} ，邮箱为 {1}", "chiaki", "yu_199012@qq.com"));

            var str = notificationData.ToString();

          var sdaf =   Type.GetType(
                "Json.MessageNotificationData, Json, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            var data = JsonConvert.DeserializeObject(str);

            Console.WriteLine(data.GetHashCode());
        }




        #region JsonDemo
        private static void JsonDemo()
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
        } 
        #endregion
    }
}
