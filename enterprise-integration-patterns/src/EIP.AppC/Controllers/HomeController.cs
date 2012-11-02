using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIP.AppC.Models;
using EIP.CanonicalDomain.Events;
using EIP.CanonicalModels;

namespace EIP.AppC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(AppCSpecificEmployeeModel employee)
		{
			// Business logic with internal model of Employee
			// employeeService.Create(employee);

			EmployeeHired employeeHiredEvent = new EmployeeHired();
			employeeHiredEvent.Employee = new Employee // adaptation between models...
			{
				Department = employee.Area,
				Id = employee.Id.ToString(),
				Name = employee.Name
			};

			ServiceBusProvider.Bus.Publish(employeeHiredEvent);
			
			return View();
		}

    }
}
