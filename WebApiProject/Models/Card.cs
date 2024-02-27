namespace WebApiProject.Models
{
    public class Card
    {
        public int Id { get; set; }     // Primary key
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Count { get; set; }
        public decimal? HitSpeed { get; set; }

        // Foreign keys
        public int? TargetId { get; set; }
        public int? ElixirUsageId { get; set; }
        public int? RarityId { get; set; }
        public int? SpeedId { get; set; }
        public int? TypeId { get; set; }

    }
}
