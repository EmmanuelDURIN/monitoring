
using System;
using System.Threading.Tasks;

namespace Monitoring.ViewModels
{
  public class MachineWatcher
  {
    public static int TimeOutInMs { get; set; } = 1000;
    static Random random = new Random();
    private Machine machine;
    private Progress<Machine> progress;

    public MachineWatcher(Machine machine)
    {
      this.machine = machine;
      // instanciation sur le thread graphique
      progress = new Progress<Machine>();
      progress.ProgressChanged += ProgressChanged;
    }

    private void ProgressChanged(object sender, Machine m)
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

    private async Task DoMonitoring()
    {
      // Exécution sur un thread non graphique
      // Comment arrêter cela : CancellationToken ?
      while (true)
      {
        await Task.Delay(TimeOutInMs);
        if (random.Next(10) == 9)
        {
          // déclenche le retour sur le thread graphique
          ((IProgress<Machine>)progress).Report(
            // Modifier ici une machine non liée au thread graphique
            new Machine {
              IsConnected = !machine.IsConnected,
             // HasSession = !machine.HasSession,
              IsWorking = !machine.IsWorking,
            }
            );
        }
      }
    }

    internal void Stop()
    {
      throw new NotImplementedException();
    }
  }
}
