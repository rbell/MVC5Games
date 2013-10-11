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
        public string PlayerId { get; set; }
        public string Name { get; set; }
    }
}