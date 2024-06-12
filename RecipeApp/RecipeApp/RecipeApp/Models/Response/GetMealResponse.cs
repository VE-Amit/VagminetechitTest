using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models.Response
{
    public class GetMealResponse
    {
        [JsonProperty("meals")]
        public List<Meal> Meals { get; set; }
    }
}
