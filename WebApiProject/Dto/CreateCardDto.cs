using WebApiProject.DTOs;

namespace WebApiProject.Dto
{
    public class CreateCardDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public decimal HitSpeed { get; set; }

        public CreateElixirUsageDto ElixirUsage { get; set; }
        public CreateSpeedDto Speed { get; set; }
        public CreateRarityDto Rarity { get; set; }
        public CreateTargetDto Target { get; set; }
        public CreateTypeDto Type { get; set; }

    }
}
