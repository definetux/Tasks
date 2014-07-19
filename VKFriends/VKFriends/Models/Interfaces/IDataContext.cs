using System.Data.Entity;
using VKFriends.Models.Concrete;

namespace VKFriends.Models.Interfaces
{
    public interface IDataContext
    {
        DbSet<User> Users { get; set; }

        DbSet<Friend> Friends { get; set; }
        void Save();
    }
}