using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagination.WebApi.Contexts;
using Pagination.WebApi.Filter;
using Pagination.WebApi.Helpers;
using Pagination.WebApi.Models;
using Pagination.WebApi.Services;
using Pagination.WebApi.Wrappers;

namespace Pagination.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUriService uriService;
        public CustomerController(ApplicationDbContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var allCustomers = await context.Customers.ToListAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Customer>(allCustomers, filter, uriService,route);
            return Ok(pagedReponse);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await context.Customers.Where(a => a.Id == id).FirstOrDefaultAsync();
            return Ok(new Response<Customer>(customer));
        }
    }
}