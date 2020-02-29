using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class FixtureModel
    {
        [Key]
        public int FixtureId { get; set; }

        public string FixtureName { get; set; }

        [ForeignKey("Team1")]
        [Column(Order = 1)]
        [Display(Name = "Home Team")]
        public int Team1Id { get; set; }

        [ForeignKey("Team2")]
        [Column(Order = 2)]
        [Display(Name = "Away Team")]
        public int Team2Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Matchdate { get; set; }

        public string MatchLocation { get; set; }

        public bool Executed { get; set; }

        public virtual TeamModel Team1 { get; set; }
        public virtual TeamModel Team2 { get; set; }
    }
}