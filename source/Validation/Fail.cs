using System.ComponentModel.DataAnnotations;

namespace Sуstem.ComponentModel.Annotations.Validation
{
    /// <summary>
    /// Unsuccessful validation result, signals that validation failed.
    /// </summary>
    public sealed class Fail : ValidationResult
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="because"></param>
        public Fail(string because) : base(because)
        {
        }
    }
}
