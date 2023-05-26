using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorProject.Models
{
    public class Teacher_Model
    {
        [Required]
        [Display(Name = "Teacher ID")]
        public string t_id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string f_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string l_name { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }
        [Required]
        [Display(Name = "Phone No")]
        public string phone_no { get; set; }
        [Required]
        [Display(Name = "City")]
        public string city { get; set; }
        [Required]
        [Display(Name = "Class")]
        public string class_t { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string subject { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }

       
        public string drop { get; set; }



    }
}