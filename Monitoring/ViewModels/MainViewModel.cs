using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
      // RefreshCmd = new RelayCommand(Refresh, o => !IsRefreshing);
      // les lambdas sont très utiles pour adapter les protos de fonction
      RefreshCmd = new RelayCommand(o => Refresh(), CanRefresh);
      Refresh();
    }
    private bool CanRefresh(object obj)
    {
      return !IsRefreshing;
    }
    private async void Refresh()
    {
      IsRefreshing = true;
      try
      {
        Machines = new ObservableCollection<Machine>();
        // Bloque le thread graphique
        //Thread.Sleep(3000);
        Task refreshTask = Task.Delay(3000);
        // Bloque le thread graphique
        //refreshTask.Wait();
        // await attend avant d'exécuter l'instruction suivante
        // mais ne bloque graphique
        await refreshTask;
        IEnumerable<Machine> query = Enumerable
                            .Range(1, 10)
                            // lambda expression = fonction anonyme, comparable à
                            // f(x) : x -> x²+2
                            .Select(i =>
                            new Machine
                            {
                              IpAddress = "192.168.1." + i,
                              Name = "Computer" + i,
                              IsConnected = i % 2 == 0
                            });
        foreach (var machine in query)
          Machines.Add(machine);
      }
      finally
      {
        IsRefreshing = false;
      }
    }
    private bool isRefreshing;

    public bool IsRefreshing
    {
      get { return isRefreshing; }
      set
      {
        bool hasChanged = SetProperty(ref isRefreshing, value);
        if (hasChanged)
        {
          // Dire à la cmd qu'elle a changé d'état
          RefreshCmd.FireExecuteChanged();
        }
      }
    }
    public RelayCommand RefreshCmd { get; set; }
  }
}
