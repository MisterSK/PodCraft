using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using PodCraft.Models;

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

        }

        [HttpGet("{id}", Name = "GetSearch")]
        public ActionResult<PodCraftProduct> GetById(int LTVRatio, DateTime DateOfBirth)
        {
            var search = _context.PodCraftProducts.Find(LTVRatio, DateOfBirth);
            if (search == null)
            {
                return NotFound();
            }

            // Calculate the number of hours between date of birth and the time now
            DateTime localTime = DateTime.Now;
            int DateTimeDiff = (localTime - DateOfBirth).Hours;

            // Check is search is by user over 18 years of age
            if (LTVRatio > 90 || DateTimeDiff > 157680)
            {
                return null;
            }
            else
            {
                return search;
            }
        }
    }
}