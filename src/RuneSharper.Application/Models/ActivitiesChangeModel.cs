using RuneSharper.Domain.Helpers;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Models;

public class ActivitiesChangeModel : ChangeModel<ActivitiesModel>
{
    public ActivitiesChangeModel() { }

    public ActivitiesChangeModel(Frequency frequency, DateRange dateRange, ActivitiesModel? model)
        : base(frequency, dateRange, model)
    {
    }
}
