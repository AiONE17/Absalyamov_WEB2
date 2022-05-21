using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
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

        [HttpPut("RegisterToTierList"), Authorize(Roles = "Noob,Admin")]
        public async Task<IActionResult> RegisterToTierList()
        {
            int id = GetUserID(User.Identity.Name);    
            var query = from Users in _context.Users where Users.Id == id select Users;
            foreach (User user in query)
            {
                user.RegisteredToTierList = true;
            }
            await _context.SaveChangesAsync();
            return Ok("Successfull");
        }
        [HttpGet("ShowBalance"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<string>> CheckMyBalance()
        {
            int _UserBalance = GetUserBalance(User.Identity.Name);
            return Ok(_UserBalance);
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
    }
}
