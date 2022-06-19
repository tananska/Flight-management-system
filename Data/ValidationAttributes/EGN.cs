using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Data.Validation
{
    public class EGN : ValidationAttribute
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
