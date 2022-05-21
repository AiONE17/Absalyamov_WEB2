namespace Absalyamov_WEB2
{
    public class UserCardRelationship
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int PlayerCardID { get; set; }
        public int Pace { get; set; }
        public int Shooting { get; set; }
        public int Passing { get; set; }
        public int Dribling { get; set; }
        public int Defending { get; set; }
        public int Physical { get; set; }
        public User User { get; set; }
        public PlayerCard PlayerCard { get; set; }

    }
}
