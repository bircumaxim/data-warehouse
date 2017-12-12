namespace Game.Domain.Aggregates.ChessPlayerAggregate
{
    public class ChessPlayerLevel : Entity
    {
        public int WinnedGames { get; private set; }

        public ChessPlayerLevel()
        {
            WinnedGames = 0;
        }

        public void UpdateWinnedGames()
        {
            WinnedGames++;
        }
    }
}