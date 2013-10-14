using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using StructureMap;
using Games.Models.GameModels.RockPaperScissors;

namespace Games.Hubs
{
    public class RockPaperScissorsHub : Hub
    {
        public void NewGameCreated(string id, string name)
        {
            Clients.All.newGame(id, name);
        }

        public void GameEnded(string id)
        {
            Clients.All.endGame(id);
        }

        public RockPaperScissorsSession JoinGame(string sessionId, string sessionName)
        {
            var game = ObjectFactory.GetInstance<RockPaperScissorsGame>();
            RockPaperScissorsSession session = null;
            var playerId = Context.ConnectionId;

            if (string.IsNullOrWhiteSpace(sessionId))
            {
                // New Game
                session = new RockPaperScissorsSession(sessionName);
                session.Players.Add(new RockPaperScissorsPlayer(playerId) {Name = Context.User.Identity.Name });
                game.Sessions.Add(session);

                Groups.Add(Context.ConnectionId, session.Id.ToString());

                // Broadcast to other subscribers that there is now a new game
                Clients.All.newGame(session.Id.ToString(), session.Name);
            }
            else
            {
                // Player joining existing game
                session = game.Sessions.First(g => g.Id.ToString() == sessionId);
                var newPlayer = new RockPaperScissorsPlayer(playerId) { Name = Context.User.Identity.Name };
                session.Players.Add(newPlayer);

                Groups.Add(Context.ConnectionId, session.Id.ToString());

                // Broadcast to other clients in this game that a player has joined
                Clients.Group(session.Id.ToString()).playerJoined(newPlayer, session.Players.Count);
            }

            return session;
        }

        public void PlayerSelectedValue(string sessionId, string playerId, string value)
        {
            var game = ObjectFactory.GetInstance<RockPaperScissorsGame>();
            var session = game.Sessions.First(g => g.Id.ToString() == sessionId);
            session.Players.First(p => p.PlayerId == playerId).Selection = value;

            if (!session.Players.Any(p => p.Selection == null))
            {
                // all players have made their selection
                var winner = session.GetWinner();

                // Broadcast to other clients in this game that there is a winner!
                Clients.Group(sessionId).winner(session.Players.ToList()[0], session.Players.ToList()[1]);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var playerId = Context.ConnectionId;
            var game = ObjectFactory.GetInstance<RockPaperScissorsGame>();
            var session = game.Sessions.FirstOrDefault(s => s.Players.Any(p => p.PlayerId == playerId));
            if (session != null)
            {
                if (session.Players.Any(p => p.PlayerId == playerId))
                {
                    session.Players.Remove(session.Players.First(p => p.PlayerId == playerId));
                }

                if (session.Players.Count == 0)
                {
                    Clients.All.EndGame(session.Id);
                    game.Sessions.Remove(session);
                }
            }
            return base.OnDisconnected();
        }

    }
}