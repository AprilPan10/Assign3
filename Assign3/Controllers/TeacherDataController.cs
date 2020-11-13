using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assign3.Models;
using MySql.Data.MySqlClient;
namespace Assign3.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Code Credit: Christine Bittle
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        //This Controller Will access the teachers table of our School database.Non-Deterministic.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet] 
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
          public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
          {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers";
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

           //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherEmployeenumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHiredate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];
                
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.TeacherEmployeenumber = TeacherEmployeenumber;
                NewTeacher.TeacherHiredate = TeacherHiredate;
                NewTeacher.TeacherSalary = TeacherSalary;
               
                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
          }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select teachers.*, classname, classid from teachers left join classes on teachers.teacherid = classes.teacherid where teachers.teacherid = @id";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherEmployeenumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHiredate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];
                string ClassName = ResultSet["classname"].ToString();
                int ClassId =  Convert.ToInt32(ResultSet["classid"]);
                
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFName;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.TeacherEmployeenumber = TeacherEmployeenumber;
                NewTeacher.TeacherHiredate = TeacherHiredate;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.ClassName = ClassName;
                NewTeacher.ClassId = ClassId;



            }
            return NewTeacher;
        }
    }
}
