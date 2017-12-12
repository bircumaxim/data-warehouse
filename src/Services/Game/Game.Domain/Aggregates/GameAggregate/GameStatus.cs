using System;
using System.Collections.Generic;
using System.Linq;
using Game.Domain.Exceptions;

namespace Game.Domain.Aggregates.GameAggregate
{
    public class GameStatus : Enumeration
    {
        public static GameStatus Pending = new GameStatus(1, nameof(Pending).ToLowerInvariant());
        public static GameStatus InProgress = new GameStatus(2, nameof(InProgress).ToLowerInvariant());
        public static GameStatus Completed = new GameStatus(3, nameof(Completed).ToLowerInvariant());

        protected GameStatus()
        {
        }

        public GameStatus(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<GameStatus> List() => new[] {Pending, InProgress, Completed};

        public static GameStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                RiseNewDomainException();
            }

            return state;
        }

        public static GameStatus From(int id)
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