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
        public TimeSpan ReactionTime { get; set; }
        public StartingLightsGameMode GameMode { get; set; }
        public User Player { get; set; }
    }
}
