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
        private string _id = Guid.NewGuid().ToString();

        public GameSession(string name)
        {
            Name = name;
            Players = new List<P>();
        }

        public string Id 
        { 
            get { return _id; } 
        }

        public string Name { get; set; }

        public List<P> Players { get; protected set; }
    }
}
