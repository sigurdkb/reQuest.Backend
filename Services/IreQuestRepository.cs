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
        IEnumerable<Player> GetPlayersWithTopic(Topic topic);
        Player GetPlayerFromId(string id);
        Player GetPlayerFromExternalId(string externalId);
        bool PlayerExists(string externalId);
        void AddPlayer(Player player);
        IEnumerable<Topic> GetTopics();
        Topic GetTopicFromExternalId(string externalId);
        Topic GetTopicFromId(string id);
        bool TopicExists(string id);
        void AddTopic(Topic topic);
        IEnumerable<Quest> GetQuests(QuestState stateFilter = (QuestState.Active | QuestState.Done | QuestState.TimedOut | QuestState.Approved));
        IEnumerable<Quest> GetPlayerQuests(Player player);
        Quest GetQuest(string id);
        void AddQuest(Quest quest);
        void DeleteQuest(Quest quest);
    }
}
