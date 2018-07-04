
using atpLib.Devices;

namespace atpLib.Scenarios
{
    /// <summary>
    /// a scenario that runs on a single device
    /// </summary>
    public abstract class SingleDeviceScenario : Scenario
    {
        public  Device device { get; protected set; }

        public SingleDeviceScenario(string name, Device device) : base(name)
        {
            this.device = device;
        }
    }
}
