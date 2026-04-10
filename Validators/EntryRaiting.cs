using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace prakt15_Leshukov_TRPO.Validators
{
    public class EntryRaiting:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            text = text.Replace('.', ',');
            double rating;
            if (!double.TryParse(text, out rating)) return new ValidationResult(false, "Должно быть число");

            if (rating > 5) return new ValidationResult(false, "Рейтинг не может быть больше 5.0");

            if (rating < 0) return new ValidationResult(false, "Рейтинг должен быть положительным");

            return ValidationResult.ValidResult;
        }
    }
}
