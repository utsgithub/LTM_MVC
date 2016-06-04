using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace IMS_MVC.Models
{
    public class IntInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Intervention Type")]
        public int IntTypeId { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        [Display(Name = "Labour")]
        public int? SetLabour { get; set; }
        [Display(Name = "Cost")]
        public int? SetCost { get; set; }
        public string AspNetUserId { get; set; }
        public int UserId { get; set; }
        [Display(Name ="Intervention Date")]
        public DateTime? IntDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public int? Reamaining { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? ApprovedByUserId { get; set; }

        [ForeignKey("IntTypeId")]
        public virtual IntType IntType { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}