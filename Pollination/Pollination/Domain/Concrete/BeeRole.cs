using NHibernate.Type;

namespace Pollination.Domain.Concrete
{
    public enum BeeRole
    {
        Soldier,
        Worker,
        Nectariferous
    }

    public class BeeRoleType : EnumStringType<BeeRole>
    {
    }
}