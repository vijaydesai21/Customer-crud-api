using CustomerModule.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateCustomer([FromBody] Customer customer)
    {
        // Validation
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if email already exists
        if (_context.Customers.Any(c => c.EmailId == customer.EmailId))
        {
            ModelState.AddModelError("EmailId", "Email already exists");
            return BadRequest(ModelState);
        }

        // Check if mobile number already exists
        if (_context.Customers.Any(c => c.MobileNumber == customer.MobileNumber))
        {
            ModelState.AddModelError("MobileNumber", "Mobile number already exists");
            return BadRequest(ModelState);
        }

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return Ok(customer);
    }

    [HttpGet("{email}")]
    public IActionResult GetCustomerByEmail(string email)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.EmailId == email);

        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        return Ok(customer);
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var customers = _context.Customers.ToList();
        return Ok(customers);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
    {
        var existingCustomer = _context.Customers.FirstOrDefault(c => c.Id == id);

        if (existingCustomer == null)
        {
            return NotFound("Customer not found");
        }

        // Check if email already exists 
        if (_context.Customers.Any(c => c.EmailId == updatedCustomer.EmailId && c.Id != id))
        {
            ModelState.AddModelError("EmailId", "Email already exists");
            return BadRequest(ModelState);
        }

        // Check if mobile number already exists 
        if (_context.Customers.Any(c => c.MobileNumber == updatedCustomer.MobileNumber && c.Id != id))
        {
            ModelState.AddModelError("MobileNumber", "Mobile number already exists");
            return BadRequest(ModelState);
        }

        // Update customer details
        existingCustomer.FirstName = updatedCustomer.FirstName;
        existingCustomer.MobileNumber = updatedCustomer.MobileNumber;
        existingCustomer.EmailId = updatedCustomer.EmailId;

        _context.SaveChanges();

        return Ok(existingCustomer);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return Ok("Customer deleted successfully");
    }


}




