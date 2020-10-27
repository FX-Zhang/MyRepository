using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Data_Access_Layer;

namespace WebApplication1.Models
{
    public class EmployeeBusinessLayer
     {

        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();                  //调用 ToList() 会立刻查询并保存结果, 而不会等到迭代时才查询. 作用和 lazy load 是对立的.  linq语句
        }

        public Employee SaveEmployee(Employee e)                //将表单输入的值保存到数据库
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
           
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if(u.UserName == "Admin"&&u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if(u.UserName == "Sukesh" && u.Password == "Sukesh")
            {
                return UserStatus.AuthenticatedUser;
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }

        }

        //public Boolean IsValidUser(UserDetails u)
        //{
        //    if (u.UserName == "Admin" && u.Password == "Admin")
        //        return true;
        //    else
        //        return false;
        //}


        // public List<Employee> GetEmployees()
        // {
        //     List<Employee> employees = new List<Employee>();
        //    Employee emp = new Employee();
        //    emp.FirstName = "johnson";
        //     emp.LastName = " fernandes";
        //     emp.Salary = 14000;
        //    employees.Add(emp);

        //    emp = new Employee();
        //    emp.FirstName = "michael";
        //    emp.LastName = "jackson";
        //    emp.Salary = 16000;
        //    employees.Add(emp);

        //    emp = new Employee();
        //    emp.FirstName = "robert";
        //    emp.LastName = " pattinson";
        //    emp.Salary = 20000;
        //    employees.Add(emp);

        //    return employees;
        //}


    }
}