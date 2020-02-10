using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class PlayerModel
    {
        [Key]
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ForeNames { get; set; }
        public string Position { get; set; }
        public DateTime DOB { get; set; }


        public virtual TeamModel Team { get; set; }
    }
}
