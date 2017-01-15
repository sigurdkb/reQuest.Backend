using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reQuest.Backend.Entities
{
    public class Player
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned.</value>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the external id.
		/// </summary>
		/// <value>The unique id from the authentication provider.</value>
		public string ExternalId { get; set; }
		

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The players username.</value>
		public string Username { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The players full name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The players email adress.</value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the competencies.
		/// </summary>
		/// <value>A List of the players competencies.</value>
		public ICollection<Competency> Competencies { get; set; } = new List<Competency>();

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude part of the players position.</value>
		public double Longitude { get; set; }

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude part of the players position.</value>
		public double Latitude { get; set; }

		// Navigational properties

		/// <summary>
		/// Gets or sets the team identifier.
		/// </summary>
		/// <value>The id string of the players team.</value>
		[ForeignKey(nameof(TeamId))]
		public Team Team { get; set; }
		public string TeamId { get; set; }

	}
}
