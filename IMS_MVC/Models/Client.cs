using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descriptive { get; set; }
        [Required]
        public int DistrictId { get; set; }
    }
}