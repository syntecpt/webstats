using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webstats.Models;

namespace webstats.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(new GameViewModel
            {
                games = db.Games.ToList(),
                user = db.Users.Find(User.Identity.GetUserId())
            });
        }

        public ActionResult Search(string Id)
        {
            if (Id == null || Id == "")
            {
                return RedirectToAction("Index");
            }
            List<Game> games = new List<Game>();
            foreach (var game in db.Games)
            {
                if (game.name.ToUpper().Contains(Id.ToUpper()))
                {
                    games.Add(game);
                }
            }
            return View(new GameViewModel
            {
                games = games,
                user = db.Users.Find(User.Identity.GetUserId())
            });
        }

        // GET: UserGames
        [Authorize]
        public ActionResult Mine()
        {
            return View(new GameViewModel
            {
                games = db.Games.ToList(),
                user = db.Users.Find(User.Identity.GetUserId())
        });
        }

        // POST: Addgame
        [Authorize]
        public ActionResult Addgame(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            var userid = User.Identity.GetUserId();
            foreach (var item in game.Users)
            {
                if (item.Id.Equals(userid)) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            db.Users.Find(User.Identity.GetUserId()).Games.Add(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            List<Score> scores = new List<Score>();
            foreach (var score in db.Scores)
            {
                if (score.GameID.Equals(game.GameID))
                {
                    scores.Add(score);
                }
            }
            scores = scores.OrderByDescending(x => x.score).ToList();
            if (scores.Count > 4)
            {
                scores.RemoveRange(5, scores.Count - 5);
            }
            return View(new GameScoreViewModel
            {
                game = game,
                users = db.Users.ToList(),
                scores = scores
            });
        }

        // GET: Games/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,name,genre")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "GameID,name,genre")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
