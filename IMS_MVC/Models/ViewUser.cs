using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS_MVC.Models
{
    public class ViewUser
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int MaxHours { get; set; }
        public int MaxCost { get; set; }
        public string AspNetUserId { get; set; }
        public string DistrictName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }


}