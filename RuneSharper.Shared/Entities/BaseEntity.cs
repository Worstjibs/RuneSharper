using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Entities;

public abstract class BaseEntity<T>
{
    public abstract T Id { get; init; }
    public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
