using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class IntType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name= "Intervention Type")]
        public string Name { get; set; }
        [Required]
        public int Labour { get; set; }
        [Required]
        public int Cost { get; set; }
    }
}