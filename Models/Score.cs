using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webstats.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        [Required]
        [Display(Name = "Game")]
        public int GameID { get; set; }
        [Required]
        [StringLength(128)]
        [Display(Name = "User")]
        public string UserID { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date")]
        public DateTime? ScoreDate { get; set; }
        [Required]
        [Display(Name = "Score")]
        public int score { get; set; }
    }
}