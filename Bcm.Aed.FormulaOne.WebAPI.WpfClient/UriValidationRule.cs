using System.Globalization;
using System.Windows.Controls;

namespace Bcm.Aed.FormulaOne.WebAPI.WpfClient
{
	/// <ChangeLog>
	/// <Create Datum="29.01.2024" Entwickler="AED" />
	/// </ChangeLog>
	/// <summary>
	/// Validates whether an input string is a valid URI
	/// </summary>
	public class UriValidationRule : ValidationRule
    {
        public string ErrorMessage { get; set; } = "";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Uri.IsWellFormedUriString((string)value, UriKind.Absolute))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, ErrorMessage);
        }
    }
}