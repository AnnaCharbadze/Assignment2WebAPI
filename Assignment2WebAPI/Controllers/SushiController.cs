using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SushiController : ControllerBase
    {
        /// <summary>
        /// Calculates the total cost of a sushi meal based on the number of red, green, and blue plates.
        /// </summary>
        /// <param name="redPlates">The number of red plates chosen ($3 each).</param>
        /// <param name="greenPlates">The number of green plates chosen ($4 each).</param>
        /// <param name="bluePlates">The number of blue plates chosen ($5 each).</param>
        /// <returns>The total cost of the meal.</returns>
        /// <example>
        /// GET: api/Sushi?redPlates=0&greenPlates=2&bluePlates=4  
        /// Response: 28
        /// </example>
        [HttpGet]
        public int CalculateCost([FromQuery] int redPlates, [FromQuery] int greenPlates, [FromQuery] int bluePlates)
        {
            const int redCost = 3;
            const int greenCost = 4;
            const int blueCost = 5;

            int totalCost = (redPlates * redCost) + (greenPlates * greenCost) + (bluePlates * blueCost);
            Debug.WriteLine($"Sushi Order: Red={redPlates}, Green={greenPlates}, Blue={bluePlates}, Total Cost={totalCost}");
            return totalCost;
        }
    }
}
