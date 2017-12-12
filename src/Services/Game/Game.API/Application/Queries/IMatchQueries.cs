using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Domain.Aggregates.MatchAggregate;

namespace Game.API.Application.Queries
{
    public interface IMatchQueries
    {
        Task<Match> GetMatchAsync(int id);

        Task<IEnumerable<Match>> GetMatchesAsync();
    }
}