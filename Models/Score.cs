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
        public int GameID { get; set; }
        [Required]
        [StringLength(128)]
        public string UserID { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime? ScoreDate { get; set; }
        [Required]
        public string score { get; set; }
    }
}