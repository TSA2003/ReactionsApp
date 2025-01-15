using ReactionsApp.Data.Entities;
using ReactionsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Dtos
{
    public class StartingLightsGameResultDto : BaseDto
    {
        [Required]
        public TimeSpan ReactionTime { get; set; }

        [Required]
        public StartingLightsGameMode GameMode { get; set; }

        [Required]
        public Guid PlayerId { get; set; }
    }
}
