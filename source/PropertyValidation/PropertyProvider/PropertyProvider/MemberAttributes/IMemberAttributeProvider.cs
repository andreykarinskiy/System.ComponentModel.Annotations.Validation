using System;
using System.Collections.Generic;
using System.Reflection;

namespace PropertyProvider.MemberAttributes
{
    /// <summary>
    /// Provides attributes owned by a class member.
    /// By default, it returns attributes that belong only to the heir class,
    /// and ignores the attributes of the ancestor class.
    /// However, redefinition of such behavior is possible.
    /// </summary>
    public interface IMemberAttributeProvider
    {
        /// <summary>
        /// Returns member attributes.
        /// The behavior is similar to MemberInfo.GetCustomAttributes(inherit: false);
        /// </summary>
        /// <param name="member">member</param>
        /// <returns>member attributes</returns>
        IEnumerable<Attribute> GetAttributes(MemberInfo member);
    }
}
