using System.ComponentModel.DataAnnotations;

namespace RuneSharper.Shared.Enums;

public enum SortDirection
{
    [Display(Name = "ASC")]
    Ascending,
    [Display(Name = "DESC")]
    Descending
}
