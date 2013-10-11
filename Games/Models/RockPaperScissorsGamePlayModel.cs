using Games.Models.GameModels.RockPaperScissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models
{
    public class RockPaperScissorsGamePlayModel
    {
        public RockPaperScissorsGamePlayModel()
        {
            Players = new List<RockPaperScissorsPlayer>();
        }

        public int PlayerCount { get; set; }

        public object PlayerId { get; set; }

        public object SessionId { get; set; }

        public List<RockPaperScissorsPlayer> Players { get; set; }
    }
}