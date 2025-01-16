using Microsoft.EntityFrameworkCore;
using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Data.Repositories
{
    public class StartingLightsGameResultRepository : BaseRepository<StartingLightsGameResult>
    {
        public StartingLightsGameResultRepository(ReactionsAppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<StartingLightsGameResult>> GetAllAsync()
        {
            return _dbSet.Include(p => p.Player);
        }
    }
}
