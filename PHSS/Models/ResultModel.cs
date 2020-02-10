using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class ResultModel
    {
        [Key]
        public int ResultId { get; set; }

        [ForeignKey("Fixtures")]
        public int FixtureId { get; set; }

        public int Team1Score { get; set; }
        public int Team2Score { get; set; }

        public virtual FixtureModel Fixtures { get; set; }
    }
}