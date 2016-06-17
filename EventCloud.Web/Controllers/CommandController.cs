using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCloud.Web.Controllers
{
    public class CommandController : Controller
    {
        //
        // GET: /Command/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Command/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Command/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Command/Create
        [HttpPost]
        public ActionResult AjaxCreate(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Command/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Command/Edit/5
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
        // GET: /Command/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Command/Delete/5
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
