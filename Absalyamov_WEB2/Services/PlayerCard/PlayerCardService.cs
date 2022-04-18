using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Services
{
    public class PlayerCardService : IPlayerCardService
    {
        private readonly DataContext _context;

        public PlayerCardService(DataContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<PlayerCard>> _GetPlayersByCountry(string countryname)
        {
            var sub = from PlayerCards in _context.PlayerCards where PlayerCards.Country == countryname select PlayerCards;
            return sub;
        }

    }
}
