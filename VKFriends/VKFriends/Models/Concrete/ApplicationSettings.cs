using System;

namespace VKFriends.Models.Concrete
{
    public class ApplicationSettings
    {
        private static readonly Lazy<ApplicationSettings> Lazy = new Lazy<ApplicationSettings>( () => new ApplicationSettings());

        private ApplicationSettings()
        {
            ClientId = "4467059";
            Scope = "2";
            ClientSecret = "a0sgVTAKXX7xPijV6RLC";
        }
        public static ApplicationSettings Instance {get{ return Lazy.Value;}}

        public string  ClientId { get; private set; }

        public string Scope { get; private set; }

        public string ClientSecret { get; private set; }
    }
}