using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class SubmissionModel
    {
        [Required(ErrorMessage = "ID is Required")]
        [DisplayName("ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(100, ErrorMessage = "Name can not be longer than 100 characters")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }
        
        [DisplayName("Salary")]
        public int Salary { get; set; }

       
        [DisplayName("Department Name")]
        public string Department { get; set; }

    }

    public class SearchModel
    {
        [Required(ErrorMessage = "ID is Required")]
        [DisplayName("ID")]
        public int ID { get; set; }
    }

    public class CodersX
    {
        
    }
}