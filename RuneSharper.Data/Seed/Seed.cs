using Microsoft.EntityFrameworkCore;

namespace RuneSharper.Data.Seed {
    public static class Seed {
        public static async Task SeedDataAsync(RuneSharperContext context) {
            if (await context.Characters.AnyAsync()) {
                return;
            }
        }
    }
}
