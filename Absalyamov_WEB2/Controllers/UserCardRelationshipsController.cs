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

        [HttpPost("BuyPlayerById")]
        public async Task<ActionResult<string>> BuyPlayer(int _CardID)
        {
            int _UserID = GetUserID(User.Identity.Name);

            UserCardRelationship Relationship = new UserCardRelationship();
            Relationship = GenerateSkills(_CardID);

            if (PlayerCardExists(_CardID))
            {
                _context.UserCardRelationships.Add(Relationship);

                int UserBalance = GetUserBalance(User.Identity.Name);
                int CardPrice = GetCardPrice(_CardID);
                int count = (from UserCardRelationships in _context.UserCardRelationships where _UserID == UserCardRelationships.UserID select _UserID).Count();
                if ((UserBalance >= CardPrice) && (count < 5))
                {
                    int NewBalance = UserBalance - CardPrice;
                    SetNewUserBalance(_UserID, _CardID, NewBalance);
                    await _context.SaveChangesAsync();
                    return Ok(Relationship);
                }
                else if ((count >= 5) && (UserBalance >= CardPrice))
                    return BadRequest("You can buy just 5 players in your team");
                else
                    return BadRequest("You haven't enough money to buy this player");
            }
            else return BadRequest("There are no players with this id");

        }

        private UserCardRelationship GenerateSkills(int _CardID)
        {
            UserCardRelationship _Relationship = new UserCardRelationship();
            Random rnd = new Random();
            int _UserID = GetUserID(User.Identity.Name);
            string CardQuality = GetCardQuality(_CardID);
            int q;
            if (CardQuality == "Gold") q = 80;
            else if (CardQuality == "Silver") q = 65;
            else if (CardQuality == "Bronze") q = 50;
            else q = 0;

            _Relationship.UserID = _UserID;
            _Relationship.CardID = _CardID;
            _Relationship.Pace = rnd.Next(q, 100);
            _Relationship.Shooting = rnd.Next(q, 100);
            _Relationship.Defending = rnd.Next(q, 100);
            _Relationship.Dribling = rnd.Next(q, 100);
            _Relationship.Passing = rnd.Next(q, 100);
            _Relationship.Physical = rnd.Next(q, 100);
            return (_Relationship);
        }

        private void SetNewUserBalance(int _UserID, int _CardID, int _NewBalance)
        {
            var query = from Users in _context.Users where Users.Id == _UserID select Users;
            foreach (User user in query)
            {
                user.Balance = _NewBalance;
            }
        }

        private int GetUserID(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Id;
            int id = query.FirstOrDefault();
            return id;
        }

        private int GetUserBalance(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Balance;
            int _Balance = query.FirstOrDefault();
            return _Balance;
        }

        private int GetCardPrice(int CardID)
        {
            IQueryable<int> query = from PlayerCard in _context.PlayerCards where PlayerCard.Id == CardID select PlayerCard.Price;
            int _Price = query.FirstOrDefault();
            return _Price;
        }
        private string GetCardQuality(int CardID)
        {
            IQueryable<string> query = from PlayerCard in _context.PlayerCards where PlayerCard.Id == CardID select PlayerCard.Quality;
            string _Quality = query.First();
            return _Quality;
        }

        private bool UserCardRelationshipExists(int id)
        {
            return _context.UserCardRelationships.Any(e => e.Id == id);
        }

        private bool PlayerCardExists(int id)
        {
            return _context.PlayerCards.Any(e => e.Id == id);
        }

    }
}
