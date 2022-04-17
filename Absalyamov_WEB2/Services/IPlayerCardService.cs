namespace Absalyamov_WEB2.Services
{
    public interface IPlayerCardService
    {
        Task<List<PlayerCard>> GetPlayersByCountry(string countryname);
    }
}
