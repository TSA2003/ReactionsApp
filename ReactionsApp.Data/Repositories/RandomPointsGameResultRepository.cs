using Microsoft.EntityFrameworkCore;
using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Data.Repositories
{
    public class RandomPointsGameResultRepository : BaseRepository<RandomPointsGameResult>
    {
        public RandomPointsGameResultRepository(DbContext context) : base(context)
        {
        }
    }
}
