using System;
using Xamarin.Essentials;

namespace RecipeApp.Models
{
    public class CurrentUser
    {
        private static readonly Lazy<CurrentUser> lazy = new Lazy<CurrentUser>(() => new CurrentUser());
        public static CurrentUser Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public string Username
        {
            get => Preferences.Get(nameof(Username), string.Empty);
            set => Preferences.Set(nameof(Username), value);
        }
        public string Password
        {
            get => Preferences.Get(nameof(Password), string.Empty);
            set => Preferences.Set(nameof(Password), value);
        }

        //public AccessToken AccessToken
        //{
        //    get => GetClassForKey<AccessToken>(nameof(AccessToken), new AccessToken());
        //    set => AddClassForKey<AccessToken>(nameof(AccessToken), value);
        //}

        //public LoginResponse LoginData
        //{
        //    get => GetClassForKey<LoginResponse>(nameof(LoginData), new LoginResponse());
        //    set => AddClassForKey<LoginResponse>(nameof(LoginData), value);
        //}

        public CurrentUser()
        {

        }
    }
}
