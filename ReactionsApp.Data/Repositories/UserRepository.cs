using Microsoft.EntityFrameworkCore;
using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ReactionsAppDbContext context) : base(context)
        {
        }
    }
}
