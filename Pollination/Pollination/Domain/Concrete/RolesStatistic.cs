using System.Collections.Generic;

namespace Pollination.Domain.Concrete
{
    public class RolesStatistic
    {
        public string BeehiveName { get; set; }

        public List<RoleInfo> RolesInfo { get; set; }
    }

    public class RoleInfo
    {
        public BeeRole BeeRole { get; set; }

        public int Percent { get; set; }
    }
}