using Org.BouncyCastle.Asn1.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3.Models;

namespace Assign3.Controllers
{
    public class TeacherController : Controller
    {
        //Code Credit: Christine Bittle
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //Get : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }
        //Get : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
           
            return View(NewTeacher);
        }
    }
}