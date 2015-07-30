using System.Data.Entity;
using EF.Model.Users;

namespace EF
{
    public class CodeFirstDbContext : DbContext
    {
        public CodeFirstDbContext()
            : base("Default")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new UserMapper());
            //注册实体配置信息
            var entityMappers = DatabaseInitializer.EntityMappers;
            foreach (var mapper in entityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }
        }
    }
}