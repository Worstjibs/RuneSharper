using System.ComponentModel.DataAnnotations;

namespace RuneSharper.Shared.Enums;

public enum FrequencyType
{
    Day,
    Week,
    [Display(Name = "2 Weeks")]
    Fortnight,
    Month, 
    [Display(Name = "3 Months")]
    Quarter,
    Year
}
