using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webstats.Models
{
    public class UserGames
    {
        public int GameId { get; set; }
        [Required]
        [StringLength(128)]
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}