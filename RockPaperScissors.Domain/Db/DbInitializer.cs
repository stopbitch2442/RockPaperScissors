using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Domain.Db
{
    public class DbInitializer
    {
        public static void Initialize(RockPaperScissorsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
