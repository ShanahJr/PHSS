using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class SchoolModel
    {
        [Key]
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}