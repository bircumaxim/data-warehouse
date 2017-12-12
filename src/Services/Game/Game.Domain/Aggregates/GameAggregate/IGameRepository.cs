using System.Threading.Tasks;

namespace Game.Domain.Aggregates.GameAggregate
{
    public interface IGameRepository : IRepository<GameAggregate.Game>
    {
        GameAggregate.Game Add(GameAggregate.Game order);
        
        void Update(GameAggregate.Game order);

        Task<GameAggregate.Game> GetAsync(int orderId);
    }
}