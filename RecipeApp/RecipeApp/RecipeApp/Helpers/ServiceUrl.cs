namespace RecipeApp.Helpers
{
    public class ServiceUrl
    {
        public const string BASE_URL = "https://www.themealdb.com/api/json/v1/1/";
        public const string GET_CATEGORY = BASE_URL + "categories.php";
        public const string GET_MEALS = BASE_URL + "filter.php";
        public const string GET_MEAL_DETAIL = BASE_URL + "lookup.php";
    }
}
