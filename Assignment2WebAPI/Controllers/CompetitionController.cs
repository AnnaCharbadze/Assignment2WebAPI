using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assignment2WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        /// <summary>
        /// Finds the bronze-level score (the third-highest distinct score) and the number of participants
        /// who achieved that score.
        /// </summary>
        /// <param name="N">The number of participants.</param>
        /// <param name="scores">
        /// A comma-separated list of participant scores.
        /// For example, "70,62,58,73" for 4 participants.
        /// </param>
        /// <returns>
        /// A string in the format "S P", where S is the bronze-level score and P is the count of participants 
        /// who achieved that score.
        /// </returns>
        /// <example>
        /// GET: api/Competition?N=4&amp;scores=70,62,58,73  
        /// Response: "62 1"
        /// </example>
        [HttpGet]
        public string BronzeCount([FromQuery] int N, [FromQuery] string scores)
        {
            string[] scoreStrings = scores.Split(',');
            if (scoreStrings.Length != N)
            {
                return "Error: The number of scores does not match N.";
            }
            int[] scoreArray = new int[N];
            for (int i = 0; i < N; i++)
            {
                scoreArray[i] = int.Parse(scoreStrings[i].Trim());
            }
            int[] distinctScores = new int[N]; 
            int distinctCount = 0;
            for (int i = 0; i < N; i++)
            {
                bool found = false;
                for (int j = 0; j < distinctCount; j++)
                {
                    if (scoreArray[i] == distinctScores[j])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    distinctScores[distinctCount] = scoreArray[i];
                    distinctCount++;
                }
            }

   
            for (int i = 0; i < distinctCount - 1; i++)
            {
                for (int j = i + 1; j < distinctCount; j++)
                {
                    if (distinctScores[j] > distinctScores[i])
                    {
                        int temp = distinctScores[i];
                        distinctScores[i] = distinctScores[j];
                        distinctScores[j] = temp;
                    }
                }
            }

            if (distinctCount < 3)
            {
                return "Error: Less than 3 distinct scores.";
            }

            int bronzeScore = distinctScores[2];

            int countBronze = 0;
            for (int i = 0; i < N; i++)
            {
                if (scoreArray[i] == bronzeScore)
                {
                    countBronze++;
                }
            }

            Debug.WriteLine($"Bronze Score: {bronzeScore}, Count: {countBronze}");

            // Return the result as "S P"
            return bronzeScore + " " + countBronze;
        }
    }
}
