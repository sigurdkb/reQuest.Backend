using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reQuest.Backend.Entities;

namespace reQuest.Backend.Services
{
    public class reQuestRepository : IreQuestRepository
    {
        private reQuestDbContext _context;

        public reQuestRepository(reQuestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players;
        }
        public void AddPlayer(Player player)
        {
            var result = _context.Players.Add(player);
        }

        public IEnumerable<Team> GetTeams()
        {
            return _context.Teams
                .Include(t => t.Players)
                .ThenInclude(p => p.Competencies);
        }

        //TODO: Randomize team assignment
        public Team GetRandomTeam()
        {
            return _context.Teams.FirstOrDefault();
        }

        public bool PlayerExists(string externalId)
        {
            return _context.Players.Any(p => p.ExternalId == externalId);
        }

        public IEnumerable<Topic> GetTopics()
        {
            return _context.Topics;
        }

        public bool TopicExists(string id)
        {
            return _context.Topics.Any(t => t.ExternalId == id || t.Id == id);
        }

        public void AddTopic(Topic topic)
        {
            var result = _context.Topics.Add(topic);
        }

        public bool Commit()
        {
            return (_context.SaveChanges() > 0);
        }

        public Topic GetTopicFromExternalId(string externalId)
        {
            return _context.Topics.SingleOrDefault(t => t.ExternalId == externalId);
        }

        public Player GetPlayerFromExternalId(string externalId)
        {
            return _context.Players
                .Include(p => p.Competencies)
                    .ThenInclude(c => c.Topic)
                .SingleOrDefault(p => p.ExternalId == externalId);
        }

        public Player GetPlayerFromId(string id)
        {
            return _context.Players
                .Include(p => p.Team)
                .Include(p => p.Competencies)
                    .ThenInclude(c => c.Topic)
                .SingleOrDefault(p => p.Id == id);
        }
        /// <summary>
        /// Retrieves a list of quests filtered by state
        /// </summary>
        /// <param name="stateFilter">Flag enumeration filter, default include all states</param>
        /// <returns></returns>
        public IEnumerable<Quest> GetQuests(QuestState stateFilter = (QuestState.Active | QuestState.Done | QuestState.TimedOut | QuestState.Approved))
        {            
            return _context.Quests
                .Include(q => q.Topic)
                .Include(q => q.Owner)
                .Where(q => q.State == (q.State & stateFilter));
        }
        
        public IEnumerable<Quest> GetPlayerQuests(Player player)
        {
            var result = _context.Quests
                .Include(q => q.Topic)
                .Include(q => q.Owner)
                .Include(q => q.Winner)
                // Get all quests where either QuestState is Active or QuestState is anything but Active and the player is the owner
                .Where(q => q.State == QuestState.Active || q.Owner == player || q.Winner == player);

            return result;
        }
        

        public Topic GetTopicFromId(string id)
        {
            return _context.Topics
                .SingleOrDefault(t => t.Id == id);
        }

        public void AddQuest(Quest quest)
        {
            var result = _context.Quests.Add(quest);
        }

        public Quest GetQuest(string id)
        {
            return _context.Quests
                            .Include(q => q.Topic)
                            .SingleOrDefault(q => q.Id == id);
        }

        public void DeleteQuest(Quest quest)
        {
            var result = _context.Quests.Remove(quest);
        }

        public IEnumerable<Player> GetPlayersWithTopic(Topic topic)
        {
            return _context.Players
                            .Where(p => p.Competencies.Any(c => c.Topic == topic));
        }
    }
}
