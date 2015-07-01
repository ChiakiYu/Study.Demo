using Autofac.Application;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autofac
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {

            InitContainer();
            _container.Resolve<DatabaseManager>().Search("SELECT * FORM USER");
            Console.ReadKey();
        }

        static private void InitContainer()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly).AsSelf().AsImplementedInterfaces();

            //builder.RegisterType<DatabaseManager>();
            //builder.RegisterType<SqlDatabase>().As<IDatabase>();
            builder.RegisterType<MySqlDatabase>().As<IDatabase>();
            _container = builder.Build();

        }
    }
}
