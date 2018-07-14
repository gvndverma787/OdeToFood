using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using System.Configuration;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        IOdeToFoodDb _db;
        public HomeController()
        {
            _db = new OdeToFoodDb();
        }
        public HomeController(IOdeToFoodDb db)
        {
            _db = db;
        }
        public ActionResult Autocomplete(string term)
        {
            var model = _db.Query<Restaurant>()
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Name
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(CacheProfile = "Long", VaryByHeader = "X-Requested-With;Accept-Language", Location = OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            //var model = from r in _db.Restaurants
            //            orderby r.Reviews.Average(review => review.Rating) descending
            //            select new RestaurantViewListModel
            //            {
            //                Id = r.Id,
            //                Name = r.Name,
            //                City = r.City,
            //                Country = r.Country,
            //                NumberOfReviews = r.Reviews.Count()
            //            };

            ViewBag.MailServer = ConfigurationManager.AppSettings["MailServer"];
            //Extension method query syntax
            var model = _db.Query<Restaurant>()
                .Where(r => searchTerm == default(object) || r.Name.StartsWith(searchTerm))
                .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                .Select(r => new RestaurantViewListModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    City = r.City,
                    Country = r.Country,
                    CountOfReviews = r.Reviews.Count()
                }).ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }
            return View(model);
        }
        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Scott";
            model.Location = "Maryland, USA";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (_db != default(object))
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
