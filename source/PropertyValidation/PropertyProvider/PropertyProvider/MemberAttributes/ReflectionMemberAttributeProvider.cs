using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PropertyProvider.MemberAttributes
{
    /// <summary>
    /// Implementation of <see cref="IMemberAttributeProvider"/> interface.
    /// Uses reflection to get member attributes.
    /// </summary>
    public sealed class ReflectionMemberAttributeProvider : IMemberAttributeProvider
    {
        /// <summary>
        /// When it is True, then attributes are returned for the whole inheritance hierarchy.
        /// </summary>
        public bool UseInherited { get; } = false;

        #region Implementation of IMemberAttributeProvider

        /// <inheritdoc cref="IMemberAttributeProvider.GetAttributes"/>
        public IEnumerable<Attribute> GetAttributes(MemberInfo member)
        {
            return member
                .GetCustomAttributes(UseInherited)
                .OfType<Attribute>();
        }

        #endregion
    }
}
