using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorProject.Models
{
    public class Apply_Model
    {
        [Display(Name = "Student ID")]
        public string std_id { get; set; }
        [Display(Name = "Teacher ID")]
        public string tid { get; set; }
        [Display(Name = "Shift")]
        public string slot { get; set; }
        [Display(Name = "Tutor Address")]
        public string tutor_address { get; set; }
        [Display(Name = "Current Status")]
        public int approval_status { get; set; }
    }
}