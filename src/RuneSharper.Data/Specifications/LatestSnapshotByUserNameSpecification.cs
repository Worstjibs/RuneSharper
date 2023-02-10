using RuneSharper.Domain.Entities.Snapshots;

namespace RuneSharper.Data.Specifications;

internal class LatestSnapshotByUserNameSpecification : Specification<Snapshot>
{
    public LatestSnapshotByUserNameSpecification(string userName)
        : base(snapshot => snapshot.Character.UserName == userName)
    {
        Apply();
    }

    private void Apply()
    {
        AddInclude(s => s.Skills);
        AddInclude(s => s.Activities);

        AddOrderByDescending(s => s.DateCreated);

        Take = 1;
    }
}
