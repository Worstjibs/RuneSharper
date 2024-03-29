﻿using RuneSharper.Shared.Enums;
using RuneSharper.Domain.Helpers;

namespace RuneSharper.Application.Models;

public class StatsChangeModel : ChangeModel<StatsModel>
{
    public StatsChangeModel() { }

    public StatsChangeModel(FrequencyType frequency, DateRange dateRange, StatsModel? model)
        : base(frequency, dateRange, model)
    {
    }
}
