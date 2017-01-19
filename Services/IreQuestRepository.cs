using System.Collections.Generic;
using reQuest.Backend.Entities;

namespace reQuest.Backend.Services
{
    public interface IreQuestRepository
    {
        bool Commit();
        IEnumerable<Team> GetTeams();
        Team GetRandomTeam();
        IEnumerable<Player> GetPlayers();
        Player GetPlayerFromId(string id);
        Player GetPlayerFromExternalId(string externalId);
        bool PlayerExists(string externalId);
        void AddPlayer(Player player);
        IEnumerable<Topic> GetTopics();
        Topic GetTopicFromExternalId(string externalId);
        Topic GetTopicFromId(string id);
        bool TopicExists(string id);
        void AddTopic(Topic topic);
        IEnumerable<Quest> GetQuests(QuestState stateFilter = QuestState.Active);
        IEnumerable<Quest> GetPlayerQuests(Player player);
        void AddQuest(Quest quest);

    }
}
