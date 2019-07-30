using System;

namespace Sуstem.ComponentModel.Annotations.Validation
{
    /// <summary>
    /// Attribute that marks class invariants.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class InvariantAttribute : Attribute
    {
        /// <summary>
        /// Name of invariant.
        /// Used to generate an error message.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// If the invariant name is not specified, it will be taken from the method name.
        /// </summary>
        public InvariantAttribute()
        {
        }

        /// <summary>
        /// Creates an attribute and sets the name of an invariant.
        /// </summary>
        /// <param name="name">Human readable invariant name</param>
        public InvariantAttribute(string name)
        {
            Name = name;
        }
    }
}
