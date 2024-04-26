using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopProject.UI.Helpers
{
    public class DecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (decimal.TryParse(value as string, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Пожалуйста, введите десятичное число.");
        }
    }
}
