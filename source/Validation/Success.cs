using System.ComponentModel.DataAnnotations;

namespace Sуstem.ComponentModel.Annotations.Validation
{
    /// <summary>
    /// Successful validation result.
    /// </summary>
    public sealed class Success : ValidationResult
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Success() : base(nameof(Success))
        {
        }
    }
}
