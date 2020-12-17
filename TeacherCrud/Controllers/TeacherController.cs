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
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }


        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/AjaxNew
        public ActionResult AjaxNew()
        {
            return View();
        }

        //POST : /Teacher/Create
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
        ///	"TeacherFname":"Christine",
        ///	"TeacherLname":"Bittle",
        ///	"TeacherBio":"Loves Coding!",
        ///	"TeacherEmail":"christine@test.ca"
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