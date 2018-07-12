using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneBook.Models;
using PhoneBook.Dto;
using AutoMapper;
using System.Web.Mvc;

namespace PhoneBook.Controllers.Api
{
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }


        //Get /api/customer

        public IHttpActionResult GetCustomers()
        {
            var customersDto= _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customersDto);
        }

        //Get /api/customer/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(m => m.Id == id);
            if (customer == null)
                return NotFound();
            var customerDto = Mapper.Map<Customer, CustomerDto>(customer);
            return Ok(customerDto);

        }

        //Delete /api/customer/id
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
