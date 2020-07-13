using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeAnWeb.Models;

namespace DeAnWeb.Controllers
{
    public class CategoriesController : Controller
    {
        Models.ShopperEntities dbCate = new Models.ShopperEntities();
        //
        // GET: /Administrator/Category/
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
                ViewBag.CateError = error;
                var modelCate = dbCate.Categories.ToList();
                return View(modelCate);
            }
        }

        [HandleError]
        public ActionResult Create()
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Categories createCate, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            createCate.catePhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreateCategory = "Không thể chọn ảnh.";
                        }
                    }
                    try
                    {
                        if (dbCate.Categories.SingleOrDefault(cr => cr.cateName.Equals(createCate.cateName)) == null)
                        {
                            dbCate.Categories.Add(createCate);
                            dbCate.SaveChanges();
                            ViewBag.CreateCategory = "Thêm danh mục thành công.";
                        }
                        else
                        {
                            ViewBag.CreateCategory = "Tên danh mục đã tồn tại.";
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.CreateCategory = "Không thể thêm danh mục.";
                    }
                }
                else
                {
                    ViewBag.HinhAnh = "Vui lòng chọn hình ảnh.";
                }
                return View();
            }
        }

        [HandleError]
        public ActionResult Edit(int id)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = dbCate.Categories.SingleOrDefault(c => c.cateID.Equals(id));
                return View(model);
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Categories editCate, HttpPostedFileBase file)
        {
            if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        try
                        {
                            string nameFile = Path.GetFileName(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("/Image"), nameFile));
                            editCate.catePhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.EditCategory = "Không thể chọn ảnh.";
                        }
                    }
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (dbCate.Categories.SingleOrDefault(cr => cr.cateName.Equals(editCate.cateName)) == null)
                        {
                            dbCate.Entry(editCate).State = EntityState.Modified;
                            dbCate.SaveChanges();
                            ViewBag.EditCategory = "Cập nhật danh mục thành công.";
                        }
                        else
                        {
                            ViewBag.EditCategory = "Tên danh mục đã tồn tại.";
                        }
                    }
                }
                catch (Exception)
                {
                    ViewBag.EditCategory = "Không thể cập nhật danh mục.";
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
                var model = dbCate.Categories.SingleOrDefault(h => h.cateID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbCate.Categories.Remove(model);
                        dbCate.SaveChanges();
                        return RedirectToAction("Index", "Categories", new { error = "Xoá danh mục thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Categories", new { error = "Danh mục không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Categories", new { error = "Không thể xoá danh mục." });
                }
            }
        }
    }
}
