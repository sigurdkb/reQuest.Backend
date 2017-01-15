using System.Collections.Generic;
using reQuest.Backend.Entities;

namespace reQuest.Backend.Services
{
    public interface IreQuestRepository
    {
        bool Commit();
        IEnumerable<Team> GetTeams();
        IEnumerable<Player> GetPlayers();
        Player GetPlayerFromId(string id);
        // double GetPlayerScore(string id);
        Player GetPlayerFromExternalId(string externalId);
        bool PlayerExists(string externalId);
        void AddPlayer(Player player);
        IEnumerable<Topic> GetTopics();
        Topic GetTopicFromExternalId(string externalId);
        Topic GetTopicFromId(string id);
        bool TopicExists(string id);
        void AddTopic(Topic topic);
        IEnumerable<Quest> GetQuests();
        void AddQuest(Quest quest);

    }
}
