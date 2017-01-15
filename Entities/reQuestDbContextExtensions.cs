using System.Collections.Generic;
using System.Linq;

namespace reQuest.Backend.Entities
{
    public static class reQuestDbContextExtensions
    {
        public static void EnsureSeedDataForContext(this reQuestDbContext context)
        {
            if (context.Teams.Any())
            {
                return;
            }

            var teams = new List<Team>()
            {
                new Team()
                {
                    Name = "Kvass Hjort",
                },
                new Team()
                {
                    Name = "Rik Toddy",
                },
                new Team()
                {
                    Name = "Flott Klokke",
                },
                new Team()
                {
                    Name = "Mild Kubbe"
                }
            };


            context.AddRange(teams);
            context.SaveChanges();

        }
    }
}
