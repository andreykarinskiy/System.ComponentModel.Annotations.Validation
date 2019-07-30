using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider;
using System.ComponentModel.Annotations.Validation.MethodValidator;
using System.ComponentModel.Annotations.Validation.PropertyValidator;
using System.ComponentModel.DataAnnotations;
using PropertyProvider;
using PropertyProvider.Accessor;

namespace Sуstem.ComponentModel.Annotations.Validation
{
    /// <inheritdoc cref="IValidator"/>
    public sealed class ObjectValidator : IValidator
    {
        private readonly IPropertyProvider propertyProvider;
        private readonly IPropertyValidator propertyValidator;
        private readonly IMethodProvider methodProvider;
        private readonly IMethodValidator methodValidator;

        // TODO naive implementation
        private readonly ICollection<object> traversedObjects = new HashSet<object>();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="propertyProvider"></param>
        /// <param name="propertyValidator"></param>
        /// <param name="methodProvider"></param>
        /// <param name="methodValidator"></param>
        public ObjectValidator(IPropertyProvider propertyProvider, IPropertyValidator propertyValidator, IMethodProvider methodProvider, IMethodValidator methodValidator)
        {
            this.propertyProvider = propertyProvider;
            this.propertyValidator = propertyValidator;
            this.methodProvider = methodProvider;
            this.methodValidator = methodValidator;
        }

        /// <summary>
        /// Main ctor
        /// </summary>
        public ObjectValidator() : this(PropertyProviderFactory.CreateCached(), new DefaultPropertyValidator(), MethodProviderFactory.CreateCached(), new DefaultMethodValidator())
        {
        }

        #region IValidator implementation

        /// <inheritdoc cref="IValidator.Validate"/>
        public IEnumerable<ValidationResult> Validate(object source)
        {
            // Parent property is null, because source is root of object graph.
            return Validate(source, parentProperty: null);
        }

        #endregion

        private IEnumerable<ValidationResult> Validate(object source, IPropertyAccessor parentProperty)
        {
            // Infinite recursion prevention.
            if (CheckIsTraversedAndMarkItIfNeeded(source))
            {
                yield break;
            }

            foreach (var validationResult in ValidateProperties(source, parentProperty))
            {
                yield return validationResult;
            }

            foreach (var validationResult in ValidateInvariants(source))
            {
                yield return validationResult;
            }
        }

        private IEnumerable<ValidationResult> ValidateProperties(object source, IPropertyAccessor parentProperty)
        {
            foreach (var property in propertyProvider.GetProperties(source))
            {
                if (property.IsReference)
                {
                    // Property is a pointer to another class,
                    // so you need to recursively validate this object.

                    var referenceEnd = property.GetValue(source);

                    if (property.IsCollection)
                    {
                        foreach (var child in (IEnumerable) referenceEnd)
                        {
                            foreach (var result in Validate(child, property))
                            {
                                yield return result;
                            }
                        }
                    }
                    else
                    {
                        foreach (var result in Validate(referenceEnd, property))
                        {
                            yield return result;
                        }
                    }
                }
                else
                {
                    // Simple (build-in) type property.
                    foreach (var result in propertyValidator.Validate(source, property, parentProperty))
                    {
                        if (result != ValidationResult.Success)
                        {
                            yield return result;
                        }
                    }
                }
            }
        }

        private IEnumerable<ValidationResult> ValidateInvariants(object source)
        {
            foreach (var method in methodProvider.GetMethods(source))
            {
                foreach (var result in methodValidator.Validate(source, method))
                {
                    yield return result;
                }
            }
        }

        private bool CheckIsTraversedAndMarkItIfNeeded(object source)
        {
            if (traversedObjects.Contains(source))
            {
                return true;
            }

            traversedObjects.Add(source);

            return false;
        }
    }
}
