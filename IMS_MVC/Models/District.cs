using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class District
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }
    }
}