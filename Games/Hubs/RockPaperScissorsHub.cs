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

        public void Join(string sessionId)
        {
            Groups.Add(Context.ConnectionId, sessionId);
        }

        public void PlayerSelectedValue(string sessionId, string playerId, string value)
        {
            var game = ObjectFactory.GetInstance<RockPaperScissorsGame>();
            var session = game.Sessions.First(g => g.Id.ToString() == sessionId);
            session.Players[playerId].Selection = value;

            if (!session.Players.Any(p => p.Value.Selection == null))
            {
                // all players have made their selection
                var winner = session.GetWinner();

                // Broadcast to other clients in this game that there is a winner!
                Clients.Group(sessionId).winner(session.Players.Values.ToList()[0], session.Players.Values.ToList()[1]);
            }
        }
    }
}