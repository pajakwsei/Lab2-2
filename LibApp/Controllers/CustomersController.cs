using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;

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
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        public IActionResult New() 
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel { MembershipTypes = membershipTypes };

            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult Create(NewCustomerViewModel viewModel)
        {
            _context.Customers.Add(viewModel.Customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");

        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("New", viewModel);
        }

        private ApplicationDbContext _context;
    }
}