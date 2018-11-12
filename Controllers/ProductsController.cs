using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using PodCraft.Models;

namespace PodCraft.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PodCraftContext _context;

        public ProductsController(PodCraftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<PodCraftProduct>> GetAll()
        {
            return _context.PodCraftProducts.ToList();
        }

        [HttpGet("{id}", Name = "GetProducts")]
        public ActionResult<PodCraftProduct> GetById(int id)
        {
            var product = _context.PodCraftProducts.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
    }
}