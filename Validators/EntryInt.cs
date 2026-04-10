using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace prakt15_Leshukov_TRPO.Validators
{
    public class EntryInt : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            int valueInt;

            if (!int.TryParse(text, out valueInt)) return new ValidationResult(false, "Введите верное целое число");

            if (valueInt < 0) return new ValidationResult(false, "Должно быть больше 0");
            
            return ValidationResult.ValidResult;
        }
    }
}
