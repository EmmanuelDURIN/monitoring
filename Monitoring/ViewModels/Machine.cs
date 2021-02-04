
using System;

namespace Monitoring.ViewModels
{
  public class Machine : BindableBase
  {
    private int priority;
    private string name;
    private string ipAddress;
    private string macAddress;

    public int Priority
    {
      get { return priority; }
      set
      {
        // BindableBase.SetProperty :
        //  1) Met à jour la propriété
        //  2) Emet l'événement
        SetProperty(ref priority, value);
      }
    }
    public string Name
    {
      get { return name; }
      set
      {
        SetProperty(ref name, value);
      }
    }
    public string IpAddress
    {
      get { return ipAddress; }
      set
      {
        SetProperty(ref ipAddress, value);
      }
    }
    public string MacAddress
    {
      get { return macAddress; }
      set
      {
        SetProperty(ref macAddress, value);
      }
    }

    private bool isConnected;

    public bool IsConnected
    {
      get { return isConnected; }
      set
      {
        SetProperty(ref isConnected, value);
      }
    }
    private bool hasSession;

    public bool HasSession
    {
      get { return hasSession; }
      set
      {
        SetProperty(ref hasSession, value);
      }
    }
    private bool isWorking;
    private MachineWatcher machineWatcher;

    public bool IsWorking
    {
      get { return isWorking; }
      set
      {
        SetProperty(ref isWorking, value);
      }
    }

    public Machine()
    {
      machineWatcher = new MachineWatcher(this);
    }

    internal void StartMonitoring()
    {
      machineWatcher.Start();
    }
    internal void StopMonitoring()
    {
      machineWatcher.Stop();
    }
  }
}
