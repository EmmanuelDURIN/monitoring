using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Monitoring.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Machine selectedMachine;

        public Machine SelectedMachine
        {
            get { return selectedMachine; }
            set
            {
                SetProperty(ref selectedMachine, value);
            }
        }

        private ObservableCollection<Machine> machines;

        public ObservableCollection<Machine> Machines
        {
            get { return machines; }
            set
            {
                SetProperty(ref machines, value);
            }
        }
        public MainViewModel()
        {
            // lambda expression = fonction anonyme, comparable à
            // f(x) : x -> x²+2
            machines = new ObservableCollection<Machine>();
            IEnumerable<Machine> query = Enumerable
                                .Range(1, 10)
                                .Select(i => new Machine { IpAddress = "192.168.1." + i, Name = "Computer" + i })
                                ;
            foreach (var machine in query)
            {
                machines.Add(machine);
            }
        }

    }
}
