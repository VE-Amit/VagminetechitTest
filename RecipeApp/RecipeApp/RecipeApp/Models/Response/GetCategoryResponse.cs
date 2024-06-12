using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models.Response
{
    public class GetCategoryResponse
    {
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
    }
}
