namespace EasyJob.Serialization
{
    public class ConfigArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigArgument"/> class.
        /// </summary>
        /// <param name="argumentQuestion">The argument question.</param>
        /// <param name="argumentAnswer">The argument answer.</param>
        public ConfigArgument(string argumentQuestion, string argumentAnswer)
        {
            ArgumentQuestion = argumentQuestion;
            ArgumentAnswer = argumentAnswer;
        }

        /// <summary>
        /// Gets or sets the argument answer.
        /// </summary>
        /// <value>
        /// The argument answer.
        /// </value>
        public string ArgumentAnswer { get; set; }

        /// <summary>
        /// Gets or sets the argument question.
        /// </summary>
        /// <value>
        /// The argument question.
        /// </value>
        public string ArgumentQuestion { get; set; }
    }
}
