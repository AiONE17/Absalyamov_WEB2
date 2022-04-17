using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Admin : ControllerBase
    {
        private readonly DataContext _context;
        public Admin(DataContext context)
        {
            _context = context;

        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut("GiveAdminRoleToSomeoneById"), Authorize(Roles = "Admin")]
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

        [HttpPut("RemoveAdminRoleFromSomeoneById"), Authorize(Roles = "Admin")]
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


        [HttpDelete("DeleteUsersTeams"), Authorize(Roles = "Admin")]
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

        [HttpDelete("DeletePlayerCards"), Authorize(Roles = "Admin")]
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

        [HttpDelete("ClearRating"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<PlayerCard>>> DeleteRating()
        {
            var query = from Ratings in _context.Ratings select Ratings;
            foreach (Absalyamov_WEB2.Rating _rating in query)
            {
                _context.Ratings.RemoveRange(_rating);
            }
            await _context.SaveChangesAsync();
            return Ok("Rating is deleted");
        }

        [HttpPut("SetAllPlayersBalance"), Authorize(Roles = "Admin")]
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

        [HttpPut("SetPlayersBalanceById"), Authorize(Roles = "Admin")]
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
