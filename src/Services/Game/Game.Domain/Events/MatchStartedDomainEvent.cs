using Game.Domain.Aggregates.MatchAggregate;
using MediatR;

namespace Game.Domain.Events
{
    public class MatchStartedDomainEvent : INotification
    {
        public Match Match { get; set; }

        public MatchStartedDomainEvent(Match match)
        {
            Match = match;
        }
    }
}