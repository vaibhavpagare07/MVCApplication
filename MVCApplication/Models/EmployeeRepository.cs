using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {

        string connectionString = "Data Source=DESKTOP-M9P5LU6;Initial Catalog=EmployeeDB;Integrated Security=True ";

        private List<Employee> emplist = new List<Employee>();

        public IEnumerable<Employee> GetAll()
        {
            string Query = "select e.ID, Name,Gender, Salary , DepartmentName from tblEmployee as e JOIN tblDepartment as d on e.DepartmentID = d.ID order by Name";

            //var list = new List<SubmissionModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var ele = new Employee { 
                        ID = Convert.ToInt16(dr[0]),
                        Name = dr[1].ToString(),
                        Gender = dr[2].ToString(),
                        Salary = Convert.ToInt32(dr[3]),
                        Department = dr[4].ToString()
                    };
                    emplist.Add(ele);
                }
            }
            return emplist;
        }

        public Employee GetEmployeeData(string id)
        {
            string query = "select tblEmployee.ID , Name , Gender, DepartmentName, salary from tblEmployee JOIN tblDepartment on tblEmployee.DepartmentId = tblDepartment.ID where tblEmployee.ID = " + id;

            var list = new Employee();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand Commnad = new SqlCommand(query, connection);
                SqlDataReader dr = Commnad.ExecuteReader();

                while (dr.Read())
                {
                    var ele = new Employee
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
            return list;
        }

        public Employee Add(Employee model, bool isAdd)
        {


            if (isAdd)
            {
                string insertquery = "Insert into tblEmployee(ID, Name, Gender, Salary, DepartmentId  )  Values (@ID , @Name, @Gender, @Salary, @DepartmentId)";
                                    

                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand(insertquery, con))
                    {
                       // cmd.Parameters.AddWithValue("@ID", model.ID);
                        cmd.Parameters.AddWithValue("@Name", model.Name);
                        cmd.Parameters.AddWithValue("@Gender", model.Gender);
                        cmd.Parameters.AddWithValue("@Salary", model.Salary);
                        cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32(model.Department));

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
               return model;
            }
            string query = "Update tblEmployee Set Name = @Name, Gender=@Gender ,Salary=@Salary,DepartmentId= @DepartmentId Where  ID = @ID";
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                   // cmd.Parameters.AddWithValue("@ID", model.ID);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Gender", model.Gender);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@DepartmentId", Convert.ToInt32(model.Department));


                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new NotImplementedException();
                    }
                }


            }
            return model;
             
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            string query = " Delete from tblEmployee where ID = " + id;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            return true;
        }


        public Departments GetDepartmentByID(string departname)
        {
            string query = "SELECT  ID,DepartmentName,Location,DepartmentHead FROM tblDepartment where DepartmentName like '" +departname+ "%'";

            var deparmentList = new Departments();

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter( query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

               foreach(DataRow dr in ds.Tables[0].Rows)
               {
                   deparmentList =  new Departments
                     {      
                        ID = dr[0].ToString(),
                        DepartmentName = dr[1].ToString(),
                        Location = dr[2].ToString(),
                        DepartmentHead = dr[3].ToString()
                     };  
               }
            }

            return deparmentList;
        }
    }
}