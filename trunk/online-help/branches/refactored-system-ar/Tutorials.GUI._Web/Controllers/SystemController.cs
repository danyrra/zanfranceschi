using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain = Tutorials.Core.UI.Boundary.Controllers;

namespace Tutorials.GUI._Web.Controllers
{
    public class SystemController : Controller
    {
       
        public ActionResult Index()
        {
            return View(Domain.SystemController.GetAllSystems());
        }

		[ValidateInput(false)]
		public string Create(string name, string description)
		{
			Domain.SystemController.CreateSystem(name, description);
			return "ok";
		}
    }
}
