using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Assignment2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiliPeppersController : ControllerBase
    {
        /// <summary>
        /// Calculates the total Scoville Heat Units (SHU) based on the peppers used.
        /// Accepts only pepper names .
        /// </summary>
        /// <param name="ingredients">A comma-separated list of pepper names.</param>
        /// <returns>The total SHU of the chili.</returns>
        /// <example>
        /// GET: api/ChiliPeppers?ingredients=Poblano,Cayenne,Thai,Poblano  
        /// Response: 118000
        /// </example>
        [HttpGet]
        public int GetTotalSHU([FromQuery] string ingredients)
        {
            var pepperSHU = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase)
            {
                { "POBLANO", 1500 },
                { "MIRASOL", 6000 },
                { "SERRANO", 15500 },
                { "CAYENNE", 40000 },
                { "THAI", 75000 },
                { "HABANERO", 125000 }
            };

            int totalSHU = 0;
            if (string.IsNullOrWhiteSpace(ingredients))
            {
                Debug.WriteLine("No peppers provided. Returning 0.");
                return totalSHU;
            }
            string[] selectedPeppers = ingredients.Split(',');
            foreach (string pepper in selectedPeppers)
            {
                string trimmedPepper = pepper.Trim();
                if (pepperSHU.ContainsKey(trimmedPepper))
                {
                    totalSHU += pepperSHU[trimmedPepper];
                }
                else
                {
                    Debug.WriteLine($"Unknown Pepper: '{trimmedPepper}'. Ignoring...");
                }
            }

            Debug.WriteLine($"Peppers: {ingredients}, Total SHU: {totalSHU}");
            return totalSHU;
        }
    }
}
