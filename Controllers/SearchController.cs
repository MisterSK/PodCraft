using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using PodCraft.Models;
using Microsoft.AspNetCore.Http;

namespace PodCraft.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly PodCraftContext _context;

        public SearchController(PodCraftContext context)
        {
            _context = context;

            if (_context.PodCraftProducts.Count() == 0)
            {
                // Delete the entire PodCraftProducts list
                var products = context.PodCraftProducts;
                foreach (var product in products)
                {
                    products.Remove(product);
                }

                // Create a new PodCraftProduct if list is empty,
                _context.PodCraftProducts.Add(new PodCraftProduct
                {
                    Lender = "Bank A",
                    InterestRate = 2,
                    RateType = "Variable",
                    LTVRatio = 60
                });

                _context.PodCraftProducts.Add(new PodCraftProduct
                {
                    Lender = "Bank B",
                    InterestRate = 3,
                    RateType = "Fixed",
                    LTVRatio = 60
                });

                _context.PodCraftProducts.Add(new PodCraftProduct
                {
                    Lender = "Bank C",
                    InterestRate = 4,
                    RateType = "Variable",
                    LTVRatio = 90
                });

                _context.SaveChanges();
            }

        }

        [HttpGet("{id}/{depositAmt}/{propertyVal}", Name = "ProductSearch")]
        //public ActionResult<PodCraftUser> Index(int id, long depositAmt, long propertyVal)
        public ActionResult<PodCraftUser> GetById(int id)
        {
            var user = _context.PodCraftUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                // Calculate the number of hours between date of birth and the time now
                DateTime dateOfBith = Convert.ToDateTime(user.DateOfBirth);
                DateTime localTime = Convert.ToDateTime(DateTime.Now);
                int DateTimeDiff = localTime.Year - dateOfBith.Year;
                HttpContext.Session.SetInt32("ageSpan", DateTimeDiff);
                var ageSpan = HttpContext.Session.GetInt32("ageSpan");
                Console.WriteLine(ageSpan);

                // Check if search is by user over 18 years of age
                if (DateTimeDiff < 18)
                {
                    return null;
                }

                return user;
            }
        }

        public ActionResult<List<PodCraftProduct>> Search(long depositAmt, long propertyVal)
        {
            var product = _context.PodCraftProducts.ToList();
            var ageSpan = HttpContext.Session.GetInt32("ageSpan");
            if (product == null || ageSpan < 18)
            {
                return NotFound();
            }
            else
            {
                // Calculate the LTV ratio
                decimal ltvRatio = Decimal.Round((depositAmt / propertyVal) * 100);
                Console.WriteLine("ltvRatio: " + ltvRatio);

                if (ltvRatio > 60 && ltvRatio <= 90)
                {
                    var products = from p in _context.PodCraftProducts select p;
                    if (products == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        products = products.Where(l => l.LTVRatio <= 90);
                    }

                    HttpContext.Items.Remove("ageSpan");
                    return product;
                }
                else if (ltvRatio < 60)
                {
                    var products = from p in _context.PodCraftProducts select p;
                    if (products == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        products = products.Where(l => l.LTVRatio <= 60);
                    }

                    HttpContext.Items.Remove("ageSpan");
                    return product;
                }

                else
                {
                    return null;
                }
            }
        }
    }
}