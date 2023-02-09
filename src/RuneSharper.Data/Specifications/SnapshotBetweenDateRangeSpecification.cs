using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Domain.Helpers;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Data.Specifications;

internal class SnapshotBetweenDateRangeSpecification : Specification<Snapshot>
{
    public SnapshotBetweenDateRangeSpecification(string userName, DateRange dateRange, FirstLast firstLast) 
        : base(snapshot => 
            dateRange.DateFrom <= snapshot.DateCreated &&
            snapshot.DateCreated <= dateRange.DateTo &&
            snapshot.Character.UserName == userName)
    {
        AddInclude(s => s.Character);
        AddInclude(s => s.Skills);
        AddInclude(s => s.Activities);

        if (firstLast == FirstLast.First)
            AddOrderBy(snapshot => snapshot.DateCreated);
        else
            AddOrderByDescending(snapshot => snapshot.DateCreated);

        Take = 1;
    }
}
