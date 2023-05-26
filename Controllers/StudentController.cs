using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTutorProject.Models;

namespace OnlineTutorProject.Controllers
{
    public class StudentController : Controller
    {
        Online_TutorEntities db = new Online_TutorEntities();
        [HttpGet]
        public ActionResult StudentSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentSignUp(Student_Model sm)
        {
            try
            {
                dr std_db = new dr();
                std_db.std_id = sm.std_id;
                std_db.fname = sm.f_name;
                std_db.lname = sm.l_name;
                std_db.gender = sm.gender;
                std_db.phone_no = sm.phone_no;
                std_db.classlevel = sm.class_level;
                std_db.password = sm.password;

                db.drs.Add(std_db);
                db.SaveChanges();
                return RedirectToAction("StudentSignIn");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult StudentSignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentSignIn(Student_Model sm)
        {
            try
            {
                var v = db.drs.Select(s => s).Where(s => s.std_id == sm.std_id && s.password == sm.password);
                foreach (var item in v)
                {
                    Session["std_id"] = sm.std_id;
                  
                    Session["classLevel"] = item.classlevel;
            

                    return RedirectToAction("StudentRequestTeacher");
                }
            }
            catch (Exception)
            {

                return View();
            }

            return View();
        }
        public ActionResult StudentRequestTeacher()
        {
            string classLevel = Session["classLevel"].ToString();
            List<Teacher_Model> lst = new List<Teacher_Model>();
            try
            {
                var v = db.tutors.Select(s => s).Where(s => s.class_t == classLevel);


                foreach (var item in v)
                {
                    Teacher_Model tm = new Teacher_Model();
                    tm.t_id = item.tid.ToString();
                    tm.f_name = item.fname;
                    tm.l_name = item.lname;
                    tm.gender = item.gender;
                    tm.class_t = item.class_t;
                    tm.city = item.city;
                    tm.phone_no = item.phone_no;
                    tm.subject = item.subject;
                    lst.Add(tm);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return View(lst);
        }
        [HttpPost]
        public ActionResult StudentRequestTeacher(string City,string Subject,string Classlevel)
        {

            List<Teacher_Model> lst = new List<Teacher_Model>();
            try
            {
                // string teacher_id = Session["t_id"].ToString();
                var v = db.tutors.Select(s => s).Where(s => s.city == City || s.subject == Subject || s.class_t == Classlevel) ;
                foreach (var item in v)
                {
                    Teacher_Model tm = new Teacher_Model();
                    tm.t_id = item.tid.ToString();
                    tm.f_name = item.fname;
                    tm.l_name = item.lname;
                    tm.gender = item.gender;
                    tm.class_t = item.class_t;
                    tm.city = item.city;
                    tm.phone_no = item.phone_no;
                    tm.subject = item.subject;
                    lst.Add(tm);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return View(lst);
        }

        public ActionResult SendRequest(string t_id)
        {
            

            int std_id = int.Parse(Session["std_id"].ToString());
            apply a = new apply();
            var v = db.applies.Select(s => s).Where(s => s.std_id == std_id && s.tid.ToString() == t_id);
            foreach (var item in v)
            {
                Session["error"] = "You Have Already Apply for this.";
                return RedirectToAction("StudentRequestTeacher");
               
            }
           
                a.tid = int.Parse(t_id);
                a.std_id = std_id;

                db.applies.Add(a);
                db.SaveChanges();
                return RedirectToAction("CheckRequest");
        }
        [HttpGet]
        public ActionResult CheckRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckRequest(string Teacher_ID,string sub)
        {
            Apply_Model a = new Apply_Model();
            string Student_ID = Session["std_id"].ToString();
            var v = db.applies.Select(s => s).Where(s=>s.std_id.ToString()== Student_ID && s.tid.ToString()== Teacher_ID && s.tutor.subject==sub);
            foreach (var item in v)
            {
                a.std_id = item.std_id.ToString();
                a.tid = item.tid.ToString();
                a.slot = item.slot;
                a.tutor_address = item.tutor_address;
                a.approval_status = Convert.ToInt32(item.aproval_status);
                if (a.tutor_address!=null)
                {
                    ViewBag.approval_status = "Approved";
                }
                else
                    ViewBag.approval_status = "NotApproved";
            }
            return View(a);
        }
    }
}