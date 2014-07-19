using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using VKFriends.Models.Concrete;
using VKFriends.Models.Interfaces;

namespace VKFriends.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отправляем запрос на авторизацию
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorize()
        {
            var appSettings = ApplicationSettings.Instance;
            return Redirect(GetLoginUrl(appSettings.ClientId, appSettings.Scope));
        }

        /// <summary>
        /// Получаем токен и сохраняем данные о пользователе
        /// </summary>
        /// <param name="code"> Код для получения токена </param>
        /// <returns></returns>
        public ActionResult CurrentUser(string code)
        {
            var appSettings = ApplicationSettings.Instance;
            var url = GetAuthorizeUrl(appSettings.ClientId, appSettings.ClientSecret, code);

            var response = GetRequest(url);

            var userData = JsonConvert.DeserializeObject<User>(response);

            if (userData != null)
            {
                _repository.AddUser(userData);
            }
            return RedirectToAction("Info");
        }

        /// <summary>
        /// Выводим информацию о текущем пользователе
        /// </summary>
        /// <returns></returns>
        public ActionResult Info()
        {
            var userData = _repository.Users.FirstOrDefault();
            if (userData != null) 
                ViewBag.UserId = userData.user_id;

            return View();
        }

        /// <summary>
        /// Получаем расширенную информацию о пользователе
        /// </summary>
        /// <param name="id"> Id пользователя </param>
        /// <returns></returns>
        public PartialViewResult _GetUserInfo(string id)
        {
            var userData = _repository.Users.FirstOrDefault(x => x.user_id == id);

            if (userData == null) 
                return PartialView();

            var userInfoUrl = GetUserInfoUrl(userData.user_id, userData.access_token);
            var response = GetRequest(userInfoUrl);

            var sJObject = JObject.Parse(response);

            var userInfo = JsonConvert.DeserializeObject<UserInfo>(sJObject["response"][0].ToString());

            return PartialView(userInfo);
        }

        /// <summary>
        /// Получить список друзей и отобразить их на View
        /// </summary>
        /// <returns></returns>
        public ActionResult FriendList()
        {
            var firstOrDefault = _repository.Users.FirstOrDefault();

            List<Friend> friends = null;

            if (firstOrDefault != null)
            {
                var userData = firstOrDefault;
                var friendsInfoUrl = GetFriendsUrl(userData.user_id, userData.access_token);

                var response = GetRequest(friendsInfoUrl);

                var sJObject = JObject.Parse(response);

                ViewBag.FriendsCount = sJObject["response"]["count"].ToString();

                friends = JsonConvert.DeserializeObject<List<Friend>>(sJObject["response"]["items"].ToString());
            }

            if (friends != null)
            {
                // Спорное место - если список друзей в БД пуст, заполняем его
                if( !_repository.Friends.Any())
                    foreach (var friend in friends)
                    {
                        _repository.AddFriend(friend);
                    }
            }

            return View(friends);
        }

        private string GetAuthorizeUrl(string clientId, string clientSecret, string code)
        {
            return string.Format("https://oauth.vk.com/access_token?" +
                                    "client_id={0}&" +
                                    "client_secret={1}&" +
                                    "code={2}&redirect_uri=http://localhost:34237/Home/CurrentUser", clientId, clientSecret, code);
        }

        private string GetFriendsUrl(string userId, string accessToken)
        {
            return String.Format(
                @"https://api.vk.com/method/friends.get?user_id={0}&order=name&fields=domain&v=5.23&access_token={1}",
                userId,
                accessToken);
        }

        private string GetUserInfoUrl(string userId, string accessToken)
        {
            return String.Format(
                @"https://api.vk.com/method/users.get?user_id={0}&fields=photo_100&v=5.23&access_token={1}",
                userId,
                accessToken);
        }

        private string GetLoginUrl(string clientId, string scope)
        {
            return String.Format(
                @"https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri=http://localhost:34237/Home/CurrentUser&response_type=code&v=5.23",
                clientId, scope);
        }

        /// <summary>
        /// Отправить запрос на сервер и получить ответ от него
        /// </summary>
        /// <param name="url"> Адрес сервера </param>
        /// <returns></returns>
        private string GetRequest(string url)
        {
            try
            {
                var wr = WebRequest.Create(url);

                var objStream = wr.GetResponse().GetResponseStream();

                if (objStream == null)
                    return "";

                var objReader = new StreamReader(objStream);

                var sb = new StringBuilder();
                while (true)
                {
                    string line = objReader.ReadLine();
                    if (line != null) sb.Append(line);

                    else
                    {
                        return sb.ToString();
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}