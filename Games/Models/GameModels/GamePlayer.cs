using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games.Models.GameModels
{
    /// <summary>
    /// A player of a game
    /// </summary>
    public abstract class GamePlayer
    {
        public GamePlayer(string playerId)
        {
            PlayerId = playerId;
        }

        public string PlayerId { get; set; }
        public string Name { get; set; }
    }
}