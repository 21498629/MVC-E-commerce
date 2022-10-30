using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21498629_HW06.Models;
using PagedList;
using PagedList.Mvc;

namespace u21498629_HW06.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Orders(int? page)
        {
            ViewBag.Message = "Your application description page.";

            var orders = db.order_items.OrderBy(o => o.order_id);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}