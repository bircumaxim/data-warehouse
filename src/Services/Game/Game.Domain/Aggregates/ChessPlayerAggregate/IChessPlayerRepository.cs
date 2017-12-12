using System.Threading.Tasks;

namespace Game.Domain.Aggregates.ChessPlayerAggregate
{
    public interface IChessPlayerRepository
    {
        ChessPlayer Add(ChessPlayer buyer);
        ChessPlayer Update(ChessPlayer buyer);
        Task<ChessPlayer> FindAsync(string chessPlayerIdentityGuid);
    }
}