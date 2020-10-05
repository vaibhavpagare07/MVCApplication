using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    /// <summary>
    /// Demo application using knockout JS
    /// </summary>
    public class KnockoutController : Controller
    {
        string connectionString;
        static readonly IEmployeeRepository repository = new EmployeeRepository();

        public ActionResult Index()
        {
            return View();
        }

         [HttpGet]
        public ActionResult EmployeeList()
        {
            //return json data
            return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);
        }


         [HttpGet]
         public ActionResult GetEmployeeData(string id)
         {
             if (string.IsNullOrEmpty(id))
             {
                return Json(null, JsonRequestBehavior.AllowGet);
             }
             var list = repository.GetEmployeeData(id);

             return Json(new { Name = list.Name, Gender = list.Gender, Salary = list.Salary, Department = list.Department }, JsonRequestBehavior.AllowGet);
         }


         [HttpPost]
         public ActionResult SaveEmployeeData(Employee model, bool isAdd)
         {
             if (!ModelState.IsValid)
                 return RedirectToAction("NewEmployee", "Home");

             model = repository.Add(model,isAdd);

             return Json(new { Success = true });
         }
        

        [HttpPost]
        public ActionResult DeleteEmployeeData(string id)
        {
            if(id ==null)
            {
               return RedirectToAction("Index", "Knockout");
            }

            var delete = repository.Delete(id);

           return Json(new { Success = delete, JsonRequestBehavior.AllowGet });
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetDepartments(string id)
        {
            var list = repository.GetDepartmentByID(id);
            return Json(new { 
                ID = list.ID,
                DepartmentName = list.DepartmentName,
                Location = list.Location,
                DepartmentHead = list.DepartmentHead},
                JsonRequestBehavior.AllowGet);
        }

    }
}