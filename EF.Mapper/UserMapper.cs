using System.ComponentModel.DataAnnotations.Schema;
using EF.Model.Users;

namespace EF.Mapper
{
    public class UserMapper : EntityConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("Sys_Users");
            HasKey(c => c.Id).Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(n => n.UserName).IsRequired().HasMaxLength(256);
        }
    }
}