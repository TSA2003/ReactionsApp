using ReactionsApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Models
{
    public class StartingLightsGameResultModel
    {
        public TimeSpan ReactionTime { get; set; }

        public StartingLightsLaunchMode GameMode { get; set; }

        public string Username { get; set; }

        public DateTime ResultTime { get; set; }
    }
}
