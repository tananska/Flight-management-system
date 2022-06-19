using System;
using System.ComponentModel.DataAnnotations;

namespace Data.ValidationAttributes
{
    public class CapacityLessThanAttribute : ValidationAttribute
    {
        private readonly string capacity;
        public CapacityLessThanAttribute(string capacity)
        {
            this.capacity = capacity;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var capacityProperty = validationContext.ObjectType.GetProperty(this.capacity);

            if (capacityProperty == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", this.capacity));
            }

            var capacityValue = capacityProperty.GetValue(validationContext.ObjectInstance, null);
            int ownCapacity = Convert.ToInt32(value);
            int otherCapacity = Convert.ToInt32(capacityValue);
            if (ownCapacity >= otherCapacity)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            }
            return null;
        }
    }
}
