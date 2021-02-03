using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Monitoring.Validations
{
  public class IpAddressValidationRule : ValidationRule
  {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {

      String input = value.ToString();
      if (!String.IsNullOrEmpty(input))
      {
        Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        MatchCollection results = ip.Matches(input);
        if (results.Count < 1)
          return new ValidationResult(false, "Not a valid ip address");
      }
      return ValidationResult.ValidResult;
    }
  }
}
