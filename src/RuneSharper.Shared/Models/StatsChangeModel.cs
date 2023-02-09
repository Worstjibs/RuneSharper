﻿using RuneSharper.Shared.Enums;
using RuneSharper.Domain.Helpers;

namespace RuneSharper.Shared.Models;

public class StatsChangeModel : ChangeModel<StatsModel>
{
    public StatsChangeModel() { }

    public StatsChangeModel(Frequency frequency, DateRange dateRange, StatsModel? model)
        : base(frequency, dateRange, model)
    {
    }
}
