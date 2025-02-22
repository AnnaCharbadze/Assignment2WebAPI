using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Assignment2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        /// <summary>
        /// Finds the bronze-level score (the third-highest distinct score) 
        /// and how many participants achieved it.
        /// </summary>
        /// <param name="N">Number of participants.</param>
        /// <param name="scores">A comma-separated list of participant scores.</param>
        /// <returns>A string in the format "S P", where S is the bronze-level score and P is the count of participants with that score.</returns>
        /// <example>
        /// GET: api/Competition?N=4&amp;scores=70,62,58,73
        /// Response: "62 1"
        /// </example>
        /// <example>
        /// GET: api/Competition?N=8&amp;scores=75,70,60,70,70,60,75,70
        /// Response: "60 2"
        /// </example>
        [HttpGet]
        public string BronzeLevel([FromQuery] int N, [FromQuery] string scores)
        {
            var scoreList = scores
                .Split(',')
                .Select(s => int.Parse(s.Trim()))
                .ToList();
            if (scoreList.Count != N)
            {
                return "Error: The number of scores does not match N.";
            }

            var distinctScores = scoreList
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            int bronzeScore = distinctScores[2];

            int countBronze = scoreList.Count(x => x == bronzeScore);

            Debug.WriteLine($"Bronze Score: {bronzeScore}, Count: {countBronze}");

            return $"{bronzeScore} {countBronze}";
        }
    }
}