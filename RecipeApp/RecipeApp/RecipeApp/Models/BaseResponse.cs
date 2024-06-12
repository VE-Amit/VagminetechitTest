using RecipeApp.Helpers;

namespace RecipeApp.Models
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public int ApiStatus { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public bool NoInternet { get; set; }
        public BaseResponse()
        {
        }

        public void GetSuccessResponse()
        {
            ErrorMessage = "Success";
            ApiStatus = 200;
            Success = true;
        }

        public void GetGenericErrorResponse()
        {
            ErrorMessage = Constants.Global_Error;
            ApiStatus = 1001;
            Success = false;
        }


        public void GetNoInternetResponse()
        {
            ErrorMessage = Constants.No_Network_Found;
            ApiStatus = 1000;
            Success = false;
            NoInternet = true;
        }
    }
}
