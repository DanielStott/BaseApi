using System;

namespace Domain.Shared.Interfaces;

public interface IEntity
{
    Guid Id { get; }
    Guid CreatedBy { get; }
    DateTime CreatedOn { get; }
    Guid LastModifiedBy { get; }
    DateTime LastModifiedOn { get; }
    bool Deleted { get; }
}