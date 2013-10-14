using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Models.GameModels.RockPaperScissors
{
    /// <summary>
    /// A Rock, Paper, Scissors player.
    /// </summary>
    public class RockPaperScissorsPlayer : GamePlayer
    {
        public RockPaperScissorsPlayer(string playerId) : base(playerId) { }

        public string Selection { get; set; }
        public bool isWinner { get; set; }
    }
}
