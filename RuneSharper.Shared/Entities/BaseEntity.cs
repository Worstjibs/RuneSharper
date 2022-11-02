using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Entities;

public abstract class BaseEntity<T> : BaseEntity
{
    public abstract T Id { get; init; }
}

public class BaseEntity { }
