using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTutorProject.Models;
using System.Web.Mvc;

namespace OnlineTutorProject.Controllers
{
    public class TeacherController : Controller
    {
        Online_TutorEntities db = new Online_TutorEntities();

        [HttpGet]
        public ActionResult TeacherSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherSignUp(Teacher_Model tm)
        {
            try
            {
                tutor t_db = new tutor();
                t_db.tid = int.Parse(tm.t_id);
                t_db.fname = tm.f_name;
                t_db.lname = tm.l_name;
                t_db.gender = tm.gender;
                t_db.phone_no = tm.phone_no;
                t_db.city = tm.city;
                t_db.class_t = tm.class_t;
                t_db.subject = tm.subject;
                t_db.password = tm.password;
                db.tutors.Add(t_db);
                db.SaveChanges();
                return RedirectToAction("TeacherSignIn");

            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpGet]
        public ActionResult TeacherSignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherSignIn(Teacher_Model tm)
        {
            try
            {
                var v = db.tutors.Select(s => s).Where(s=>s.tid==int.Parse(tm.t_id)&&s.password==tm.password);
                if (v != null) 
                {
                    Session["t_id"] = tm.t_id;

                   return  RedirectToAction("StudentRequest");
                }


            }
            catch (Exception)
            {

                return View();
            }

            return View();
        }
        [HttpGet]
        public ActionResult StudentRequest()
        {

            List<Student_Model> lst = new List<Student_Model>();
            try
            {
                int teacher_id = int.Parse(Session["t_id"].ToString());
                var v = db.applies.Select(s => s).Where(s => s.tid == teacher_id && s.tutor_address == null);

                foreach (var item in v)
                {
                    Student_Model sm = new Student_Model();
                    sm.std_id = item.std_id.Value;
                    sm.f_name = item.dr.fname;
                    sm.l_name = item.dr.lname;
                    sm.gender = item.dr.gender;
                    sm.phone_no = item.dr.phone_no;
                    lst.Add(sm);
                }

            }
            catch (Exception)
            {

                return View();
            }

            return View(lst);
          //  return View();

        }


        public ActionResult AcceptRequest(string std_id)
        {
            Session["std_id"] = std_id;

            return View();
        }
        [HttpPost]
        public ActionResult AcceptRequest(Apply_Model am)
        {

            int teacher_id = int.Parse(Session["t_id"].ToString());
            int std_id = int.Parse(Session["std_id"].ToString());
            var v = db.applies.Where(s => s.tid == teacher_id && s.std_id == std_id);
            
            foreach (var item in v)
            {
                item.tutor_address = am.tutor_address;               
            }

            db.SaveChanges();
            return RedirectToAction("StudentRequest");
        }
    }
}