namespace Managers
{
    public enum PlayerMoves
    {
        NONE,
        WR_RUSH, WR_CURL, WR_SLANT,
        QB_RUSH, QB_PASS,
    }

    public class PlayerMove
    {
        public PlayerMoves Move { get; }
        public float Duration { get; }
        
        public PlayerMove(PlayerMoves move, float duration)
        {
            Move = move;
            Duration = duration;
        }
    }
}