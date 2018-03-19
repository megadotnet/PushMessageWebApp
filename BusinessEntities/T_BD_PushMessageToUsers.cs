using System;

namespace BusinessEntities
{

    /// <summary>
    /// T_BD_PushMessageToUsers
    /// </summary>
    public partial class T_BD_PushMessageToUsers 
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets the userid.
        /// </summary>
        /// <value>
        /// The userid.
        /// </value>
        public string Userid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read; otherwise, <c>false</c>.
        /// </value>
        public bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets the sending time.
        /// </summary>
        /// <value>
        /// The sending time.
        /// </value>
        public System.DateTime SendingTime { get; set; }

        /// <summary>
        /// Gets or sets the t bd push message.
        /// </summary>
        /// <value>
        /// The t bd push message.
        /// </value>
        public virtual T_BD_PushMessage T_BD_PushMessage { get; set; }
    }
}