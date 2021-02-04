using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// micro : envoie l'ordre d'annulation : Source de l'événement
    private CancellationTokenSource cancellationTokenSource;
    public MainViewModel()
    {
      // RefreshCmd = new RelayCommand(Refresh, o => !IsRefreshing);
      // les lambdas sont très utiles pour adapter les protos de fonction
      RefreshCmd = new RelayCommand(o => Refresh(), CanRefresh);
      CancelCmd = new RelayCommand(o => Cancel(), o => IsRefreshing);
      Refresh();
    }
    private void Cancel()
    {
      // inutile avec ?.
      //if (cancellationTokenSource != null)
      cancellationTokenSource?.Cancel();
      //string noVoie = personne?.Adresse?.Voie;
    }
    private bool CanRefresh(object obj)
    {
      return !IsRefreshing;
    }
    // une fonction async peut avoir 3 types de retour :
    // 1) Task (éq. void) - bien  parce que permet la transmission des exceptions
    // 2) Task<T>  (éq. T) - bien 
    // 3) void - moins bien sauf si c'est une callback
    private async void Refresh()
    {
      cancellationTokenSource = new CancellationTokenSource();
      // l'écouteur de l'ordre d'annulation
      CancellationToken cancellationToken = cancellationTokenSource.Token;

      IsRefreshing = true;
      try
      {
        StopMachines();
        Machines = new ObservableCollection<Machine>();
        // Bloque le thread graphique
        //Thread.Sleep(3000);

        // les meilleures méthodes async acceptent un CancellationToken
        Task refreshTask = Task.Delay(3000, cancellationToken);
        // Bloque le thread graphique
        //refreshTask.Wait();
        // await attend avant d'exécuter l'instruction suivante
        // mais ne bloque graphique
        await refreshTask;

        IEnumerable<Machine> query = Enumerable
                            .Range(1, 1000)
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
        {

          Machines.Add(machine);
          machine.StartMonitoring();
        }
      }
      catch (TaskCanceledException)
      {
        // annulation en appuyant sur Cancel
      }
      finally
      {
        IsRefreshing = false;
      }
    }

    private void StopMachines()
    {
      if (Machines == null)
        return;
      foreach (var machine in Machines)
      {
        machine.StopMonitoring();
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
          CancelCmd.FireExecuteChanged();
          // Dans le cas d'une propriété dérivée
          // il faut émettre des événements pour dire que
          // les propriétés dérivées ont changées
          OnPropertyChanged(nameof(IsNotRefreshing));
        }
      }
    }
    // Propriété dérivée, calculée
    public bool IsNotRefreshing
    {
      get { return !isRefreshing; }
    }

    public RelayCommand RefreshCmd { get; set; }
    public RelayCommand CancelCmd { get; set; }
  }
}
