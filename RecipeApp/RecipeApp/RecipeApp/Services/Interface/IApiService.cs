using RecipeApp.Models.Response;
using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Services.Interface
{
    public interface IApiService
    {
        Task<BaseResponse<GetCategoryResponse>> GetCategories();
        Task<BaseResponse<GetMealResponse>> GetMeals(string category);
        Task<BaseResponse<GetMealResponse>> GetMealDetails(string meal);
    }
}
