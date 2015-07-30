using EF.Model.Navigations;

namespace EF.Mapper.Navigations
{
    public class NavigationMapper : EntityConfiguration<Navigation>
    {
        public NavigationMapper()
        {
            ToTable("Sys_Navigations");
        }
    }
}