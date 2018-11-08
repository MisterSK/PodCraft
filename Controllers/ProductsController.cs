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

            if (_context.PodCraftProducts.Count() == 0)
            {
                // Create a new PodCraftProduct if list is empty,
                _context.PodCraftProducts.Add(new PodCraftProduct { 
                    Lender = "Bank A",
                    InterestRate = 2,
                    RateType = "Variable",
                    LTVRatio = 60 });

                _context.PodCraftProducts.Add(new PodCraftProduct { 
                    Lender = "Bank B",
                    InterestRate = 3,
                    RateType = "Fixed",
                    LTVRatio = 60 });

                _context.PodCraftProducts.Add(new PodCraftProduct { 
                    Lender = "Bank C",
                    InterestRate = 4,
                    RateType = "Variable",
                    LTVRatio = 90 });

                _context.SaveChanges();
            }
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