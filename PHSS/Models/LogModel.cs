using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class LogModel
    {
        [Key]
        [ForeignKey("Team")]
        //[Required]
        //[Column(Order = 1)]
        public int TeamId { get; set; }
        //public int LogId { get; set; }

        public int Played { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public int LogStanding { get; set; }

        //[Key]
        [ForeignKey("Season")]
        //[Required]
        //[Column(Order = 2)]
        public int SeasonId { get; set; }

        public virtual SeasonModel Season { get; set; }
        public virtual TeamModel Team { get; set; }

        //public static implicit operator LogModel(TeamModel v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}