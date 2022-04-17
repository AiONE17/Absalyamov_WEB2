namespace Absalyamov_WEB2.Services
{
    public class PlayerCardService : IPlayerCardService
    {
        private readonly DataContext _context;

        public PlayerCardService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<PlayerCard>> GetPlayersByCountry(string countryname)
        {
            var sub = from PlayerCards in _context.PlayerCards where PlayerCards.Country == countryname select new { PlayerCards.Name, PlayerCards.Surname };
            return (List<PlayerCard>)sub;
        }

    }
}
