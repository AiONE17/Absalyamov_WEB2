using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        public AdminController(DataContext context)
        {
            _context = context;

        }

        [HttpGet("Get all users"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(int id)
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut("Give admin role to someone by id"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GiveAdminRole(int id)
        {
            var query = from Users in _context.Users where Users.Id == id select Users;
            foreach (User user in query)
            {
                user.Role = true;
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut("Remove admin role from someone by id"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdminRole(int id)
        {
            var query = from Users in _context.Users where Users.Id == id select Users;
            foreach (User user in query)
            {
                user.Role = false;
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost("Add new player card"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> AddCard(PlayerCard sub)
        {
            _context.PlayerCards.Add(sub);
            await _context.SaveChangesAsync();
            return Ok(await _context.PlayerCards.ToListAsync());
        }

        [HttpPut("Update player card"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> UpdateCard(PlayerCard request)
        {
            var dbPlayerCard = await _context.PlayerCards.FindAsync(request.Id);
            if (dbPlayerCard == null)
                return BadRequest("Country not found :(");
            dbPlayerCard.Name = request.Name;
            dbPlayerCard.Surname = request.Surname;
            dbPlayerCard.Country = request.Country;


            await _context.SaveChangesAsync();

            return Ok(await _context.PlayerCards.ToListAsync());
        }

        [HttpDelete("Delete playercard by id"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> Delete(int id)
        {
            var dbPlayerCard = await _context.PlayerCards.FindAsync(id);
            if (dbPlayerCard == null)
                return BadRequest("Country not found :(");
            _context.PlayerCards.Remove(dbPlayerCard);
            return Ok(await _context.PlayerCards.ToListAsync());
        }

        [HttpDelete("Delete user's teams"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> DeleteRelationships()
        {
            var query = from UserCardRelationships in _context.UserCardRelationships select UserCardRelationships;
            foreach (UserCardRelationship relationship in query)
            {
                _context.UserCardRelationships.RemoveRange(relationship);
            }
            await _context.SaveChangesAsync();
            return Ok("User's teams are deleted");
        }

        [HttpDelete("Delete PlayerCards"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> DeletePlayerCards()
        {
            var query = from PlayerCards in _context.PlayerCards select PlayerCards;
            foreach (PlayerCard _playercard in query)
            {
                _context.PlayerCards.RemoveRange(_playercard);
            }
            await _context.SaveChangesAsync();
            return Ok("Cards are deleted");
        }

        [HttpDelete("Clear rating"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> DeleteRating()
        {
            var query = from Ratings in _context.Ratings select Ratings;
            foreach (Rating _rating in query)
            {
                _context.Ratings.RemoveRange(_rating);
            }
            await _context.SaveChangesAsync();
            return Ok("Rating is deleted");
        }

        [HttpPut("Set all player's balance"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> Setallbalance(int newbalance)
        {
            var query = from Users in _context.Users select Users;
            foreach (User _user in query)
            {
                _user.Balance = newbalance;
            }
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut("Set player's balance by id"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> SetOneBalance(int id, int newbalance)
        {
            var query = from Users in _context.Users where Users.Id == id select Users;
            foreach (User user in query)
            {
                user.Balance = newbalance;
            }
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
