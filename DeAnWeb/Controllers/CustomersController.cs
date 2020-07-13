using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeAnWeb.Models;

namespace DeAnWeb.Controllers
{
    public class CustomersController : Controller
    {
       ShopperEntities dbCus = new ShopperEntities();
        //
        // GET: /Administrator/Category/
        [HandleError]
        public ActionResult Index(string name, string error)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.CusError = error;
                var model = dbCus.Customers.ToList();
                if (!string.IsNullOrEmpty(name))
                {
                    model = model.Where(p => p.cusPhone.Contains(name)).ToList();
                }
                return View(model);
            }
        }
    }
}
