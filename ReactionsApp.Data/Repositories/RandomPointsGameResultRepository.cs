﻿using Microsoft.EntityFrameworkCore;
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
        public RandomPointsGameResultRepository(ReactionsAppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<RandomPointsGameResult>> GetAllAsync()
        {
            return _dbSet.Include(p => p.Player);
        }
    }
}
