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
using Microsoft.AspNetCore.Authorization;
using Absalyamov_WEB2.Services;
using Absalyamov_WEB2.Dto;

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCardsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserService _playercardservice;

        public PlayerCardsController(DataContext context, IUserService playercardservice)
        {
            _context = context;
            _playercardservice = playercardservice;
        }

        // GET: api/PlayerCards
        [HttpGet, Authorize(Roles = "Admin,Noob")]
        public async Task<ActionResult<IEnumerable<PlayerCard>>> GetPlayerCards()
        {
            return await _context.PlayerCards.ToListAsync();
        }

        // GET: api/PlayerCards/5
        [HttpGet("{id}"), Authorize(Roles = "Admin,Noob")]
        public async Task<ActionResult<PlayerCard>> GetPlayerCard(int id)
        {
            var playerCard = await _context.PlayerCards.FindAsync(id);

            if (playerCard == null)
            {
                return NotFound();
            }

            return playerCard;
        }

        // PUT: api/PlayerCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPlayerCard(int id, PlayerCard playerCard)
        {
            if (id != playerCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerCardExists(id))
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

        // POST: api/PlayerCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerCard>> PostPlayerCard(PlayerCardDto request)
        {
            PlayerCard playercard = new PlayerCard();
            playercard.Surname = request.Surname;
            playercard.Name = request.Name;
            playercard.Country = request.Country;
            playercard.Quality = request.Quality;
            if (request.Quality == "Golden")
                playercard.Price = 3000;
            else if (request.Quality == "Silver")
                playercard.Price = 1000;
            else if (playercard.Quality == "Bronze")
                playercard.Price = 500;
            else return BadRequest("Data isn't correct");

            _context.PlayerCards.Add(playercard);
            await _context.SaveChangesAsync();

            return Ok(_context.PlayerCards);
        }

        // DELETE: api/PlayerCards/5
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlayerCard(int id)
        {
            var playerCard = await _context.PlayerCards.FindAsync(id);
            if (playerCard == null)
            {
                return NotFound();
            }

            _context.PlayerCards.Remove(playerCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetPlayersByCountry")]
        public async Task<IActionResult> GetPlayersByCountry(string countryname)
        {
            var players = await _playercardservice._GetPlayersByCountry(countryname);
            return Ok(players);
        }

        [HttpGet("GetPlayersByName")]
        public async Task<IActionResult> GetPlayersByName(string name)
        {
            var players = await _playercardservice._GetPlayersByName(name);
            return Ok(players);
        }

        [HttpGet("GetPlayersBySurname")]
        public async Task<IActionResult> GetPlayersBySurname(string surname)
        {
            var players = await _playercardservice._GetPlayersBySurname(surname);
            return Ok(players);
        }

        [HttpGet("GetPlayersByQuality")]
        public async Task<IActionResult> GetPlayersByQuality(string quality)
        {
            var players = await _playercardservice._GetPlayersByQuality(quality);
            return Ok(players);
        }

        private bool PlayerCardExists(int id)
        {
            return _context.PlayerCards.Any(e => e.Id == id);
        }
    }
}
