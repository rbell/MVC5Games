using System;
using System.Collections.Generic;

namespace Games.Models.GameModels
{
    /// <summary>
    /// An abstract class that defines a game, such as "Rock, Paper, Scissors" or "tic-tac-toe".
    /// A game simply defines the game, and contains a reference to sessions that are currently active.
    /// </summary>
    /// <typeparam name="S">Type of GameSession</typeparam>
    /// <typeparam name="P">Type of GamePlayer</typeparam>
    public abstract class Game<S,P> 
        where P : GamePlayer
        where S : GameSession<P>
    {
        public Game()
        {
            Sessions = new List<S>();
        }

        public List<S> Sessions { get; private set; }
    }
}