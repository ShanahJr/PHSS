using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHSS.Models
{
    public class AgeGroupModel
    {
        [Key]
        public int AgeGroupID { get; set; }
        public string AgeGroupName { get; set; }
    }
}