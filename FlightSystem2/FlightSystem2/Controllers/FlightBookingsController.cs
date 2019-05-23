using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using FlightSystem2.Models;

namespace FlightSystem2.Controllers
{
    public class FlightBookingsController : Controller
    {
        private FlightDatabaseEntities db = new FlightDatabaseEntities();

        // GET: FlightBookings
        public ActionResult Index()
        {
            var flightBookings = db.FlightBookings.Include(f => f.Customer).Include(f => f.Flight);
            return View(flightBookings.ToList());
        }

        public ActionResult Index1()
        {
            int fiid = Convert.ToInt32(Session["fid"]);
            int id = Convert.ToInt32(Session["CustomerID"]);
            ViewBag.price = from x in db.Flights
                               where x.flightID == fiid
                               select x.price;
            ViewBag.date = from x in db.Flights
                            where x.flightID == fiid
                            select x.flightDate;

            
            //DateTime bookDate = DateTime.ParseExact(date, )
            /*if (date)
            {
            }*/
            var flightBookings = db.FlightBookings.Include(f => f.Customer).Include(f => f.Flight);
            return View(flightBookings.ToList());
        }

        // GET: FlightBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            if (flightBooking == null)
            {
                return HttpNotFound();
            }
            return View(flightBooking);
        }

        // GET: FlightBookings/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.Customers, "customerID", "username");
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "flightName");
            ViewBag.price = new SelectList(db.Flights, "price", "price");
            Session["fid"] = ViewBag.flightID;
            return View();
        }

        // POST: FlightBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookingID,customerID,flightID")] FlightBooking flightBooking)
        {
            if (ModelState.IsValid)
            {
                flightBooking.customerID = Convert.ToInt32(Session["CustomerID"]);
                db.FlightBookings.Add(flightBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.Customers, "customerID", "username", flightBooking.customerID);
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "flightName", flightBooking.flightID);
            return View(flightBooking);
        }

        // GET: FlightBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            if (flightBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.Customers, "customerID", "username", flightBooking.customerID);
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "flightName", flightBooking.flightID);
            return View(flightBooking);
        }

        // POST: FlightBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookingID,customerID,flightID")] FlightBooking flightBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.Customers, "customerID", "username", flightBooking.customerID);
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "flightName", flightBooking.flightID);
            return View(flightBooking);
        }

        // GET: FlightBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            if (flightBooking == null)
            {
                return HttpNotFound();
            }
            return View(flightBooking);
        }

        // POST: FlightBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            db.FlightBookings.Remove(flightBooking);
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
    }
}
