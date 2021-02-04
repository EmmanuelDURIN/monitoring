
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monitoring.ViewModels
{
  public class MachineWatcher
  {
    private static int counter = 0;
    private int number = counter++;
    public static int TimeOutInMs { get; set; } = 1000;
    static Random random = new Random();
    private Machine machine;
    private Progress<MachineUpdateData> progress;

    public MachineWatcher(Machine machine)
    {
      this.machine = machine;
      // instanciation sur le thread graphique
      progress = new Progress<MachineUpdateData>();
      progress.ProgressChanged += ProgressChanged;
    }

    private void ProgressChanged(object sender, MachineUpdateData m)
    {
      // invocation sur le thread graphique
      // machine est databindée
      // pas m
      machine.IsConnected = m.IsConnected;
    }

    //public async Task Start()
    //{
    //  // démarrage d'un thread
    //  Task monitorTask = Task.Run(DoMonitoring);
    //  await monitorTask;
    //}
    public void Start()
    {
      // démarrage d'un thread
      Task monitorTask = Task.Run(DoMonitoring);
    }
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private async Task DoMonitoring()
    {
      // Exécution sur un thread non graphique
      // Comment arrêter cela : CancellationToken ? Oui car évite l'attente
      CancellationToken cancellationToken = cancellationTokenSource.Token;
      try
      {
        while (true)
        {
          //System.Diagnostics.Debug.WriteLine($"Ping from {number}");
          await Task.Delay(TimeOutInMs, cancellationToken);
          if (random.Next(10) == 9)
          {
            // déclenche le retour sur le thread graphique
            ((IProgress<MachineUpdateData>)progress).Report(
              // Modifier ici une machine non liée au thread graphique
              new MachineUpdateData
              {
                IsConnected = !machine.IsConnected,
                IsWorking = !machine.IsWorking,
              }
              );
          }
        }
      }
      finally
      {
        System.Diagnostics.Debug.WriteLine("Canceled");
      }
    }

    internal void Stop()
    {
      cancellationTokenSource.Cancel();
    }
  }
}
