using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Data.Entities
{
    public class RandomPointsGameResult : BaseEntity
    {
        [Required]
        public int Score { get; set; }

        [Required]
        public User Player { get; set; }
    }
}
