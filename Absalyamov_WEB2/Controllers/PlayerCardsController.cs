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

namespace Absalyamov_WEB2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCardsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPlayerCardService _playercardservice;

        public PlayerCardsController(DataContext context, IPlayerCardService playercardservice)
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
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<PlayerCard>> PostPlayerCard(PlayerCard playerCard)
        {
            _context.PlayerCards.Add(playerCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerCard", new { id = playerCard.Id }, playerCard);
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
        public async Task<IActionResult> Get(string countryname)
        {
            return Ok(await _playercardservice.GetPlayersByCountry(countryname));
        }


        private bool PlayerCardExists(int id)
        {
            return _context.PlayerCards.Any(e => e.Id == id);
        }
    }
}
