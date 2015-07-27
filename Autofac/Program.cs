using System;
using System.Reflection;
using Autofac.Application;
using Autofac.Core;

namespace Autofac
{
    internal class Program
    {
        private static IContainer _container;

        private static void Main(string[] args)
        {
            InitContainer();
            _container.Resolve<DatabaseManager>().Search("SELECT * FORM USER");
            Console.ReadKey();
        }

        private static void InitContainer()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly).AsSelf().AsImplementedInterfaces();

            //builder.RegisterType<DatabaseManager>();
            //builder.RegisterType<SqlDatabase>().As<IDatabase>();
            builder.RegisterType<MySqlDatabase>().As<IDatabase>();
            _container = builder.Build();
        }
    }
}