using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.ValidationRules
{
    /// <summary>
    /// Class StringToIntValidationRule.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ValidationRule" />
    public class StringToIntValidationRule : ValidationRule
    {
        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>A <see cref="T:System.Windows.Controls.ValidationResult" /> object.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int castVal;
            if (int.TryParse(value.ToString(), out castVal))
            {
                if(castVal > 0)
                    return new ValidationResult(true, null);
                return new ValidationResult(false, "Please enter an integer value greater than 0.");
            }

            return new ValidationResult(false, "Please enter a valid integer value.");
        }
    }
}
