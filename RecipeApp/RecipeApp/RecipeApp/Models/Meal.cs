using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class Meal
    {
        [JsonProperty("idMeal")]
        public string IdMeal { get; set; }

        [JsonProperty("strMeal")]
        public string StrMeal { get; set; }

        [JsonProperty("strMealThumb")]
        public string StrMealThumb { get; set; }

        [JsonProperty("strCategory")]
        public string StrCategory { get; set; }

        [JsonProperty("strArea")]
        public string StrArea { get; set; }

        [JsonProperty("strInstructions")]
        public string StrInstructions { get; set; }

        [JsonProperty("strYoutube")]
        public string StrYoutube { get; set; }

        [JsonProperty("strIngredient1")]
        public string StrIngredient1 { get; set; }

        [JsonProperty("strIngredient2")]
        public string StrIngredient2 { get; set; }

        [JsonProperty("strIngredient3")]
        public string StrIngredient3 { get; set; }

        [JsonProperty("strIngredient4")]
        public string StrIngredient4 { get; set; }
    }
}
