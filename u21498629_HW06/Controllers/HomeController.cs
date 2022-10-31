using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21498629_HW06.Models;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.Entity;

namespace u21498629_HW06.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report()
        {
            GetReports();
            return View();
        }

        public string GetReports()
        {

            object orders = db.orders.Select(p => new
            {
                Products = db.order_items.Where(y => y.order_id == p.order_id).Select(o => new { category_id = o.product.category.category_id, Quantity = o.quantity, month = p.order_date.Month }).ToList<dynamic>(),
            }).ToList();
            return JsonConvert.SerializeObject(orders);
        }

    }
}