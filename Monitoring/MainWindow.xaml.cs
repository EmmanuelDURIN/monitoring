using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMachineWindow window = new AddMachineWindow();
            bool? result = window.ShowDialog();
            if ( result == true)
            {
                var machine = window.Machine;
                // machine added
                // Interpolation string :
                ///   1) mettre un $
                ///   2) mettre des expressions {machine.Name} plutôt que ds numéros {0}
                ///  Référence formatage
                ///            https://dzone.com/refcardz/coredotnet?chapter=3
                MessageBox.Show($"Machine : {machine.Name} {machine.Priority:000}");
            }
        }
    }
}
