namespace Absalyamov_WEB2.Services.UserCardRelationships
{
    public interface IUserCardRelationshipsService
    {
        Task<IQueryable> _ShowMyPlayers(int _UserID);
        Task<float> GetTeamRating(int _UserID);
        Task<IQueryable> _GetTierList();
        void SetNewUserBalance(int _UserID, int _CardID, int _NewBalance);
        int GetUserID(string name);
        int GetUserBalance(string name);
        int GetCardPrice(int CardID);
        string GetCardQuality(int CardID);
    }
}
