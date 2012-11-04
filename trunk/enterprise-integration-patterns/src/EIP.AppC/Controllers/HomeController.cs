using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EIP.AppC.Models;
using EIP.CanonicalModel.Events;

namespace EIP.AppC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(TestModel test)
		{
			// Business logic with internal model of Test
			// testService.Create(test);

			TestOccurred testEvent = new TestOccurred();
			testEvent.Text = test.Name;

			ServiceBusProvider.Bus.Publish(testEvent);
			
			return View();
		}

    }
}
