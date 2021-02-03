using Monitoring.Validations;
using Monitoring.ViewModels;
using System.Windows;

namespace Monitoring
{
  /// <summary>
  /// Interaction logic for AddMachineWindow.xaml
  /// </summary>
  public partial class AddMachineWindow : Window
  {

    public Machine Machine { get; set; } = new Machine { Name = "Chene" };
    // Add property so that Mainwindow can get the machine
    public AddMachineWindow()
    {
      InitializeComponent();
      this.DataContext = Machine;
    }
    private void ButtonCancelClick(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
    private void ButtonOkClick(object sender, RoutedEventArgs e)
    {
      if (this.IsValid())
        DialogResult = true;
    }
  }
}
