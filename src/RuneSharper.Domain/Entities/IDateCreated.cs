﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Domain.Entities;

public interface IDateCreated
{
    public DateTime DateCreated { get; init; }
}
