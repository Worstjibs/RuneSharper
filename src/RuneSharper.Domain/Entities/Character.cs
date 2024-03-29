﻿using RuneSharper.Domain.Entities.Snapshots;

namespace RuneSharper.Domain.Entities;

public class Character : BaseIntEntity, IDateCreated
{
    public string UserName { get; set; } = default!;
    public ICollection<Snapshot> Snapshots { get; set; } = new List<Snapshot>();
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
    public bool NameChanged { get; set; }
}
