using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Data.Seed {
    public static class Seed
    {
        private const int NUMBER_OF_DAYS = 90;

        private static readonly string[] ACCOUNT_NAMES = { "worstjibs" };

        public static async Task SeedDataAsync(RuneSharperContext context) {

            if (await context.Characters.AnyAsync()) {
                return;
            }

            var random = new Random();

            foreach (var name in ACCOUNT_NAMES)
            {
                var character = new Character
                {
                    UserName = name
                };

                var snapshots = new List<Snapshot>();
                Snapshot lastSnapshot = null;
                for (int i = NUMBER_OF_DAYS; i > 0; i--) {
                    var newSnapshot = new Snapshot
                    {
                        Character = character,
                        DateCreated = DateTime.UtcNow.AddDays(-i)
                    };

                    var skillSnapshots = Enum.GetValues(typeof(SkillType)).Cast<SkillType>()
                        .Select(x => new SkillSnapshot
                        {
                            Snapshot = newSnapshot,
                            Type = x,
                            Experience = lastSnapshot != null ? lastSnapshot.Skills.First(s => s.Type == x).Experience + random.Next(10000) : 0
                        }).ToList();

                    newSnapshot.Skills = skillSnapshots;

                    snapshots.Add(newSnapshot);
                    lastSnapshot = newSnapshot;

                    context.Characters.Add(character);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
