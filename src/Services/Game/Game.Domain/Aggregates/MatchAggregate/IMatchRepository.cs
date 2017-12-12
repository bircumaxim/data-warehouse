using System.Threading.Tasks;

namespace Game.Domain.Aggregates.MatchAggregate
{
    public interface IMatchRepository : IRepository<Match>
    {
        Match Add(Match order);
        
        void Update(Match order);

        Task<Match> GetAsync(int orderId);
    }
}