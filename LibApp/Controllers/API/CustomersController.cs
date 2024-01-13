using LibApp.Data;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CustomersController(ApplicationDbContext context) {
            _context = context;
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        { 
            return _context.Customers.ToList(); 
        }        
        
        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        { 
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id); 

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST /api/customers
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok(customer);
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.SubscribedToNewsletter = customer.SubscribedToNewsletter;
            customerInDb.MembershipType = customer.MembershipType;

            _context.SaveChanges();
            return Ok(customerInDb);
        }

        // DELETE /api/customer/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _context.Remove(customerInDb);
            _context.SaveChanges();

            return Ok(customerInDb);
        }


        private ApplicationDbContext _context;
    }
}
