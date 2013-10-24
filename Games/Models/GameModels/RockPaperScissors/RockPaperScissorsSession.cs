using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Models.GameModels.RockPaperScissors
{
    /// <summary>
    /// A Rock, Paper, Scissors game session
    /// </summary>
    public class RockPaperScissorsSession : GameSession<RockPaperScissorsPlayer>
    {
        public RockPaperScissorsSession(string name)
            : base(name)
        {
        }

        internal RockPaperScissorsPlayer GetWinner()
        {
            if (Players.Count() != 2 || Players.Any(p => p.Selection == null))
            {
                return null;
            }


            var playersList = Players.ToList();
            playersList.ForEach(p =>
                {
                    p.isWinner = false;
                });

            var winner = comparePlayer(playersList[0], playersList[1]);

            if (winner != null)
            {
                playersList.First(p => p.PlayerId == winner.PlayerId).isWinner = true;
            }

            return winner;
        }

        private RockPaperScissorsPlayer comparePlayer(RockPaperScissorsPlayer p1, RockPaperScissorsPlayer p2)
        {
            var s1 = p1.Selection.ToLower();
            var s2 = p2.Selection.ToLower();

            if ((s1 == "rock" && s2 == "scissors") || (s2 == "rock" && s1 == "scissors"))
            {
                // Rock breaks scissors
                return s1 == "rock" ? p1 : p2;
            }

            if ((s1 == "paper" && s2 == "scissors") || (s2 == "paper" && s1 == "scissors"))
            {
                // Scissors cuts paper
                return s1 == "scissors" ? p1 : p2;
            }

            if ((s1 == "paper" && s2 == "rock") || (s2 == "paper" && s1 == "rock"))
            {
                // Paper covers rock
                return s1 == "paper" ? p1 : p2;
            }

            // Players selected same value
            return null;
        }
    }
}
