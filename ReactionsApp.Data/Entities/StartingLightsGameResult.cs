using ReactionsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Data.Entities
{
    public class StartingLightsGameResult : BaseEntity
    {
        [Required]
        public TimeSpan ReactionTime { get; set; }

        [Required]
        public StartingLightsGameMode GameMode { get; set; }

        [Required]
        public Guid PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public User Player { get; set; }
    }
}
