using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain = Tutorials.Core.UI.Boundary.Controllers;
using Tutorials.Core.Domain.System;

namespace Tutorials.GUI._Web.Controllers
{
	public class ContextController : Controller
	{
		//
		// GET: /Context/
		public ActionResult Index()
		{
			Context c = Domain.SystemController.GetFirstContext();
			return Redirect(string.Format("~/Context/Details/{0}", c.Key));
		}

		public ActionResult GetAllContexts()
		{
			return PartialView("Partial/Context-List", Domain.SystemController.GetAllContexts());
		}

		public ActionResult Details(string id)
		{
			return View(Domain.SystemController.GetContext(id));
		}

		public string UpdateContextsOrder(string idsraw)
		{
			string[] ids = idsraw.Split(',');
			Domain.SystemController.OrderContexts(ids);
			return "ok";
		}

		public string UpdateTopicsOrder(string contextKey, string idsraw)
		{
			string[] ids = idsraw.Split(',');
			Domain.SystemController.OrderTopics(contextKey, ids);
			return "ok";
		}

		[ValidateInput(false)]
		public string CreateContext(string title, string description, int order)
		{
			Context c = Domain.SystemController.CreateNewContext("x", title, description, order);
			return "ok";
		}

		public string DeleteContext(string id)
		{
			Domain.SystemController.DeleteContext(id);
			return "ok";
		}

		public string UpdateContextTitle(string contextKey, string title)
		{
			Domain.SystemController.UpdateContextTitle(contextKey, title);
			return "ok";
		}

		[ValidateInput(false)]
		public string UpdateContextDescription(string contextKey, string description)
		{
			Domain.SystemController.UpdateContextDescription(contextKey, description);
			return "ok";
		}
	}
}
