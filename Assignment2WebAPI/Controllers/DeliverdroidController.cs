using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace Assignment2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DelivedroidController : ControllerBase
    {
        /// <summary>
        /// Calculates the final score.
        /// </summary>
        /// <param name="collisions">The number of collisions with obstacles.</param>
        /// <param name="deliveries">The number of packages delivered.</param>
        /// <returns>The final score based on game rules.</returns>
        /// <example>
        /// POST: api/Delivedroid  
        /// Content-Type: application/x-www-form-urlencoded  
        /// Request Body: Collisions=2&Deliveries=5  
        /// Response: 730
        /// </example>
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public int CalculateScore([FromForm] int collisions, [FromForm] int deliveries)
        {
            int score = (deliveries * 50) - (collisions * 10);

            if (deliveries > collisions)
            {
                score += 500;
            }

            Debug.WriteLine($"Collisions: {collisions}, Deliveries: {deliveries}, Final Score: {score}");
            return score;
        }
    }
}