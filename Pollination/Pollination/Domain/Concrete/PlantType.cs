using NHibernate.Type;

namespace Pollination.Domain.Concrete
{
    public enum PlantType
    {
        Flower,
        Bush,
        Tree
    }

    public class PlantsType : EnumStringType<PlantType>
    {
    }
}