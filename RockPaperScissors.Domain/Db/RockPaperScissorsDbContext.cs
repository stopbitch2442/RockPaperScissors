using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Domain.Db
{
    public class RockPaperScissorsDbContext : DbContext
    {
        public RockPaperScissorsDbContext(DbContextOptions<RockPaperScissorsDbContext> options)
                : base(options) { }
        public DbSet<Game.Game> Game { get; set; }
    }
}
