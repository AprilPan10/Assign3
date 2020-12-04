using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assign3.Models
{
    public class Teacher
    {
        //The following fields define an Teacher
        public int TeacherId;
        //try to do validation
        //[Required]
        //public int TeacherFname { get; set; }
        //[Required]
        //public int TeacherLname { get; set; }
        public string TeacherFname;
        public string TeacherLname;
        public string TeacherEmployeenumber;
        public DateTime TeacherHiredate;
        public string TeacherSalary;
        public int ClassId;
        public string ClassName;

        public Teacher() { }
       
    }
}