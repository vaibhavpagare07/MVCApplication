using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApplication.Models
{
    interface IEmployeeRepository
    {

        IEnumerable<Employee> GetAll();


        Employee GetEmployeeData(string id);


        Employee Add(Employee item, bool isAdd);


        //bool Update(Employee id);


        bool Delete(string id);

        //IEnumerable<Departments> GetAllDepartments();

        Departments GetDepartmentByID(string departname);
    }
}
