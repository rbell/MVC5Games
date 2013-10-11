using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Models.GameModels
{
    /// <summary>
    /// A GameSession is an instance of a game being played by players.  A game may have multiple players envolved in playing
    /// a game in a session.  Each session has a unique Id and a Name (i.e. "Donald's Game").
    /// </summary>
    /// <typeparam name="P">Type of GamePlayer</typeparam>
    public abstract class GameSession<P> where P : GamePlayer
    {
        private Guid _id = Guid.NewGuid();

        public GameSession(string name)
        {
            Name = name;
            Players = new Dictionary<string, P>();
        }

        public Guid Id 
        { 
            get { return _id; } 
        }

        public string Name { get; set; }

        public Dictionary<string, P> Players { get; protected set; }
    }
}
