using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Db4objects.Db4o;
using TI.Usuarios.Modelos;
using Db4objects.Db4o.CS;

namespace TI.Usuarios.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			return View();
        }

		public ActionResult RescisoesTable()
		{
			return PartialView(Rescisao.GetAll());
		}

        public ActionResult VerificarNovaRescisao()
        {
			 return Json(Rescisao.GetLastOrDefault(), JsonRequestBehavior.AllowGet);
        }
    }
}
