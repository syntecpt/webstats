using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webstats.Models
{
    public class Game
    {
        public int GameID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string genre { get; set; }
    }
}