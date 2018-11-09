using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using PodCraft.Models;

namespace PodCraft.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PodCraftContext _context;

        public UsersController(PodCraftContext context)
        {
            _context = context;

            if (_context.PodCraftUsers.Count() == 0 || _context.PodCraftUsers.Count() > 12)
            {
                // Delete the entire PodCraftUsers list
                var users = context.PodCraftUsers;
                foreach (var user in users)
                {
                    users.Remove(user);
                }

                // Create a new PodCraftUser if list is empty,
                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john.doe@example.com",
                    DateOfBirth = DateTime.Parse("01/01/1970") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "jane.doe@example.com",
                    DateOfBirth = DateTime.Parse("13/02/2003") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Ally",
                    LastName = "Pally",
                    EmailAddress = "ally.pally@example.com",
                    DateOfBirth = DateTime.Parse("29/05/1988") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Peter",
                    LastName = "Pan",
                    EmailAddress = "peter.pan@example.com",
                    DateOfBirth = DateTime.Parse("06/06/1959") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Betsy",
                    LastName = "Brandt",
                    EmailAddress = "betsy.brandt@example.com",
                    DateOfBirth = DateTime.Parse("20/12/2004") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Marylyn",
                    LastName = "Monroe",
                    EmailAddress = "marylyn.monroe@example.com",
                    DateOfBirth = DateTime.Parse("18/11/1966") });


                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Jack",
                    LastName = "Daniels",
                    EmailAddress = "jack.daniels@example.com",
                    DateOfBirth = DateTime.Parse("17/09/2001") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Steven",
                    LastName = "Segal",
                    EmailAddress = "steven.segal@example.com",
                    DateOfBirth = DateTime.Parse("25/08/1994") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Muhammad",
                    LastName = "Ali",
                    EmailAddress = "john.doe@example.com",
                    DateOfBirth = DateTime.Parse("09/10/1980") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Steven",
                    LastName = "Myers",
                    EmailAddress = "steven.myers@example.com",
                    DateOfBirth = DateTime.Parse("30/11/2002") });

                _context.PodCraftUsers.Add(new PodCraftUser { 
                    FirstName = "Angela",
                    LastName = "Beard",
                    EmailAddress = "angela.beard@example.com",
                    DateOfBirth = DateTime.Parse("31/12/1960") });

                _context.PodCraftUsers.Add(new PodCraftUser
                {
                    FirstName = "Amina",
                    LastName = "Abdul",
                    EmailAddress = "amina.abdul@example.com",
                    DateOfBirth = DateTime.Parse("16/10/1979")
                });

                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<PodCraftUser>> GetAll()
        {
            return _context.PodCraftUsers.ToList();
        }

        [HttpGet("{id}", Name = "GetUsers")]
        public ActionResult<PodCraftUser> GetById(int id)
        {
            var user = _context.PodCraftUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public IActionResult Create(PodCraftUser user)
        {
            _context.PodCraftUsers.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetUsers", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PodCraftUser user)
        {
            var users = _context.PodCraftUsers.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            users.FirstName = users.FirstName;
            users.LastName = users.LastName;
            users.EmailAddress = users.EmailAddress;
            users.DateOfBirth = users.DateOfBirth;

            _context.PodCraftUsers.Remove(users);
            _context.PodCraftUsers.Update(users);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.PodCraftUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.PodCraftUsers.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}