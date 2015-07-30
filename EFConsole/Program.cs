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

            //using (var context = new CodeFirstDbContext())
            //{
            //    var user = new User {UserName = "111"};
            //    context.Users.Add(user);
            //    context.SaveChanges();
            //}
        }
    }
}