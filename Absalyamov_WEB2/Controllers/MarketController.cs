using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly DataContext _context;
        public MarketController(DataContext context)
        {
            _context = context;

        }

        [HttpGet("Get players by id"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<PlayerCard>> Get(int id)
        {
            var sub = await _context.PlayerCards.FindAsync(id);
            if (sub == null)
                return BadRequest("Country not found :(");
            return Ok(sub);
        }

        [HttpGet("Get players by country"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<PlayerCard>> SuperPuperGet(string countryname)
        {
            var sub = from PlayerCards in _context.PlayerCards where PlayerCards.Country == countryname select new { PlayerCards.Name, PlayerCards.Surname };
            return Ok(sub);
        }

        [HttpGet("Get all players on Market"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<List<PlayerCard>>> Get()
        {
            return Ok(await _context.PlayerCards.ToListAsync());
        }

        [HttpPut("Buy player by id"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<string>> BuyPlayer(int _CardID)
        {
            UserCardRelationship Relationship = new UserCardRelationship();
            Random rnd = new Random();
            int _UserID = GetUserID(User.Identity.Name);

            int count_of_players = (from PlayerCards in _context.PlayerCards where _CardID == PlayerCards.Id select _UserID).Count();
            if (count_of_players == 1)
            {
                string CardQuality = GetCardQuality(_CardID);
                int q;
                if (CardQuality == "Gold") q = 80;
                else if (CardQuality == "Silver") q = 65;
                else if (CardQuality == "Bronze") q = 50;
                else q = 0;

                Relationship.UserID = _UserID;
                Relationship.CardID = _CardID;
                Relationship.Pace = rnd.Next(q, 100);
                Relationship.Shooting = rnd.Next(q, 100);
                Relationship.Defending = rnd.Next(q, 100);
                Relationship.Dribling = rnd.Next(q, 100);
                Relationship.Passing = rnd.Next(q, 100);
                Relationship.Physical = rnd.Next(q, 100);
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
    }
}
