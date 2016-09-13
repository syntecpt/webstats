using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webstats.Models
{
    public class GameViewModel
    {
        public List<Game> games { get; set; }
        public ApplicationUser user { get; set; }
    }
}