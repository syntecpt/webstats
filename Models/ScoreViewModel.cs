using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webstats.Models
{
    public class CreateScoreViewModel
    {
        public List<Game> games { get; set; }
        public List<ApplicationUser> users { get; set; }
        public Score score { get; set; }
    }

    public class ScoreViewModel
    {
        public List<Game> games { get; set; }
        public List<ApplicationUser> users { get; set; }
        public List<Score> scores { get; set; }
    }

    public class ScoreDetailsViewModel
    {
        public List<Game> games { get; set; }
        public List<ApplicationUser> users { get; set; }
        public Score score { get; set; }
    }
}