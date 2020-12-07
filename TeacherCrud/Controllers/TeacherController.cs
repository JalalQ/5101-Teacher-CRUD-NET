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


    }
}