using System;

namespace Banking.Data.Entitites
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}