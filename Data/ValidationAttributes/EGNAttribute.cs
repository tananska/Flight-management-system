using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Data.ValidationAttributes
{
    public class EGNAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string egn = value.ToString();
            if (egn.Length != 10)
            {
                return false;
            }
            if (!egn.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }
    }
}
