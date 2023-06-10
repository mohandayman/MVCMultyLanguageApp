   using System;   
   using System.ComponentModel.DataAnnotations;
namespace ITSpark_Task.Models.ValidationClasses
{

    public class DateFromToAttribute : ValidationAttribute
    {
        private readonly string _fromPropertyName;
        private readonly string _toPropertyName;

        public DateFromToAttribute(string fromPropertyName, string toPropertyName)
        {
            _fromPropertyName = fromPropertyName;
            _toPropertyName = toPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fromProperty = validationContext.ObjectType.GetProperty(_fromPropertyName);
            var toProperty = validationContext.ObjectType.GetProperty(_toPropertyName);

            var fromValue = (DateTime)fromProperty.GetValue(validationContext.ObjectInstance);
            var toValue = (DateTime)value;

            if (fromValue <= toValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}

