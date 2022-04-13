namespace Absalyamov_WEB2
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public int Balance { get; set; } = 6001;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Role { get; set; } = false;
        //public List<PlayerCard> PlayerCards { get; set; }
    }
}
