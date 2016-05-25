using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name="Client Name")]
        public string Name { get; set; }
        public string Descriptive { get; set; }
        [Required]
        
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District {get; set;}
    }
}