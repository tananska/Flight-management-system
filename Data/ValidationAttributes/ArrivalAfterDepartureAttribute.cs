using System;
using System.ComponentModel.DataAnnotations;

namespace Data.ValidationAttributes
{
    public class ArrivalAfterDepartureAttribute : ValidationAttribute
    {
        private readonly string departure;

        public ArrivalAfterDepartureAttribute(string departure)
        {
            this.departure = departure;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var departureProperty = validationContext.ObjectType.GetProperty(this.departure);

            if (departureProperty == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", this.departure));
            }

            var departureValue = departureProperty.GetValue(validationContext.ObjectInstance, null);
            DateTime departure = Convert.ToDateTime(departureValue);
            DateTime arrival = Convert.ToDateTime(value);

            if (arrival <= departure)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}
