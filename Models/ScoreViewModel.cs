using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webstats.Models
{
    public class ScoreViewModel
    {
        public List<Game> games { get; set; }
        public List<ApplicationUser> users { get; set; }
        public Score score { get; set; }
    }
}