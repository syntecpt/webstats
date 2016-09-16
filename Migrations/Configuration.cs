namespace webstats.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<webstats.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "webstats.Models.ApplicationDbContext";
        }

        protected override void Seed(webstats.Models.ApplicationDbContext context)
        {
            Game floweytale = new Game
            {
                name = "Serial Killer Simulator 2016",
                genre = "Simulation",
                Users = new List<ApplicationUser>()
            };
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);
            }
            //games
            if (!context.Games.Any())
            {
                Game game = new Game
                {
                    name = "Minesweeper",
                    genre = "Puzzle",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "FlappyTale",
                    genre = "Arcade",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "TreeDude",
                    genre = "Arcade",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "RockClimber",
                    genre = "Platform",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "Goal Scoring Revolution",
                    genre = "Sports",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "Hoop This",
                    genre = "Arcade",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "Space Fighters",
                    genre = "Arcade",
                    Users = new List<ApplicationUser>()
                };
                game = new Game
                {
                    name = "Wave Master",
                    genre = "Sports",
                    Users = new List<ApplicationUser>()
                };
            }
            context.SaveChanges();
            //users
            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new ApplicationUserManager(userStore);

                var user = new ApplicationUser
                {
                    Email = "skeleton@mail.com",
                    UserName = "cool_dude_89",
                    Games = new List<Game>()
                };
                userManager.Create(user, "Pass.1");
                user = new ApplicationUser
                {
                    Email = "jack@mail.com",
                    UserName = "JackNightly",
                    Games = new List<Game>()
                };
                userManager.Create(user, "Pass.1");
                user = new ApplicationUser
                {
                    Email = "flower@field.com",
                    UserName = "Flowey",
                    Games = new List<Game>()
                };
                userManager.Create(user, "Pass.1");
                user = new ApplicationUser
                {
                    Email = "wok@sushi.com",
                    UserName = "masterwok",
                    Games = new List<Game>()
                };
                userManager.Create(user, "Pass.1");
                user = new ApplicationUser
                {
                    Email = "user@together.pt",
                    UserName = "User",
                    Games = new List<Game>()
                };
                userManager.Create(user, "Pass.1");
                //add games to users and scores randomly   
                foreach (var ppl in context.Users)
                {
                    Game game1;
                    Game game2;
                    Game game3;
                    Score score;
                    Random rnd = new Random();
                    int randomgame= rnd.Next(1, 9);
                    double randomdate;
                    int randomscore;
                    int scorecount = 0;
                    game1 = context.Games.Find(randomgame);
                    ppl.Games.Add(game1);
                    game1.Users.Add(ppl);
                    while(scorecount<3)
                    {
                        score = new Score();
                        score.GameID = game1.GameID;
                        score.UserID = ppl.Id;
                        randomdate = rnd.Next(90);
                        score.ScoreDate = DateTime.UtcNow.AddDays(-randomdate);
                        randomscore = rnd.Next(500);
                        score.score = randomscore;
                        context.Scores.Add(score);
                        scorecount++;
                    }
                    scorecount = 0;
                    do
                    {
                        randomgame = rnd.Next(1, 9);
                        game2 = context.Games.Find(randomgame);
                    } while (game2.Equals(game1));
                    ppl.Games.Add(game2);
                    game2.Users.Add(ppl);
                    while (scorecount < 3)
                    {
                        score = new Score();
                        score.GameID = game2.GameID;
                        score.UserID = ppl.Id;
                        randomdate = rnd.Next(90);
                        score.ScoreDate = DateTime.UtcNow.AddDays(-randomdate);
                        randomscore = rnd.Next(500);
                        score.score = randomscore;
                        context.Scores.Add(score);
                        scorecount++;
                    }
                    scorecount = 0;
                    do
                    {
                        randomgame = rnd.Next(1, 9);
                        game3 = context.Games.Find(randomgame);
                    } while (game3.Equals(game1) || game3.Equals(game2));
                    ppl.Games.Add(game3);
                    game3.Users.Add(ppl);
                    while (scorecount < 3)
                    {
                        score = new Score();
                        score.GameID = game3.GameID;
                        score.UserID = ppl.Id;
                        randomdate = rnd.Next(90);
                        score.ScoreDate = DateTime.UtcNow.AddDays(-randomdate);
                        randomscore = rnd.Next(500);
                        score.score = randomscore;
                        context.Scores.Add(score);
                        scorecount++;
                    }
                    scorecount = 0;
                    if (ppl.UserName == "Flowey")
                    {
                        if (!ppl.Games.Contains(floweytale))
                        {
                            ppl.Games.Add(floweytale);
                            floweytale.Users.Add(ppl);
                        }
                        score = new Score();
                        score.GameID = floweytale.GameID;
                        score.UserID = ppl.Id;
                        score.ScoreDate = new DateTime(2016, 2, 6);
                        score.score = 9999;
                        context.Scores.Add(score);
                    }
                }
                context.SaveChanges();
                user = new ApplicationUser
                {
                    Email = "admin@mail.com",
                    UserName = "Admin",
                    Games = new List<Game>()
                };
                userManager.Create(user, "Admin.9");
                userManager.AddToRole(user.Id, "Admin");
                context.SaveChanges();
            }
        }
    }
}
