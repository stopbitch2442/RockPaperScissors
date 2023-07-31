using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Api.Helpers;
using RockPaperScissors.Domain.Game;

namespace RockPaperScissorsGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly Dictionary<string, Game> games = new Dictionary<string, Game>();
        /// <summary>
        /// Создать игру
        /// </summary>
        /// <param name="userName">Введите никнейм</param>
        /// <returns></returns>
        [HttpPost("create")]
        public ActionResult<Game> CreateGame(string userName)
        {
            var gameId = GenerateGameId();
            var playerCode = GeneratePlayerCode();

            var game = new Game(gameId);
            game.AddPlayer(playerCode, userName);

            games.Add(gameId, game);

            return Ok(new { GameId = gameId, PlayerCode = playerCode });
        }
        /// <summary>
        /// Присоединиться к игре
        /// </summary>
        /// <param name="gameId">Введите код игры</param>
        /// <param name="userName">Введите никнейм</param>
        /// <returns></returns>
        [HttpPost("{gameId}/join")]
        public ActionResult<string> JoinGame(string gameId, string userName)
        {
            if (!games.ContainsKey(gameId))
            {
                return NotFound();
            }

            var playerCode = GeneratePlayerCode();
            var game = games[gameId];
            game.AddPlayer(playerCode, userName);

            return Ok(playerCode);
        }

        [HttpPost("{gameId}/user/{userId}/{turn}")]
        public ActionResult<string> MakeTurn(string gameId, string userId, string turn)
        {
            if (!games.ContainsKey(gameId))
            {
                return NotFound();
            }

            var game = games[gameId];

            if (!game.HasPlayer(userId))
            {
                return NotFound();
            }

            game.AddTurn(userId, turn);

            if (game.IsRoundComplete())
            {
                game.PlayNextRound();
            }

            if (game.IsGameComplete())
            {
                game.EndGame();
            }

            return Ok();
        }

        [HttpGet("{gameId}/stat")]
        public ActionResult<Dictionary<string, int>> GetStatistics(string gameId)
        {
            if (!games.ContainsKey(gameId))
            {
                return NotFound();
            }

            var game = games[gameId];

            if (!game.IsGameComplete())
            {
                return BadRequest("Game is not complete yet.");
            }

            return Ok(game.GetStatistics());
        }

        private string GenerateGameId()
        {
            string gameId = GenerateCodeSixChar.GenerateCode();
            return gameId;
            
        }

        private string GeneratePlayerCode()
        {
            string playerCode = GenerateCodeSixChar.GenerateCode();
            return playerCode;
        }
    }
}