using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Shared.Models;

public class ChangeModel<T> where T : class
{
    public Frequency? Frequency { get; set; }
    public DateRange? DateRange { get; set; }
    public T? Model { get; set; }

    public ChangeModel() { }

    public ChangeModel(Frequency frequency, DateRange dateRange, T? model)
    {
        Frequency = frequency;
        DateRange = dateRange;
        Model = model;
    }
}
