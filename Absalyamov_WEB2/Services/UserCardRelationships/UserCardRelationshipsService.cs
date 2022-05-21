using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Absalyamov_WEB2.Services.UserCardRelationships
{
    public class UserCardRelationshipsService : IUserCardRelationshipsService
    {
        private readonly DataContext _context;
        private IQueryable q;

        public UserCardRelationshipsService(DataContext context)
        {
            _context = context;
        }
        public async Task<IQueryable> _ShowMyPlayers(int _UserID)
        {
            var query = from PlayerCards in _context.PlayerCards
                        join UserCardRelationships in _context.UserCardRelationships
                        on PlayerCards.Id equals UserCardRelationships.PlayerCardID
                        where UserCardRelationships.UserID == _UserID
                        select new
                        {
                            PlayerCards.Name,
                            PlayerCards.Surname,
                            UserCardRelationships.Pace,
                            UserCardRelationships.Shooting,
                            UserCardRelationships.Defending,
                            UserCardRelationships.Passing,
                            UserCardRelationships.Physical,
                            UserCardRelationships.Dribling
                        };
            return query;
        }
        public async Task<float> GetTeamRating(int _UserID)
        {
            int sum = _context.UserCardRelationships.Where(o => o.UserID == _UserID).Sum(o => o.Pace + o.Shooting + o.Dribling + o.Passing + o.Physical + o.Defending);
            float rating = ((float)sum) / 30;
            return rating;
        }
        public async Task<IQueryable> _GetTierList()
        {
            var query = from Users in _context.Users
                        join UserCardRelationships in _context.UserCardRelationships
                        on Users.Id equals UserCardRelationships.UserID
                        where Users.RegisteredToTierList == true
                        group UserCardRelationships by new { Users.Username, UserCardRelationships.UserID } into tier
                        select new
                        {
                            Username = tier.Key.Username,
                            Rating = tier.Sum(o => o.Pace + o.Shooting + o.Dribling + o.Passing + o.Physical + o.Defending) / 30
                        };
            return query;
        }
        public void SetNewUserBalance(int _UserID, int _CardID, int _NewBalance)
        {
            var query = from Users in _context.Users where Users.Id == _UserID select Users;
            foreach (User user in query)
            {
                user.Balance = _NewBalance;
            }
        }

        public int GetUserID(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Id;
            int id = query.FirstOrDefault();
            return id;
        }

        public int GetUserBalance(string name)
        {
            IQueryable<int> query = from Users in _context.Users where Users.Username == name select Users.Balance;
            int _Balance = query.FirstOrDefault();
            return _Balance;
        }

        public int GetCardPrice(int CardID)
        {
            IQueryable<int> query = from PlayerCard in _context.PlayerCards where PlayerCard.Id == CardID select PlayerCard.Price;
            int _Price = query.FirstOrDefault();
            return _Price;
        }
        public string GetCardQuality(int CardID)
        {
            IQueryable<string> query = from PlayerCard in _context.PlayerCards where PlayerCard.Id == CardID select PlayerCard.Quality;
            string _Quality = query.First();
            return _Quality;
        }
    }
}
