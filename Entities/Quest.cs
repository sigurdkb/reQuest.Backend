using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reQuest.Backend.Entities
{
	public enum QuestState
    {
		Done,
        Active,
        TimedOut,
		Approved
        
    };

    public class Quest
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the title
		/// </summary>
		/// <value>The quest title.</value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the description
		/// </summary>
		/// <value>A textual description of the quest</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the state
		/// </summary>
		/// <value>The state of this quest.</value>
        public QuestState State { get; set; } = QuestState.Active;

		/// <summary>
		/// Gets or sets the timestamp
		/// </summary>
		/// <value>The timestamp of the creation of this quest.</value>
        public DateTime Timestamp { get; set; }

		/// <summary>
		/// Gets or sets the timeout
		/// </summary>
		/// <value>The time remaining to access this quest.</value>
        public TimeSpan Timeout { get; set; }

		/// <summary>
		/// Gets or sets the activeplayers
		/// </summary>
		/// <value>A List of the active players.</value>
		public ICollection<Player> ActivePlayers { get; set; } = new List<Player>();

		/// <summary>
		/// Gets or sets the passiveplayers
		/// </summary>
		/// <value>A List of the passive players.</value>
		public ICollection<Player> PassivePlayers { get; set; } = new List<Player>();

		// Navigational properties

		/// <summary>
		/// Gets or sets the topic
		/// </summary>
		/// <value>The topic associated with this quest.</value>
		[ForeignKey(nameof(TopicId))]
		public Topic Topic { get; set; }
		public string TopicId { get; set; }

		/// <summary>
		/// Gets or sets the owner
		/// </summary>
		/// <value>The quest owner.</value>
		[ForeignKey(nameof(OwnerId))]
		public Player Owner { get; set; }
		public string OwnerId { get; set; }
		/// <summary>
		/// Gets or sets the winner
		/// </summary>
		/// <value>The quest winner.</value>
		[ForeignKey(nameof(WinnerId))]
		public Player Winner { get; set; }
		public string WinnerId { get; set; }

    }
}
