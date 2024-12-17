using ReactionsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public User Player { get; set; }
    }
}
