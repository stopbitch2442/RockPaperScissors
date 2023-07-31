using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Domain.Game
{
    public class Game
    {
        public string GameId { get; }
        public Dictionary<string, string> Players { get; }
        public Dictionary<string, string> Turns { get; }
        public int CurrentRound { get; private set; }
        public bool IsComplete { get; private set; }

        public Game(string gameId)
        {
            GameId = gameId;
            Players = new Dictionary<string, string>();
            Turns = new Dictionary<string, string>();
            CurrentRound = 1;
            IsComplete = false;
        }

        public void AddPlayer(string playerCode, string userName)
        {
            Players.Add(playerCode, userName);
        }

        public bool HasPlayer(string playerCode)
        {
            return Players.ContainsKey(playerCode);
        }

        public void AddTurn(string playerCode, string turn)
        {
            if (Turns.ContainsKey(playerCode))
            {
                Turns[playerCode] = turn;
            }
            else
            {
                Turns.Add(playerCode, turn);
            }
        }

        public bool IsRoundComplete()
        {
            return Turns.Count == Players.Count;
        }

        public void PlayNextRound()
        {
            // Implement your logic to determine the winner of the round
            // based on the players' turns

            Turns.Clear();
            CurrentRound++;

            if (CurrentRound > 5)
            {
                IsComplete = true;
            }
        }

        public bool IsGameComplete()
        {
            return IsComplete;
        }

        public void EndGame()
        {
            if (IsGameComplete()) { }
        }

        public Dictionary<string, int> GetStatistics()
        {
            // Implement your logic to retrieve game statistics
            return new Dictionary<string, int>();
        }
    }
}
