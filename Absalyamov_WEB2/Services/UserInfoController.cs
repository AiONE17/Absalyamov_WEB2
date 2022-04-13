using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly DataContext _context;
        public UserInfoController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("ShowMyPlayersPleeeeeeaase"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<string>> GetMyPlayers()
        {
            UserCardRelationship Relationship = new UserCardRelationship();
            Random rnd = new Random();
            int _UserID = GetUserID(User.Identity.Name);

            var query = from PlayerCards in _context.PlayerCards join UserCardRelationships in _context.UserCardRelationships
                        on PlayerCards.Id equals UserCardRelationships.CardID where UserCardRelationships.UserID == _UserID
                        select new { 
                        PlayerCards.Name, 
                        PlayerCards.Surname, 
                        UserCardRelationships.Pace,
                        UserCardRelationships.Shooting,
                        UserCardRelationships.Defending,
                        UserCardRelationships.Passing,
                        UserCardRelationships.Physical,
                        UserCardRelationships.Dribling};
            return Ok(query);
        }

        [HttpGet("CheckMyBalancePleeeeeeaase"), Authorize(Roles = "Noob")]
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
