using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reQuest.Backend.Entities
{
    public class Team
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name of the team.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>A description of the team</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the players.
		/// </summary>
		/// <value>A List of the team's players.</value>
		public ICollection<Player> Players { get; set; } = new List<Player>();

	}
}
