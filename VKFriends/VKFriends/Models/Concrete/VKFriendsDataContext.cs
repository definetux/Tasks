using System.Data.Entity;
using VKFriends.Models.Interfaces;

namespace VKFriends.Models.Concrete
{
    public class VkFriendsDataContext : DbContext, IDataContext
    {
        public VkFriendsDataContext()
            :base("VkFriends")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public void Save()
        {
            SaveChanges();
        }
    }
}