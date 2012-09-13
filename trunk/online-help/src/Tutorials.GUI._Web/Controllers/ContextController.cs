using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain = Tutorials.Core.UI.Boundary.Controllers;
using Tutorials.Core.Domain.Context;

namespace Tutorials.GUI._Web.Controllers
{
	public class ContextController : Controller
	{
		//
		// GET: /Context/
		public ActionResult Index()
		{
			Context c = Domain.ContextController.GetFirstContext();
			return Redirect(string.Format("~/Context/Details/{0}", c.Key));
		}

		public ActionResult GetAllContexts()
		{
			return PartialView("Partial/Context-List", Domain.ContextController.GetAllContexts());
		}

		public ActionResult Details(string id)
		{
			return View(Domain.ContextController.GetContext(id));
		}

		public string UpdateContextsOrder(string idsraw)
		{
			string[] ids = idsraw.Split(',');
			Domain.ContextController.OrderContexts(ids);
			return "ok";
		}

		public string UpdateTopicsOrder(string contextKey, string idsraw)
		{
			string[] ids = idsraw.Split(',');
			Domain.ContextController.OrderTopics(contextKey, ids);
			return "ok";
		}

		[ValidateInput(false)]
		public string CreateContext(string title, string description, int order)
		{
			Context c = Domain.ContextController.CreateNewContext("x", title, description, order);
			return "ok";
		}

		public string DeleteContext(string id)
		{
			Domain.ContextController.DeleteContext(id);
			return "ok";
		}

		public string UpdateContextTitle(string contextKey, string title)
		{
			Domain.ContextController.UpdateContextTitle(contextKey, title);
			return "ok";
		}

		[ValidateInput(false)]
		public string UpdateContextDescription(string contextKey, string description)
		{
			Domain.ContextController.UpdateContextDescription(contextKey, description);
			return "ok";
		}
	}
}
