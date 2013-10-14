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
            return View("GamePlay", new RockPaperScissorsGamePlayModel() { SessionId = id, SessionName = name });
        }

    }
}