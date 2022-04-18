using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Services
{
    public interface IPlayerCardService
    {
        Task<IQueryable<PlayerCard>> _GetPlayersByCountry(string countryname);
    }
}
