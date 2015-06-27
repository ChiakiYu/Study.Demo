using Autofac.Application;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<DatabaseManager>();
            builder.RegisterType<SqlDatabase>().As<IDatabase>();
            using (var container = builder.Build())
            {
                var manager = container.Resolve<DatabaseManager>();
                manager.Search("SELECT * FORM USER");
            }
            Console.ReadKey();
        }
    }
}
