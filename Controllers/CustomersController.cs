using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;
using NorthwindAPI.Repositories;
using System.Collections.Generic;

namespace NorthwindAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomersController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Customer GetCustomerById(string id)
        {
            return _customerRepository.GetById(id);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _customerRepository.Create(customer);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, Customer customer)
        {
            var existingCustomer = _customerRepository.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.ContactName = customer.ContactName;
            existingCustomer.ContactTitle = customer.ContactTitle;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.Region = customer.Region;
            existingCustomer.PostalCode = customer.PostalCode;
            existingCustomer.Country = customer.Country;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Fax = customer.Fax;

            _customerRepository.Update(existingCustomer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var existingCustomer = _customerRepository.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(existingCustomer);
            return Ok();
        }
    }
}
