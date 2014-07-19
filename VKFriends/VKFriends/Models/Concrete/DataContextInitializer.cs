using System.Data.Entity;

namespace VKFriends.Models.Concrete
{
    public class DataContextInitializer : DropCreateDatabaseAlways<VkFriendsDataContext>
    {
        protected override void Seed(VkFriendsDataContext context)
        {
            base.Seed(context);
        }
    }
}