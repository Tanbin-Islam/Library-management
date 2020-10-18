using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMamagementSystem.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public System.DateTime DOB { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}