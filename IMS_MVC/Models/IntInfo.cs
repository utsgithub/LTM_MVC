using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class IntInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IntTypeId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int SetLabour { get; set; }
        [Required]
        public int SetCost { get; set; }
        [Required]
        public string AspNetUserId { get; set; }
        [Display(Name ="Intervention Date")]
        public DateTime IntDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public int Reamaining { get; set; }
        public DateTime VisitDate { get; set; }
        public int ApprovedByUserId { get; set; }
    }
}