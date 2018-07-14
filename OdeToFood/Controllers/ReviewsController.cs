using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();
        public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
        {
            //Eagerly loading related entities using the Include() method
            var restaurant = _db.Restaurants.Include(p => p.Reviews).ToList().Where(r => r.Id == restaurantId);
            if(restaurant != default(object))
            {
                return View(restaurant);
            }

            return HttpNotFound();
        }

        //
        // GET: /Reviews/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reviews/Create
        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            return View();
        }

        //
        // POST: /Reviews/Create

        [HttpPost]
        public ActionResult Create(RestaurantReview review)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _db.Reviews.Add(review);
                    _db.SaveChanges();
                    return RedirectToAction("Index", new { id = review.RestaurantId});
                }
                return View(review);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reviews/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var review = _db.Reviews.Find(id);
            return View(review);
        }

        ////
        //// POST: /Reviews/Edit/5

        [HttpPost]
        public ActionResult Edit(RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                // ..
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        //
        // GET: /Reviews/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reviews/Delete/5

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
        protected override void Dispose(bool disposing)
        {
            if(_db != default(object))
            {
                _db.Dispose();
            }
            base.Dispose(disposing);    
        }
    }
}
