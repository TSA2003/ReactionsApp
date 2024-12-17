using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Dtos
{
    public class RandomPointsGameResultDto : BaseDto
    {
        public int Score { get; set; }
        public User Player { get; set; }
    }
}
