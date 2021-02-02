using Monitoring.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            DialogResult = true;
        }
    }
}
