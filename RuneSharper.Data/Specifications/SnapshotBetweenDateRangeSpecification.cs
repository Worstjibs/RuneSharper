

using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Specifications;

internal class SnapshotBetweenDateRangeSpecification : Specification<Snapshot>
{
    public SnapshotBetweenDateRangeSpecification(string userName, DateRange dateRange, FirstLast firstLast) : base(snapshot => 
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
