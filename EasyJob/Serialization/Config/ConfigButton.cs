using System;
using System.Collections.Generic;

namespace EasyJob.Serialization
{
    public class ConfigButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigButton"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text">The text.</param>
        /// <param name="description">The description.</param>
        /// <param name="script">The script.</param>
        /// <param name="scriptpathtype">The scriptpathtype.</param>
        /// <param name="scripttype">The scripttype.</param>
        /// <param name="arguments">The arguments.</param>
        public ConfigButton(Guid id, string text, string description, string script, string scriptpathtype, string scripttype, List<ConfigArgument> arguments)
        {
            Id = id.Equals(Guid.Empty) ? Guid.NewGuid() : id;
            Text = text;
            Description = description;
            Script = script;
            ScriptPathType = scriptpathtype;
            ScriptType = scripttype;
            Arguments = arguments;
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public List<ConfigArgument> Arguments { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>
        /// The script.
        /// </value>
        public string Script { get; set; }

        /// <summary>
        /// Gets or sets the type of the script path.
        /// </summary>
        /// <value>
        /// The type of the script path.
        /// </value>
        public string ScriptPathType { get; set; }

        /// <summary>
        /// Gets or sets the type of the script.
        /// </summary>
        /// <value>
        /// The type of the script.
        /// </value>
        public string ScriptType { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }
    }
}
