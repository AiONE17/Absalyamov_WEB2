using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Services
{
    public class User : IUserService
    {
        private readonly DataContext _context;

        public User(DataContext context)
        {
            _context = context;
        }
    }
}
