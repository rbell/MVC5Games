using Games.Models.GameModels.RockPaperScissors;
using Games.Hubs;
using Games.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers
{
    public class RockPaperScissorsController : Controller
    {
        private RockPaperScissorsGame _game;

        public RockPaperScissorsController(RockPaperScissorsGame game)
        {
            _game = game;
        }

        public ActionResult Index()
        {
            var model = new GameList();
            foreach (RockPaperScissorsSession game in _game.Sessions)
            {
                model.Add(new Tuple<string, string>(game.Id.ToString(), game.Name));
            }
            return View(model);
        }

        public ActionResult JoinGame(string id = "", string name = "")
        {
            var playerId = Guid.NewGuid().ToString();
            RockPaperScissorsSession session = null;

            if (string.IsNullOrWhiteSpace(id))
            {
                // New Game
                session = new RockPaperScissorsSession(name);
                session.Players.Add(playerId, new RockPaperScissorsPlayer() {PlayerId = playerId, Name = User.Identity.Name });
                _game.Sessions.Add(session);

                // Broadcast to other subscribers that there is now a new game
                GlobalHost.ConnectionManager.GetHubContext<RockPaperScissorsHub>().Clients.All.newGame(session.Id.ToString(), session.Name);
            }
            else
            {
                // Player joining existing game
                session = _game.Sessions.First(g => g.Id.ToString() == id);
                var newPlayer = new RockPaperScissorsPlayer() {PlayerId = playerId, Name = User.Identity.Name};
                session.Players.Add(playerId, newPlayer);

                // Broadcast to other clients in this game that a player has joined
                GlobalHost.ConnectionManager.GetHubContext<RockPaperScissorsHub>().Clients.Group(id).playerJoined(newPlayer, session.Players.Count);
            }
            return View("GamePlay", new RockPaperScissorsGamePlayModel() { SessionId = session.Id, PlayerId = playerId, PlayerCount = session.Players.Count, Players = session.Players.Values.ToList() });
        }

    }
}