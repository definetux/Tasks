using System.ComponentModel.DataAnnotations;

namespace VKFriends.Models.Concrete
{
    public class Friend
    {
        public int id { get; set; }

        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Display(Name = "Vk Id")]
        public string domain { get; set; }

        [Display(Name = "Online")]
        public bool online { get; set; }
    }
}