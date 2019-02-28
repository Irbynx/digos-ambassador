using System.Collections.Generic;

namespace DIGOS.Ambassador.Database.TCG
{
    /// <summary>
    /// Represents a keyword element; either a keyword itself, a string or a number that works as a parameter
    /// </summary>
    public class TCGEffectElement
    {
        /// <summary>
        /// Gets or sets the effect name value
        /// </summary>
        public string Effect { get; set; }

        /// <summary>
        /// Gets or sets the attached string values
        /// </summary>
        public List<string> StringValue { get; set; }

        /// <summary>
        /// Gets or sets the attached numeric values
        /// </summary>
        public List<long> IntValue { get; set; }

        /// <summary>
        /// Gets or sets the children elements
        /// </summary>
        public List<TCGEffectElement> ChildrenEffect { get; set; }
    }
}