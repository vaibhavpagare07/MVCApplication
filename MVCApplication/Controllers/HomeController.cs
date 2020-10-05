using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {

        #region Strings

        string connectionString = "Data source = dev040; Database = Employee; user id = sa; password = admin_1234 ";

        #endregion



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Submission()
        {
            string Query = "select e.ID, Name, Salary , DepartmentName from tblEmployee1 as e JOIN tblDepartment as d on e.DepartmentID = d.ID";

            var list = new List<SubmissionModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var ele = new SubmissionModel { ID = Convert.ToInt16(dr[0]), Name = dr[1].ToString(), Salary = Convert.ToInt32(dr[2]), Department = dr[3].ToString() };
                    list.Add(ele);
                }

            }

            return View("SubmissionViewList", list);
        }


        public ActionResult NewEmployee()
        {
            return View("Submission");
        }


        [HttpPost]
        public ActionResult Submission(SubmissionModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("NewEmployee", "Home");


            string query = "Insert into tblEmployee1(ID, Name, Gender, Salary, DepartmentId  )" +
                            "Values (@ID , @Name, @Gender, @Salary, @DepartmentId)";
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", model.ID);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Gender", model.Gender);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32( model.Department ));


                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index", ex.Message);
                    }
                }

            }
            return RedirectToAction("Submission", "Home");

        }


        //--------------------------------------------------------------------------- Search Fuctionality

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            string query = "Select Name , Gender, salary , DepartmentName from tblEmployee1 join tblDepartment on tblEmployee1.DepartmentId = tblDepartment.ID where tblEmployee1.ID =  " + model.ID;
            var list = new List<SubmissionModel>();

            if (null != model.ID)
            {
                using (SqlConnection sqlcon = new SqlConnection(connectionString))
                {
                    sqlcon.Open();
                    SqlCommand command = new SqlCommand(query, sqlcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var ele = new SubmissionModel { Name = reader[0].ToString(), Gender = reader[1].ToString(), Salary = Convert.ToInt32(reader[2]), Department = reader[3].ToString() };
                        list.Add(ele);
                    }

                }
            }

            if (list.Count == 0)
            {
                ViewBag.Message = string.Format("Sorry, We Don't have any record of this ID : {0}", model.ID);
            }
            return View("_SearchView", list);

        }


        public ActionResult SearchByName(string id)
        {
            return View();
        }


        //------------------------------------------------------------------

        // Funtion Edit 

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Submission", "Home");
            }

            string query = "select tblEmployee1.ID , Name , Gender, DepartmentName, salary from tblEmployee1 JOIN tblDepartment on tblEmployee1.DepartmentId = tblDepartment.ID where tblEmployee1.ID = " + id;

            var model = new SubmissionModel();
            var list = new SubmissionModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand Commnad = new SqlCommand(query, connection);
                SqlDataReader dr = Commnad.ExecuteReader();

                while (dr.Read())
                {
                    var ele = new SubmissionModel 
                                { 
                                    ID = Convert.ToInt16(dr[0]),
                                    Name = dr[1].ToString(),
                                    Gender = dr[2].ToString(),
                                    Department = dr[3].ToString(), 
                                    Salary = Convert.ToInt32(dr[4])
                                };
                    list = ele;
                }
            }

            return View("edit", list);
        }

        [HttpPost]
        public ActionResult Edit(SubmissionModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("NewEmployee", "Home");

            string query = "Update tblEmployee1 Set Name = @Name, Gender=@Gender ,Salary=@Salary,DepartmentId= @DepartmentId Where  ID = @ID";
            //string qury = "Update tblEmployee1 Set Name =  + model.Name + @Gender = + model.Gender + @salary = + model.Salary + @DepartmentID =  + model.DepartmentId + where @ID =  + model.ID ";


            using (SqlConnection con = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@ID", model.ID);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Gender", model.Gender);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32 ( model.Department ) );


                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index", ex.Message);
                    }
                }
                return RedirectToAction("Submission", "Home");
            }
        }



        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Submission", "Home");
            }

            string query = "select tblEmployee1.ID , Name , Gender, DepartmentName, salary from tblEmployee1 JOIN tblDepartment on tblEmployee1.DepartmentId = tblDepartment.ID where tblEmployee.ID = " + id;

            var model = new SubmissionModel();
            var list = new SubmissionModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand Commnad = new SqlCommand(query, connection);
                SqlDataReader dr = Commnad.ExecuteReader();

                while (dr.Read())
                {
                    var ele = new SubmissionModel
                    {
                        ID = Convert.ToInt16(dr[0]),
                        Name = dr[1].ToString(),
                        Gender = dr[2].ToString(),
                        Department = dr[3].ToString(),
                        Salary = Convert.ToInt32(dr[4])
                    };
                    list = ele;
                }
            }

            return View("_SearchView", list);
        }
    }
}

        