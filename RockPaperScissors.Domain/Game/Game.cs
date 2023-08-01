using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Domain.Game
{
    public class Game
    {
        public string GameId { get; set; }
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
            var roundWinner = GetRoundWinner();
            UpdateStatistics(roundWinner);

            Turns.Clear();
            CurrentRound++;

            if (IsGameComplete())
            {
                IsComplete = true;
            }
        }

        public bool IsGameComplete()
        {
            return CurrentRound > 5;
        }


        public Dictionary<string, int> GetStatistics()
        {
            var statistics = new Dictionary<string, int>();
            foreach (var player in Players)
            {
                statistics.Add(player.Value, 0);
            }

            foreach (var turn in Turns.Values)
            {
                if (statistics.ContainsKey(turn))
                {
                    statistics[turn]++;
                }
            }

            return statistics;
        }

        private string GetRoundWinner()
        {
            var roundTurns = new List<string>(Turns.Values);
            var uniqueTurns = roundTurns.Distinct().ToList();

            if (uniqueTurns.Count == 1)
            {
                return null;
            }

            if (uniqueTurns.Count == 3)
            {
                return null;
            }

            if (uniqueTurns.Count == 2)
            {
                string winningTurn = null;

                if (uniqueTurns.Contains("rock") && uniqueTurns.Contains("paper"))
                {
                    winningTurn = "paper";
                }
                else if (uniqueTurns.Contains("rock") && uniqueTurns.Contains("scissors"))
                {
                    winningTurn = "rock";
                }
                else if (uniqueTurns.Contains("paper") && uniqueTurns.Contains("scissors"))
                {
                    winningTurn = "scissors";
                }

                return winningTurn;
            }

            return null;
        }

        private void UpdateStatistics(string roundWinner)
        {
            if (string.IsNullOrEmpty(roundWinner))
            {
                return;
            }

            foreach (var player in Players)
            {
                if (Turns.ContainsKey(player.Key) && Turns[player.Key] == roundWinner)
                {

                }
            }
        }
    }
}