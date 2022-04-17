#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Absalyamov_WEB2;
using Absalyamov_WEB2.Data;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardRelationshipsController : ControllerBase
    {
        private readonly DataContext _context;

        public UserCardRelationshipsController(DataContext context)
        {
            _context = context;

        }

        // GET: api/UserCardRelationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCardRelationship>>> GetUserCardRelationships()
        {
            return await _context.UserCardRelationships.ToListAsync();
        }

        // GET: api/UserCardRelationships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCardRelationship>> GetUserCardRelationship(int id)
        {
            var userCardRelationship = await _context.UserCardRelationships.FindAsync(id);

            if (userCardRelationship == null)
            {
                return NotFound();
            }

            return userCardRelationship;
        }

        // PUT: api/UserCardRelationships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCardRelationship(int id, UserCardRelationship userCardRelationship)
        {
            if (id != userCardRelationship.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCardRelationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCardRelationshipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserCardRelationships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCardRelationship>> PostUserCardRelationship(UserCardRelationship userCardRelationship)
        {
            _context.UserCardRelationships.Add(userCardRelationship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCardRelationship", new { id = userCardRelationship.Id }, userCardRelationship);
        }

        // DELETE: api/UserCardRelationships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCardRelationship(int id)
        {
            var userCardRelationship = await _context.UserCardRelationships.FindAsync(id);
            if (userCardRelationship == null)
            {
                return NotFound();
            }

            _context.UserCardRelationships.Remove(userCardRelationship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCardRelationshipExists(int id)
        {
            return _context.UserCardRelationships.Any(e => e.Id == id);
        }
    }
}
