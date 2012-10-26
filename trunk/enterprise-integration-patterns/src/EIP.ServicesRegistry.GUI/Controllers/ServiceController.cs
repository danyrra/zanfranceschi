using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIP.ServicesRegistry.GUI.Controllers
{
    public class ServiceController
		: Controller
    {
        //
        // GET: /Service/

		public ActionResult Index()
        {
			return View();
        }

        //
        // GET: /Service/Details/5

        public ActionResult Details(string id)
        {
			return View();
        }

		//
		// GET: /Service/Details/5

		public ActionResult Search(string q)
		{
			return View();
		}

        //
        // GET: /Service/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Service/Create

        [HttpPost]
        public ActionResult Create(string x)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Service/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Service/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Service/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Service/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
