using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingGGGGController : ControllerBase
    {
        private readonly DataContext _context;
        public RatingGGGGController(DataContext context)
        {
            _context = context;

        }

        [HttpGet("GetRatingOfMyTeam"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<string>> GetTeamRating()
        {
            int _UserID = GetUserID(User.Identity.Name);
            int sum = _context.UserCardRelationships.Where(o => o.UserID == _UserID).Sum(o => o.Pace + o.Shooting + o.Dribling + o.Passing + o.Physical + o.Defending);
            float rating = ((float)sum)/30;
            return Ok(rating);
        }

        [HttpPost("RegisterMyTeamToRating"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<string>> RegisterTeam()
        {
            Rating Rate = new Rating();
            Rate.Username = User.Identity.Name;
            int _UserID = GetUserID(User.Identity.Name);
            int sum = _context.UserCardRelationships.Where(o => o.UserID == _UserID).Sum(o => o.Pace + o.Shooting + o.Dribling + o.Passing + o.Physical + o.Defending);
            float rating = ((float)sum) / 30;
            Rate.Rateofteam = rating;
            _context.Ratings.Add(Rate);
            await _context.SaveChangesAsync();
            return Ok(Rate);
        }

        [HttpGet("GetListOfRegisteredTeamsByDescending"), Authorize(Roles = "Noob")]
        public async Task<ActionResult<string>> GetTeams()
        {
            var TeamList = from Ratings in _context.Ratings
                            orderby Ratings.Rateofteam descending
                            select new { Ratings.Username, Ratings.Rateofteam};
            return Ok(TeamList);
        }
        private int GetUserID(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Id;
            int id = query.FirstOrDefault();
            return id;
        }

    }
}
