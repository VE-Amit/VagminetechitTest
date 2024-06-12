using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Models.Response;
using RecipeApp.Services.Interface;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public class ApiService : IApiService
    {
        /// <summary>
        /// Get Recipe Category Api Call
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse<GetCategoryResponse>> GetCategories()
        {
            return await ApiHelper.GetAsync<GetCategoryResponse>(ServiceUrl.GET_CATEGORY);
        }

        /// <summary>
        /// Get Meals Api Call
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse<GetMealResponse>> GetMeals(string category)
        {
            return await ApiHelper.GetAsync<GetMealResponse>($"{ServiceUrl.GET_MEALS}?c={category}");
        }

        /// <summary>
        /// Get Meals Detail Api Call
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse<GetMealResponse>> GetMealDetails(string meal)
        {
            return await ApiHelper.GetAsync<GetMealResponse>($"{ServiceUrl.GET_MEAL_DETAIL}?i={meal}");
        }
    }
}
