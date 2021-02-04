using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ViewModels
{
  struct MachineUpdateData
  {
    public bool IsConnected { get; internal set; }
    public bool IsWorking { get; internal set; }
  }
}
