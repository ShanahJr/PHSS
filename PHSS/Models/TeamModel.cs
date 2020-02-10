using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class TeamModel
    {
        public TeamModel()
        {
            this.Players = new HashSet<PlayerModel>();
            //this.TeamLogs = new HashSet<LogModel>();

        }

        [Key]
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        //public LogModel TeamLog { get; set; }


        [ForeignKey("School")]
        public int SchoolId { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }


        [ForeignKey("AgeGroup")]
        public int AgeGroupId { get; set; }

        //public virtual ICollection<LogModel> TeamLogs { get; set; }
        public virtual SchoolModel School { get; set; }
        public virtual DivisionModel Division { get; set; }
        public virtual AgeGroupModel AgeGroup { get; set; }
        public virtual ICollection<PlayerModel> Players { get; set; }
    }
}