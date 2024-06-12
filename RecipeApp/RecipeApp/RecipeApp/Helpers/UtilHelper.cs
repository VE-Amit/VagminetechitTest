using Xamarin.Essentials;

namespace RecipeApp.Helpers
{
    public static class UtilHelper
    {
        public static bool IsInternetConnected()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }

        public static bool VerifyHasValue(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            return true;
        }
    }
}
