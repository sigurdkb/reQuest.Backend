using Microsoft.EntityFrameworkCore;

namespace reQuest.Backend.Entities
{
    public class reQuestDbContext : DbContext
    {
        public reQuestDbContext(DbContextOptions<reQuestDbContext> options) : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Quest> Quests { get; set; }

    }
}
