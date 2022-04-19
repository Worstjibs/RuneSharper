namespace RuneSharper.Shared.Entities {
    public record BaseIntEntity {
        public int Id { get; set; }
        public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}