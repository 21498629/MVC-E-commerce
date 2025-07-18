﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21498629_HW06.Models;
using PagedList;

namespace u21498629_HW06.Models
{
    public class productsController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: products
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {

            var products = db.products.Include(p => p.brand).Include(p => p.category);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(product => product.product_name.Contains(searchString));
            }

            products = products.OrderBy(p => p.model_year);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: products/Details/5
        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return PartialView("DetailsPartial", product);
            //return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private Context _context;
        //public productsController()
        //{
        //    _context = new Context();
        //}

        //public JsonResult List()
        //{
        //    return Json(_context.Products.ToList(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Add(product products)
        //{
        //    _context.Products.Add(products);
        //    _context.SaveChanges();
        //    return Json(JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetbyID(int ID)
        //{
        //    return Json(_context.Products.FirstOrDefault(x => x.product_id == ID), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Update(product products)
        //{
        //    var data = _context.Products.FirstOrDefault(x => x.product_id == products.product_id);
        //    if (data != null)
        //    {
        //        data.product_name = products.product_name;
        //        data.model_year = products.model_year;
        //        data.list_price = products.list_price;
        //        data.brand.brand_name = products.brand.brand_name;
        //        data.category.category_name = products.category.category_name;
        //        _context.SaveChanges();
        //    }
        //    return Json(JsonRequestBehavior.AllowGet);
        //}

    }
}
