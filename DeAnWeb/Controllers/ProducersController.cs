﻿using System;
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
    public class ProducersController : Controller
    {
        Models.ShopperEntities dbPdc = new Models.ShopperEntities();
        //
        // GET: /Administrator/Producer/123
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
                ViewBag.PdcError = error;
                return View(dbPdc.Producers.ToList());
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
                return View();
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Create(Models.Producers createPdc, HttpPostedFileBase file)
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
                            createPdc.pdcPhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.CreatePdcError = "Không thể chọn ảnh.";
                        }
                    }
                    var pdc = dbPdc.Producers.SingleOrDefault(c => c.pdcName.Equals(createPdc.pdcName));
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (pdc != null)
                            {
                                ViewBag.CreatePdcError = "Hãng sản xuất đã tồn tại.";
                            }
                            else
                            {
                                dbPdc.Producers.Add(createPdc);
                                dbPdc.SaveChanges();
                                ViewBag.CreatePdcError = "Thêm hãng sản xuất thành công.";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.CreatePdcError = "Không thể thêm hãng sản xuất.";
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
                return View(dbPdc.Producers.SingleOrDefault(e => e.pdcID.Equals(id)));
            }
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Models.Producers editPdc, HttpPostedFileBase file)
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
                            editPdc.pdcPhoto = "/Image/" + nameFile;
                        }
                        catch (Exception)
                        {
                            ViewBag.EditPdcError = "Không thể chọn ảnh.";
                        }
                    }
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        dbPdc.Entry(editPdc).State = EntityState.Modified;
                        dbPdc.SaveChanges();
                        ViewBag.EditPdcError = "Cập nhật hãng sản xuất thành công.";
                    }
                }
                catch (Exception)
                {
                    ViewBag.EditPdcError = "Không thể cập nhật hãng sản xuất.";
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
                var model = dbPdc.Producers.SingleOrDefault(h => h.pdcID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbPdc.Producers.Remove(model);
                        dbPdc.SaveChanges();
                        return RedirectToAction("Index", "Producers", new { error = "Xoá hãng sản xuất thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Producers", new { error = "Hãng sản xuất không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Producers", new { error = "Không thể xoá hãng sản xuất." });
                }
            }
        }
    }
}
