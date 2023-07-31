using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.Db;

namespace RockPaperScissors.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RockPaperScissorsDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
