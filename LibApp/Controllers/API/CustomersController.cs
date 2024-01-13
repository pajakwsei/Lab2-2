using AutoMapper;
using LibApp.Data;
using LibApp.Models;
using LibApp.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Connections;

namespace LibApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public CustomersController(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>); 
        }        
        
        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        { 
            var customer = _mapper.Map<CustomerDto>(_context.Customers.SingleOrDefault(c => c.Id == id)); 

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST /api/customers
        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Customers.Add(_mapper.Map<Customer>(customerDto));
            _context.SaveChanges();

            // Consider changing status - should be 201
            return CreatedAtRoute(nameof(GetCustomer), new { id = customerDto.Id});
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            _mapper.Map(customerDto, customerInDb);

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
        private IMapper _mapper;
    }
}
