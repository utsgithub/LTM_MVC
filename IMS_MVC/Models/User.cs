using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int MaxHours { get; set; }
        [Required]
        public int MaxCost { get; set; }
        [Required]
        public string AspNetUserId { get; set; }
        [Display(Name ="Created By")]
        public string UserName { get; set; }
        [Display(Name ="Role")]
        public string UserType { get; set; }
    }
}