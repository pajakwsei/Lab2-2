using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;

namespace LibApp.Controllers
{
    public class CustomersController : Controller
    {
        // DbContext will be polled through Dependency Injection
        public CustomersController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public ViewResult Index()
        {
            var customers = GetCustomers();

            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Jan Kowalski" },
                new Customer { Id = 2, Name = "Monika Nowak" }
            };
        }

        private ApplicationDbContext _context;
    }
}