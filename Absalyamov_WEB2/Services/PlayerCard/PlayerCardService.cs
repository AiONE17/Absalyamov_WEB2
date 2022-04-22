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
        public async Task<IQueryable<PlayerCard>> _GetPlayersByName(string name)
        {
            var sub = from PlayerCards in _context.PlayerCards where PlayerCards.Name == name select PlayerCards;
            return sub;
        }
        public async Task<IQueryable<PlayerCard>> _GetPlayersBySurname(string surname)
        {
            var sub = from PlayerCards in _context.PlayerCards where PlayerCards.Surname == surname select PlayerCards;
            return sub;
        }
        public async Task<IQueryable<PlayerCard>> _GetPlayersByQuality(string quality)
        {
            var sub = from PlayerCards in _context.PlayerCards where PlayerCards.Quality == quality select PlayerCards;
            return sub;
        }

    }
}
