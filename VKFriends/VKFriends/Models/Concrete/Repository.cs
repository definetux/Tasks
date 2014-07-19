using System.Linq;
using VKFriends.Models.Interfaces;

namespace VKFriends.Models.Concrete
{
    public class Repository : IRepository
    {
        private readonly IDataContext _context;

        public Repository(IDataContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users
        {
            get { return _context.Users; }
        }

        public IQueryable<Friend> Friends
        {
            get { return _context.Friends; }
        }

        public void AddUser(User user)
        {
            // Если пользователей в БД не существует, добавить
            if( !_context.Users.Any() )
                _context.Users.Add(user);
            else
            {
                // Иначе изменить токен и время жизни для текущего пользователя
                var currentUser = _context.Users.FirstOrDefault(x => x.user_id == user.user_id);
                if (currentUser != null)
                {
                    currentUser.access_token = user.access_token;
                    currentUser.expires_in = user.expires_in;
                }
            }
            _context.Save();
        }

        public void AddFriend(Friend friend)
        {
            _context.Friends.Add(friend);
            _context.Save();
        }
    }
}