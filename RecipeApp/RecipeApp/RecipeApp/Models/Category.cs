using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class Category
    {
        [JsonProperty("idCategory")]
        public string IdCategory { get; set; }

        [JsonProperty("strCategory")]
        public string StrCategory { get; set; }

        [JsonProperty("strCategoryThumb")]
        public string StrCategoryThumb { get; set; }

        [JsonProperty("strCategoryDescription")]
        public string StrCategoryDescription { get; set; }
    }
}
