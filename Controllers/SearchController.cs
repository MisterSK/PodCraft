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
        //public ActionResult<PodCraftUser> GetById(int id, double depositAmt, double propertyVal)
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

                // Check if search is by user over 18 years of age
                if (DateTimeDiff < 18)
                {
                    return null;
                }

                var product = _context.PodCraftProducts.ToList();
                if (product == null || DateTimeDiff < 18)
                {
                    return NotFound();
                }
                else
                {
                    // Calculate the LTV ratio
                    double ltvRatio = (propertyVal-depositAmt)/propertyVal * 100;

                    // Determine whether or not products are returned, and which
                    // List<PodCraftProduct> lstProducts = new List<PodCraftProduct>();
                    if (ltvRatio > 60 && ltvRatio <= 90)
                    {
                        var products = from p in _context.PodCraftProducts select p;
                        if (products == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            Console.WriteLine("LTVRatio: >60 <=90 is " + ltvRatio);
                            products = products.Where(p => p.LTVRatio <= 90);
                            Console.WriteLine("Products: " + product.ToList());
                        }

                        return user;
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
                            Console.WriteLine("LTVRatio: <60 is " + ltvRatio);
                            products = products.Where(p => p.LTVRatio <= 60);
                            Console.WriteLine("Products: " + products.ToList());
                        }

                        return user;
                    }

                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}