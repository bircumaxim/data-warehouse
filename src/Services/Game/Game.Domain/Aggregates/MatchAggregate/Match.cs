using System;
using Game.Domain.Events;
using Game.Domain.Exceptions;

namespace Game.Domain.Aggregates.MatchAggregate
{
    public class Match : Entity, IAggregateRoot
    {
        private DateTime _matchDate;
        public MatchStatus MatchStatus { get; }
        private int _matchStatusId;

        private string _matchInitiatorId;
        private string _secondPartenerId;

        private string _description;

        public Match(string matchInitiatorId)
        {
            _matchInitiatorId = matchInitiatorId;
            _matchDate = DateTime.UtcNow;
            _matchStatusId = MatchStatus.Pending.Id;

            AddMatchStartedDomainEvent();
        }

        private void AddMatchStartedDomainEvent()
        {
            var gameStartedDomainEvent = new MatchStartedDomainEvent(this);
            AddDomainEvent(gameStartedDomainEvent);
        }

        public void JoinGame(string secondPartenerId)
        {
            _secondPartenerId = secondPartenerId;
            SetInProgressStatus();
        }

        private void SetInProgressStatus()
        {
            if (_matchStatusId != MatchStatus.Pending.Id)
            {
                StatusChangeException(MatchStatus.InProgress);
            }

            _matchStatusId = MatchStatus.InProgress.Id;
            _description = "The game was set in progress.";
        }

        private void SetCompletedStatus()
        {
            if (_matchStatusId != MatchStatus.InProgress.Id)
            {
                StatusChangeException(MatchStatus.Completed);
            }

            _matchStatusId = MatchStatus.Completed.Id;
            _description = "The game was completed.";
        }

        private void StatusChangeException(MatchStatus matchStatusToChange)
        {
            throw new GameDomainException(
                $"Is not possible to change the game status from {MatchStatus.Name} to {matchStatusToChange.Name}.");
        }
    }
}