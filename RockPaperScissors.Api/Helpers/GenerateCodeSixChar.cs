namespace RockPaperScissors.Api.Helpers
{
    public class GenerateCodeSixChar
    {
        public static string GenerateCode()
        {
            Guid randomGuid = Guid.NewGuid();
            string gameGuidString = randomGuid.ToString();
            string gameId = gameGuidString.Substring(0, 6);
            return gameId;
        }
    }
}
