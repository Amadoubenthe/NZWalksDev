﻿using NZWalksDev.DataAccess.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalksDev.DataAccess.Models.DTO
{
    public class WalkDtoRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public string LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

    }
}
