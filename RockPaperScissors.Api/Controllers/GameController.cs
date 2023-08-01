using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Api.Helpers;
using RockPaperScissors.Domain.Db;
using RockPaperScissors.Domain.Game;
using System.Threading.Tasks;

namespace RockPaperScissorsGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly RockPaperScissorsDbContext _context;
        private static readonly Dictionary<string, Game> games = new Dictionary<string, Game>();

        public GameController(RockPaperScissorsDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Game>> CreateGame(string userName)
        {
            if (ModelState.IsValid)
            {
                var gameId = GenerateGameId();
                var playerCode = GeneratePlayerCode();

                var game = new Game(gameId);
                game.AddPlayer(playerCode, userName);
                games.Add(gameId, game);

                await _context.Game.AddAsync(game);
                await _context.SaveChangesAsync();

                return Ok(new { GameId = gameId, PlayerCode = playerCode });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("{gameId}/join")]
        public async Task<ActionResult<string>> JoinGame(string gameId, string userName)
        {
            var game = await _context.Game.FindAsync(gameId);
            if (game == null)
            {
                return NotFound("Игра не найдена");
            }

            if (game.IsGameComplete())
            {
                return BadRequest("Игра уже завершена");
            }

            var playerCode = GeneratePlayerCode();
            game.AddPlayer(playerCode, userName);

            await _context.SaveChangesAsync();

            return Ok($"player2Code: {playerCode}");
        }

        [HttpPost("{gameId}/user/{playerId}/{turn}")]
        public ActionResult<string> MakeTurn(string gameId, string playerId, string turn)
        {
            if (!games.ContainsKey(gameId))
            {
                return NotFound();
            }

            var game = games[gameId];

            if (!game.HasPlayer(playerId))
            {
                return NotFound();
            }

            if (game.IsGameComplete())
            {
                return BadRequest("Игра уже завершена");
            }

            game.AddTurn(playerId, turn);

            if (game.IsRoundComplete())
            {
                game.PlayNextRound();
            }

            if (game.IsGameComplete())
            {
                return BadRequest("Игра закончилась");
            }

            _context.SaveChanges();

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
                return BadRequest("Игра ещё не закончилась");
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