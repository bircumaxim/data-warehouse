using System;

namespace Game.Domain.Aggregates.ChessPlayerAggregate
{
    public class ChessPlayer : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ChessPlayerLevel ChessPlayerLevel { get; }
        
        public ChessPlayer(string identity, string firstName, string lastName, ChessPlayerLevel chessPlayerLevel)
        {
            ChessPlayerLevel = chessPlayerLevel;
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
        }
    }
}