using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public User Player { get; set; }
    }
}
