using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Monitoring.Validations
{
  public static class ValidationHelper
  {
    public static bool IsValid(this DependencyObject obj)
    {
      // The dependency object is valid if it has no errors and all
      // of its children (that are dependency objects) are error-free.
      return !Validation.GetHasError(obj) &&
          LogicalTreeHelper.GetChildren(obj)
          .OfType<DependencyObject>()
          .All(IsValid);
    }
  }
}
