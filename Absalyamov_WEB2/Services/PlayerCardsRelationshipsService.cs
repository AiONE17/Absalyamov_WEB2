namespace Absalyamov_WEB2.Services
{
    public class PlayerCardsRelationshipsService : IPlayerCardsRelationshipsService
    {
        private readonly DataContext _context;

        public PlayerCardsRelationshipsService(DataContext context)
        {
            _context = context;
        }
    }

}
