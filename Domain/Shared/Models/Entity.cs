using System;
using Domain.Shared.Interfaces;

namespace Domain.Shared.Models;

public class Entity : IEntity
{
    public Guid Id { get; private init; }
    public Guid CreatedBy { get; private init; }
    public DateTime CreatedOn { get; private init; }
    public Guid LastModifiedBy { get; private set; }
    public DateTime LastModifiedOn { get; private set; }
    public bool Deleted { get; private set; }

    protected Entity()
    {
        // TODO: Take in a UserSession to see who created/modified
        var entityId = Guid.NewGuid();
        Id = entityId;
        CreatedBy = entityId;
        CreatedOn = DateTime.Now;
        LastModifiedBy = entityId;
        LastModifiedOn = DateTime.Now;
        Deleted = false;
    }
}