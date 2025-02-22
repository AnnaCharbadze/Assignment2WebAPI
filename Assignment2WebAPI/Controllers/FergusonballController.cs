using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Assignment2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FergusonballController : ControllerBase
    {
        /// <summary>
        /// Calculates the number of players with a star rating greater than 40.
        /// If all players have a rating greater than 40, a '+' is appended to the count.
        /// </summary>
        /// <param name="scores">A comma-separated list of player scores.</param>
        /// <param name="fouls">A comma-separated list of player fouls.</param>
        /// <returns>A string in the format "X" or "X+" where X is the count of players with a rating > 40.</returns>
        /// <example>
        /// GET: api/Fergusonball?scores=12,10,9&fouls=4,3,1  
        /// Response: "3+"
        /// </example>
        [HttpGet]
        public string GetFergusonballResults([FromQuery] string scores, [FromQuery] string fouls)
        {
            string[] scoreStrings = scores.Split(',');
            int[] scoreArray = new int[scoreStrings.Length];
            for (int i = 0; i < scoreStrings.Length; i++)
            {
                scoreArray[i] = int.Parse(scoreStrings[i].Trim());
            }

            string[] foulStrings = fouls.Split(',');
            int[] foulArray = new int[foulStrings.Length];
            for (int i = 0; i < foulStrings.Length; i++)
            {
                foulArray[i] = int.Parse(foulStrings[i].Trim());
            }

            if (scoreArray.Length != foulArray.Length)
            {
                return "Error: Mismatched input sizes.";
            }

            int totalPlayers = scoreArray.Length;
            int countAbove40 = 0;
            bool allQualify = true;

            for (int i = 0; i < totalPlayers; i++)
            {
                int rating = (scoreArray[i] * 5) - (foulArray[i] * 3);
                Debug.WriteLine($"Player {i + 1}: Points = {scoreArray[i]}, Fouls = {foulArray[i]}, Rating = {rating}");

                if (rating > 40)
                {
                    countAbove40++;
                }
                else
                {
                    allQualify = false;
                }
            }

            return allQualify ? $"{countAbove40}+" : $"{countAbove40}";
        }
    }
}