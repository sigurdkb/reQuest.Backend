using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reQuest.Backend.Entities
{
    public class Topic
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
		/// <value>The external id of the topic, typically provided by SIS.</value>
        public string ExternalId { get; set; }

		/// <summary>
		/// Gets or sets the short name.
		/// </summary>
		/// <value>The short name of the topic, typically the course code.</value>
        public string ShortName { get; set; }

		/// <summary>
		/// Gets or sets the long name.
		/// </summary>
		/// <value>The long name of the topic.</value>
        public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>A description of the topic.</value>
        public string Description { get; set; }

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		/// <value>A url to a page describing the topic.</value>
        public string Url { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this topic is locked or not.
		/// Indicates whether or not the course is imported from external source.
		/// </summary>
		/// <value><c>true</c> if imported; otherwise, <c>false</c>.</value>
        public bool Locked { get; set; }
    }
}