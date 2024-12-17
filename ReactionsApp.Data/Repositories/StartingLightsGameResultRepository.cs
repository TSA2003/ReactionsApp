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
        public StartingLightsGameResultRepository(DbContext context) : base(context)
        {
        }
    }
}
