using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Add_Delete_Rows_Table_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CustomersEntities entities = new CustomersEntities();
            List<Customer> customers = entities.Customers.ToList();

            //Add a Dummy Row.
            customers.Insert(0, new Customer());
            return View(customers);
        }

        [HttpPost]
        public JsonResult InsertCustomer(Customer customer)
        {
            using (CustomersEntities entities = new CustomersEntities())
            {
                entities.Customers.Add(customer);
                entities.SaveChanges();
            }

            return Json(customer);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
            using (CustomersEntities entities = new CustomersEntities())
            {
                Customer customer = (from c in entities.Customers
                                     where c.CustomerId == customerId
                                     select c).FirstOrDefault();
                entities.Customers.Remove(customer);
                entities.SaveChanges();
            }
            return new EmptyResult();
        }
    }
}
