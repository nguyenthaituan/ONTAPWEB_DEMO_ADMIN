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
    public class ProductTypesController : Controller
    {
        Models.ShopperEntities dbType = new Models.ShopperEntities();
        //
        // GET: /Administrator/ProductType/
        [HandleError]
        public ActionResult Index(string error)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.TypeError = error;
                return View(dbType.ProductTypes.ToList());
            }
        }

        [HandleError]
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.cateListCreate = new SelectList(dbType.Categories, "cateID", "cateName");
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.ProductTypes createType)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.cateListCreate = new SelectList(dbType.Categories, "cateID", "cateName");
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbType.ProductTypes.Add(createType);
                        dbType.SaveChanges();
                        ViewBag.CreateTypeError = "Thêm loại sản phẩm thành công.";
                    }
                }
                catch (Exception)
                {
                    ViewBag.CreateTypeError = "Không thể thêm loại sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.cateListEdit = new SelectList(dbType.Categories, "cateID", "cateName");
                return View(dbType.ProductTypes.SingleOrDefault(e => e.typeID.Equals(id)));
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.ProductTypes editType)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.cateListEdit = new SelectList(dbType.Categories, "cateID", "cateName");
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbType.Entry(editType).State = EntityState.Modified;
                        dbType.SaveChanges();
                        ViewBag.EditTypeError = "Cập nhật loại sản phẩm thành công.";
                    }
                }
                catch (Exception)
                {
                    ViewBag.EditTypeError = "Không thể cập nhật loại sản phẩm.";
                }
                return View();
            }
        }

        [HandleError]
        public ActionResult Delete(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = dbType.ProductTypes.SingleOrDefault(h => h.typeID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbType.ProductTypes.Remove(model);
                        dbType.SaveChanges();
                        return RedirectToAction("Index", "ProductTypes", new { error = "Xoá loại sản phẩm thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "ProductTypes", new { error = "Loại sản phẩm không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "ProductTypes", new { error = "Không thể xoá loại sản phẩm." });
                }
            }
        }
    }
}

