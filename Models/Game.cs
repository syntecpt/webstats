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
        [Display(Name = "Game Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public string genre { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}