using System;
using Game.Domain.Events;
using Game.Domain.Exceptions;

namespace Game.Domain.Aggregates.GameAggregate
{
    public class Game : Entity, IAggregateRoot
    {
        private DateTime _gameDate;
        public GameStatus GameStatus { get; }
        private int _gameStatusId;

        private string _gameInitiatorId;
        private string _secondPartenerId;

        private string _description;

        public Game(string gameInitiatorId)
        {
            _gameInitiatorId = gameInitiatorId;
            _gameDate = DateTime.UtcNow;
            _gameStatusId = GameStatus.Pending.Id;

            AddGameStartedDomainEvent();
        }

        private void AddGameStartedDomainEvent()
        {
            var gameStartedDomainEvent = new GameStartedDomainEvent(this);
            AddDomainEvent(gameStartedDomainEvent);
        }

        public void JoinGame(string secondPartenerId)
        {
            _secondPartenerId = secondPartenerId;
            SetInProgressStatus();
        }

        private void SetInProgressStatus()
        {
            if (_gameStatusId != GameStatus.Pending.Id)
            {
                StatusChangeException(GameStatus.InProgress);
            }

            _gameStatusId = GameStatus.InProgress.Id;
            _description = "The game was set in progress.";
        }

        private void SetCompletedStatus()
        {
            if (_gameStatusId != GameStatus.InProgress.Id)
            {
                StatusChangeException(GameStatus.Completed);
            }

            _gameStatusId = GameStatus.Completed.Id;
            _description = "The game was completed.";
        }

        private void StatusChangeException(GameStatus gameStatusToChange)
        {
            throw new GameDomainException(
                $"Is not possible to change the game status from {GameStatus.Name} to {gameStatusToChange.Name}.");
        }
    }
}