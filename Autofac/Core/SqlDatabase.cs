using System;

namespace Autofac.Core
{
    public class SqlDatabase : IDatabase
    {
        public string Name
        {
            get { return "SqlServer"; }
        }

        public void Select(string commandText)
        {
            Console.WriteLine("'{0}' is a query sql in {1}!", commandText, Name);
        }

        public void Insert(string commandText)
        {
            Console.WriteLine("'{0}' is a insert sql in {1}!", commandText, Name);
        }

        public void Update(string commandText)
        {
            Console.WriteLine("'{0}' is a update sql in {1}!", commandText, Name);
        }

        public void Delete(string commandText)
        {
            Console.WriteLine("'{0}' is a delete sql in {1}!", commandText, Name);
        }
    }
}