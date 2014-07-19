namespace VKFriends.Models.Concrete
{
    public class User
    {
        public int UserId { get; set; }
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string user_id { get; set; }
    }
}