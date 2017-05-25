using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.ValidationRules
{
    public class MazeNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value.ToString();
            if (str.Contains(" "))
                return new ValidationResult(false, "Name can't contain white spaces.");
            else if(str.Equals(string.Empty))
                return new ValidationResult(false, "Name can't be empty.");
            return new ValidationResult(true, null);
        }
    }
}
