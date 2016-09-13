using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webstats.Models
{
    public class GameScoreViewModel
    {
        public Game game { get; set; }
        public List<Score> scores { get; set; }
        public List<ApplicationUser> users { get; set; }
    }
}