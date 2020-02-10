using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class SeasonModel
    {
        [Key]
        public int SeasonId { get; set; }
        public string SeasonName { get; set; }

    }
}