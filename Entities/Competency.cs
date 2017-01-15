using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reQuest.Backend.Entities
{
    public class Competency
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the score.
		/// </summary>
		/// <value>The score optained in this topic</value>
        public double Score { get; set; }
		public bool Active { get; set; } = true;
		
		// Navigational properties

		/// <summary>
		/// Gets or sets the topic identifier.
		/// </summary>
		/// <value>The associated topic.</value>
		[ForeignKey(nameof(TopicId))]
		public Topic Topic { get; set; }
		public string TopicId { get; set; }
    }
}