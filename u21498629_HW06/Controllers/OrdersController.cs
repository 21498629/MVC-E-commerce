﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList.Mvc;
using PagedList;
using System.Web.Mvc;
using u21498629_HW06.Models;

namespace u21492362_HW06.Views
{
    public class OrdersController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: order_items
        [HttpGet]
        public ActionResult Orders(DateTime? orderdate, int? page)
        {
            var order_items = db.order_items.Include(o => o.product).Include(o => o.order);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(order_items.Where(o => o.order.order_date == orderdate).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: order_items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            return View(order_items);
        }

        // GET: order_items/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id");
            return View();
        }

        // POST: order_items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,item_id,product_id,quantity,list_price,discount")] order_items order_items)
        {
            if (ModelState.IsValid)
            {
                db.order_items.Add(order_items);
                db.SaveChanges();
                return RedirectToAction("Orders");
            }

            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // GET: order_items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // POST: order_items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,item_id,product_id,quantity,list_price,discount")] order_items order_items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // GET: order_items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            return View(order_items);
        }

        // POST: order_items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_items order_items = db.order_items.Find(id);
            db.order_items.Remove(order_items);
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}