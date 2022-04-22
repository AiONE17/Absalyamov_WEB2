using Microsoft.AspNetCore.Mvc;

namespace Absalyamov_WEB2.Services
{
    public interface IPlayerCardService
    {
        Task<IQueryable<PlayerCard>> _GetPlayersByCountry(string countryname);
        Task<IQueryable<PlayerCard>> _GetPlayersByName(string name);
        Task<IQueryable<PlayerCard>> _GetPlayersBySurname(string surname);
        Task<IQueryable<PlayerCard>> _GetPlayersByQuality(string quality); 
    }
}
