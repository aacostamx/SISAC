

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    /// <summary>
    /// Message view object
    /// </summary>
    public class MessageVO
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the confirm text.
        /// </summary>
        /// <value>
        /// The confirm.
        /// </value>
        public string Confirm { get; set; }

        /// <summary>
        /// Gets or sets the cancel text.
        /// </summary>
        /// <value>
        /// The cancel.
        /// </value>
        public string Cancel { get; set; }
    }
}