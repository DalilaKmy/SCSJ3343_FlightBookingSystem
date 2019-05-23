using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FlightSystem2.Models;

namespace FlightSystem2.Controllers
{
    public class LoginController : Controller
    {
        FlightDatabaseEntities db = new FlightDatabaseEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            //FlightDatabaseEntities db = new FlightDatabaseEntities();
            int count = (from x in db.Customers
                         where x.username == cust.username
                         where x.password == cust.password
                         select x).Count();
            if(count == 0)
            {
                ViewBag.errMsg = "No match found";
                return View();
            }
            else
            {
                var id = (from x in db.Customers
                          where x.username == cust.username
                          where x.password == cust.password
                          select x.customerID).FirstOrDefault();

                Session["Username"] = cust.username;
                Session["CustomerID"] = id;
                FormsAuthentication.SetAuthCookie(cust.username, false);
                return RedirectToAction("MainUserPage", "Login");
            }
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdmninLogin(Admin admin)
        {
            //FlightDatabaseEntities db = new FlightDatabaseEntities();
            int count1 = (from x in db.Admins
                         where x.username == admin.username
                         where x.password == admin.password
                         select x).Count();
            if (count1 == 0)
            {
                ViewBag.errMsg = "No match found";
                return View();
            }
            else
            {
                var adminid = (from y in db.Admins
                               where y.username == admin.username
                               where y.password == admin.password
                               select y.adminID).FirstOrDefault();

                Session["AdminUsername"] = admin.username;
                Session["AdminID"] = adminid;
                FormsAuthentication.SetAuthCookie(admin.username, false);
                return RedirectToAction("MainAdminPage", "Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return RedirectToAction("MainUserPage", "Login");
            }
            return View(cust);
        }

        public ActionResult MainUserPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("MainAdminPage", "Login");
            }
            return View(admin);
        }


        public ActionResult MainAdminPage()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,username,name,email,hpno,password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID,username,name,email,hpno,password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
