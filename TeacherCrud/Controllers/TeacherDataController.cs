using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeacherCrud.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;

//Code design adopted from:
//https://github.com/christinebittle/BlogProject_5/blob/master/BlogProject/Controllers/TeacherDataController.cs

namespace TeacherCrud.Controllers
{
    public class TeacherDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Connects to the database.
        /// Fetches all the data from the teachers table
        /// </summary>
        /// <returns>Data Fetched from the database (Array of Teacher's record)</returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL query to select all the rows from the teachers table of School database.
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> teachers = new List<Teacher> { };

            // read each of the row line by line using a while loop which loops until the end of line.
            while (ResultSet.Read())
            {

                int teacherid = (int)ResultSet["teacherid"];
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherid = teacherid;
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;

                //Object added to the array.
                teachers.Add(NewTeacher);

            }

            Conn.Close();

            return teachers;

        }

        /// <summary>
        /// Finds a teacher based on the teacherid using SQL statement
        /// </summary>
        /// <param name="id">teacherid in the teacher table of the database</param>
        /// <returns>The full information about the teacher.</returns>
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
            cmd.CommandText = "Select * from teachers where teacherid =@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                int teacherid = (int)ResultSet["teacherid"];
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                string employeenumber = ResultSet["employeenumber"].ToString();
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];

                NewTeacher.teacherid = teacherid;
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;

            }

            Conn.Close();

            return NewTeacher;
        }


        /// <summary>
        /// Deletes a Teacher from the connected MySQL Database if the ID of that Teacher exists. Maintains referential integrity.
        /// </summary>
        /// <param name="id">The ID of the Teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where Teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //To maintain referential integrity, those classes which are taught by a teacher should also be deleted.
            cmd.CommandText = "Delete from classes where classes.Teacherid=@id";
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }


        /// <summary>
        /// Adds a Teacher to the MySQL Database using AJAX approach.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// 	"teacherfname": "Tom",
		///     "teacherlname": "James",
		///     "employeenumber": "T098",
		///     "hiredate": "2020-27-03",
		///     "salary": 43
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherfname,@teacherlname,@employeenumber, @hiredate, @salary)";
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }



        /// <summary>
        /// Updates an Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the Teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/208 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        /// 
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Debug.WriteLine(TeacherInfo.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            // cmd.CommandText = "update authors set authorfname=@AuthorFname, authorlname=@AuthorLname, authorbio=@AuthorBio, authoremail=@AuthorEmail  where authorid=@AuthorId";
            cmd.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, employeenumber=@employeenumber, hiredate=@hiredate, salary=@salary where teacherid=@teacherid";
            
            
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", TeacherInfo.hiredate);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.salary);
            cmd.Parameters.AddWithValue("@teacherid", id); 

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }


    }
}
