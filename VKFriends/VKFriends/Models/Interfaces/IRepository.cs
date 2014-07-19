using System.Linq;
using VKFriends.Models.Concrete;

namespace VKFriends.Models.Interfaces
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }

        IQueryable<Friend> Friends { get; } 

        void AddUser(User user);

        void AddFriend(Friend friend);
    }
}