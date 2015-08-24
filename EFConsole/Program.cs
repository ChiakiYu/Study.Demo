using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using EF;
using EF.Model.Users;

namespace EFConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string file = "EF.Mapper.dll";
            var assembly = Assembly.LoadFrom(file);
            DatabaseInitializer.AddMapperAssembly(assembly);
            DatabaseInitializer.Initialize();


            var list = new List<User>();
            for (var i = 0; i < 1000; i++)
            {
                var user = new User { UserName = i.ToString(), NickName = "用户" + i };
                list.Add(user);
            }


            var sw = new Stopwatch();
            sw.Start();
            using (var context = new CodeFirstDbContext())
            {
                context.Users.AddRange(list);
                context.SaveChanges();
            }
            sw.Stop();
            var time = sw.Elapsed;
            string result = string.Format("写入数据库耗时：{0}", time);
            Console.WriteLine(result);
            Console.ReadKey();
            //using (var context = new CodeFirstDbContext())
            //{
            //    var user = new User { UserName = "111", NickName = "123" };
            //    context.Users.Add(user);
            //    context.SaveChanges();
            //}
        }
    }
}