using System;
using System.Collections.Generic;

namespace EasyJob.Serialization
{
    public class ConfigTab
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigTab"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="header">The header.</param>
        /// <param name="buttons">The buttons.</param>
        public ConfigTab(Guid id, string header, List<ConfigButton> buttons)
        {
            ID = id.Equals(Guid.Empty) ? Guid.NewGuid() : id;
            Header = header;
            Buttons = buttons;
        }

        /// <summary>
        /// Gets or sets the buttons.
        /// </summary>
        /// <value>
        /// The buttons.
        /// </value>
        public List<ConfigButton> Buttons { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid ID { get; set; }
    }
}
