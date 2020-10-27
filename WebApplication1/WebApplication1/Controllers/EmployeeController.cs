using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.ViewModels;
namespace WebApplication1.Controllers
{
  
     public class EmployeeController : Controller
     {
         public string GetString()
         {
            return "Hello World is old now. It’s time for wassup bro ;)"; 
         }

        [Authorize]
        public ActionResult Index()
     {
         EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

         employeeListViewModel.UserName = User.Identity.Name;

            employeeListViewModel.FooterData  = new FooterViewModel();         //不要忘记写这个，不然无法调用.Company
            employeeListViewModel.FooterData.Company = "StepByStepSchools";
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
      
         EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
         List<Employee> employees = empBal.GetEmployees();
      
         List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
      
        foreach (Employee emp in employees)
        {
            EmployeeViewModel empViewModel = new EmployeeViewModel();
            empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
            empViewModel.Salary = emp.Salary.ToString("C");
            if (emp.Salary > 15000)
            {
                empViewModel.SalaryColor = "yellow";
            }
            else
            {
                empViewModel.SalaryColor = "green";
            }
           empViewModels.Add(empViewModel);
        }
        employeeListViewModel.Employees = empViewModels;
        //employeeListViewModel.UserName = "Admin";
        return View("Index", employeeListViewModel);   //一览列表：listViewModel 包含viewModel集合的(变量)对象Employees和其他的viewModel，那个
                                                        //对象是为了方便在视图里 foreach  in。

        }

        [AdminFiter]
        public ActionResult AddNew() 
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.Company = "StepByStepSchools";
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            employeeListViewModel.UserName = User.Identity.Name;
            return View("CreateEmployee", employeeListViewModel);
            //return View("CreateEmployee",new CreateEmployeeViewModel());
        }

       
        [AdminFiter]
        public ActionResult SaveEmployee(Employee e,string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");

                    }
                    else
                    //return View("CreateEmployee");
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        vm.Salary = (e.Salary == 0)? ModelState["Salary"].Value.AttemptedValue : e.Salary.ToString("C");

                        vm.FooterData = new FooterViewModel();
                        vm.FooterData.Company = "StepByStepSchools";
                        vm.FooterData.Year = DateTime.Now.Year.ToString();
                        vm.UserName = User.Identity.Name;

                        return View("CreateEmployee", vm);
                    }
                   
                case "Cancel":
                   return RedirectToAction("Index");
            }
            return new EmptyResult();  

         }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");          //呈现一个分部视图
            }
            else
            {
                return new EmptyResult();
            }
        }
}
    
}