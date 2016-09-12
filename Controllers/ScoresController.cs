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
    public class ScoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Scores
        public ActionResult Index()
        {
            return View(new ScoreViewModel
            {
                games = db.Games.ToList(),
                users = db.Users.ToList(),
                scores = db.Scores.ToList()
            });
        }

        // GET: Scores/Details/5
        [Route("score/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(new ScoreDetailsViewModel
            {
                games = db.Games.ToList(),
                users = db.Users.ToList(),
                score = score
            });
        }

        // GET: Scores/Submit
        public ActionResult Submit()
        {
            return View(new CreateScoreViewModel
            {
                games = db.Games.ToList(),
                users = db.Users.ToList()
            });
        }

        // POST: Scores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "ScoreID,GameID,UserID,ScoreDate,score")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Scores.Add(score);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.GameID = new SelectList(db.Games, "GameID", "name", score.GameID);
            ViewBag.GameID = new SelectList(db.Games, "GameID", "name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View(score);
        }

        // GET: Scores/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(new ScoreDetailsViewModel
            {
                games = db.Games.ToList(),
                users = db.Users.ToList(),
                score = score
            });
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ScoreID,GameID,UserID,ScoreDate,score")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Entry(score).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new ScoreDetailsViewModel
            {
                games = db.Games.ToList(),
                users = db.Users.ToList(),
                score = score
            });
        }

        // GET: Scores/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(new ScoreDetailsViewModel
            {
                games = db.Games.ToList(),
                users = db.Users.ToList(),
                score = score
            });
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Score score = db.Scores.Find(id);
            db.Scores.Remove(score);
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
