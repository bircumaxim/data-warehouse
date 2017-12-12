using MediatR;

namespace Game.Domain.Events
{
    public class GameStartedDomainEvent : INotification
    {
        public Aggregates.GameAggregate.Game Game { get; set; }

        public GameStartedDomainEvent(Aggregates.GameAggregate.Game game)
        {
            Game = game;
        }
    }
}