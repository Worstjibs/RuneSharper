using RuneSharper.Shared.Enums;
using RuneSharper.Domain.Helpers;

namespace RuneSharper.Application.Models;

public class ChangeModel<T> where T : class
{
    public FrequencyModel? Frequency { get; set; }
    public DateRange? DateRange { get; set; }
    public T? Model { get; set; }

    public ChangeModel() { }

    public ChangeModel(FrequencyType frequency, DateRange dateRange, T? model)
    {
        Frequency = new FrequencyModel(frequency);
        DateRange = dateRange;
        Model = model;
    }
}
