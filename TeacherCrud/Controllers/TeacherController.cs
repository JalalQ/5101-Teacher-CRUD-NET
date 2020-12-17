using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherCrud.Models;
using System.Diagnostics;

namespace TeacherCrud.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        /// <summary>
        /// Shows the list of all teachers along with the search box so that user can search for teacher by name.
        /// </summary>
        /// <param name="SearchKey">teacher first/ last name, or both first and last name</param>
        /// <returns></returns>
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }


        //GET : /Teacher/DeleteConfirm/{id}
        /// <summary>
        /// Providing user confirmation that the action can not be undone
        /// </summary>
        /// <param name="id">teacherid</param>
        /// <returns></returns>
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        /// <summary>
        /// Delete a teacher
        /// </summary>
        /// <param name="id">teacherid</param>
        /// <returns>List of teachers after teacher with the specified id has been deleted.</returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }


        //GET : /Teacher/New
        /// <summary>
        /// Form to create new teacher.
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/AjaxNew
        /// <summary>
        /// Form to create a new teacher using Ajax
        /// </summary>
        public ActionResult AjaxNew()
        {
            return View();
        }

        //POST : /Teacher/Create
        /// <summary>
        /// Creates a new teacher
        /// </summary>
        /// <param name="teacherfname">Teacher's first name</param>
        /// <param name="teacherlname">Teacher's last name</param>
        /// <param name="employeenumber">Teacer's employee Number</param>
        /// <param name="hiredate">The date the teacher was hired</param>
        /// <param name="salary">Salary</param>
        /// <returns>To the list of the teachers if there is no error otherwise to error page</returns>
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            //C# Server Side Validation - If the user does not input the name and employeenumber
            //then the user is directed to a page in which the user is informed about the missing values.
            if ((teacherfname != "") && (teacherlname!= "") && (employeenumber != ""))
            {

                Debug.WriteLine("I have accessed the Create Method!");
                Debug.WriteLine(teacherfname);
                Debug.WriteLine(teacherlname);
                Debug.WriteLine(employeenumber);
                Debug.WriteLine(hiredate);
                Debug.WriteLine(salary);

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;

                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);
                return RedirectToAction("List");
            }

              else //redirect to a page displaying the error.
               {
                   return RedirectToAction("InputError");
               }


        }

        //GET : /Teacher/New
        /// <summary>
        /// displays the error using server side form validation.
        /// </summary>
        /// <returns>Information about the missing field values.</returns>
        public ActionResult InputError()
        {
            return View();
        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the Teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Dispay the author which will be updated using the Ajax method
        /// </summary>
        /// <param name="id">teacherid</param>
        /// <returns>Ajax Update Page for a specified teacherid</returns>
        public ActionResult AjaxUpdate(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing Teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated Teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the Teacher</param>
        /// <param name="TeacherLname">The updated last name of the Teacher</param>
        /// <param name="EmployeeNumber">The employee number of the Teacher.</param>
        /// <param name="hiredate">The updated email of the Teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the Teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Chris",
        ///	"TeacherLname":"Yang",
        ///	"EmployeeNumber":"Loves Coding!",
        ///	"HireDate":"10/10/2020",
        ///	"salary" : 9,
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;
            NewTeacher.employeenumber = employeenumber;
            NewTeacher.hiredate = hiredate;
            NewTeacher.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, NewTeacher);

            return RedirectToAction("Show/" + id);
        }

    }
}