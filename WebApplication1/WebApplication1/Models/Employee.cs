using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee
      
    {

     [Key]
     public int EmployeeId { get; set; }

        [FirstNameValidation]
    //[Required(ErrorMessage ="Enter First Name")]
     public string FirstName { get; set; }
    [StringLength(5, ErrorMessage = "Last Name length should not be greater than 5")]
     public string LastName { get; set; }
      
     public int Salary { get; set; }
      
    }

    public class FirstNameValidation:ValidationAttribute            //这是自己放到这的，为了方便看  
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            if(value == null)
            {
                return new ValidationResult("Please Provide First Name");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("First Name should not contain @");
                }
            }
            return ValidationResult.Success;
        }
    }
}