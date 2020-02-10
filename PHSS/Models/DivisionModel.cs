using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class DivisionModel
    {
        [Key]
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string Description { get; set; }
    }
}