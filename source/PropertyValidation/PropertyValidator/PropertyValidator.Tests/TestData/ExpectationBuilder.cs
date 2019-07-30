using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PropertyProvider;
using PropertyProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.PropertyValidator.Tests.TestData
{
    public sealed class Expected
    {
        private readonly string expectationMessage;
        private readonly ReflectionPropertyAccessorFactory propertyFactory = new ReflectionPropertyAccessorFactory();
        private readonly List<ValidationAttribute> validationAttributes = new List<ValidationAttribute>();
        private readonly ICollection<ValidationResult> expectedResults = new List<ValidationResult>();

        private object source;
        private IPropertyAccessor property;
        private IPropertyAccessor parentProperty;

        public Expected(string expectationMessage)
        {
            this.expectationMessage = expectationMessage;
        }

        public Expected FromSource<TSource>() where TSource : class, new()
        {
            source = new TSource();
            return this;
        }

        public Expected OfProperty(string propertyName, object value = null, bool isReference = false)
        {
            SetSourcePropertyValue(propertyName, value);

            property = propertyFactory.Create(source, propertyName, isReference, validationAttributes);
            return this;
        }

        public Expected AndParentProperty(string propertyName, bool isReference = false)
        {
            parentProperty = propertyFactory.Create(source, propertyName, isReference, validationAttributes);
            return this;
        }

        public Expected WithAttributes(params ValidationAttribute[] attributes)
        {
            validationAttributes.AddRange(attributes);
            return this;
        }

        public Expected HasNoResult()
        {
            return this;
        }

        public Expected HasSucceededResult()
        {
            expectedResults.Add(ValidationResult.Success);
            return this;
        }

        public Expected HasResult(string validationResultMessage, params string[] propertyNames)
        {
            var memberNames = PrepareMemberNames(propertyNames);

            var validationResult = new ValidationResult(validationResultMessage, memberNames);
            expectedResults.Add(validationResult);
            return this;
        }

        public object[] Build()
        {
            return new[]
            {
                source,
                property,
                parentProperty,
                expectationMessage,
                expectedResults
            };
        }

        private void SetSourcePropertyValue(string propertyName, object value)
        {
#pragma warning disable UnhandledExceptions // Unhandled exception(s)
            var propertyInfo = source.GetType().GetProperty(propertyName);
#pragma warning restore UnhandledExceptions // Unhandled exception(s)
#pragma warning disable UnhandledExceptions // Unhandled exception(s)
            propertyInfo.SetValue(source, value);
#pragma warning restore UnhandledExceptions // Unhandled exception(s)
        }

        private string[] PrepareMemberNames(string[] propertyNames)
        {
            return propertyNames.Any() ? 
                propertyNames : 
                new[] { property.Name };
        }
    }
}