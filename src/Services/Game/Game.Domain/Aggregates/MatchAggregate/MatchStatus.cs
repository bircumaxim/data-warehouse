using System;
using System.Collections.Generic;
using System.Linq;
using Game.Domain.Exceptions;

namespace Game.Domain.Aggregates.MatchAggregate
{
    public class MatchStatus : Enumeration
    {
        public static MatchStatus Pending = new MatchStatus(1, nameof(Pending).ToLowerInvariant());
        public static MatchStatus InProgress = new MatchStatus(2, nameof(InProgress).ToLowerInvariant());
        public static MatchStatus Completed = new MatchStatus(3, nameof(Completed).ToLowerInvariant());

        protected MatchStatus()
        {
        }

        public MatchStatus(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<MatchStatus> List() => new[] {Pending, InProgress, Completed};

        public static MatchStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                RiseNewDomainException();
            }

            return state;
        }

        public static MatchStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                RiseNewDomainException();
            }

            return state;
        }

        private static void RiseNewDomainException()
        {
            var message = $"Possible values for GameStatus: {string.Join(",", List().Select(s => s.Name))}";
            throw new GameDomainException(message);
        }
    }
}