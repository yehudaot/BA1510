using System;
using atpLib.Devices;
using atpLib.Messages.zUp;
using System.Threading;

namespace atpLib.Scenarios.zUp
{
    public class zUpGetVoltageScenario : SingleDeviceScenario
    {

        public zUpGetVoltageScenario(string name, Device device)
            : base(name, device)
        {
        }

        protected override Scenario.ScenarioResult internalRun(CancellationToken ct)
        {
            ScenarioResult.RunResult sResult = ScenarioResult.RunResult.Fail;
            float voltage = 0;
            try
            {
                device.connect();
                if(!device.isConnected())
                {
                    throw new ScenarioException("could not connect to zUP device!");
                }
                device.sendMsg(new zUPSetAddressMessage());
                /* read current output voltage */
                device.sendMsg(new zUpGetVoltageMessage());
                string readVoltage = ((zUpGetVoltageResponse)device.receiveAnswer()).voltage;
                log.Info("Read voltage of: " + readVoltage);
                voltage = 0;
                if (!float.TryParse(readVoltage, out voltage))
                {
                    throw new ScenarioException("Error reading voltage from zUp!");
                }
                sResult = ScenarioResult.RunResult.Pass;
            }
            catch (Exception ex)
            {
                log.Error("Error in scenario", ex);
            }
            finally
            {
                device.disconnect();
            }
            return new ScenarioResult(sResult, voltage);
        }
    }
}
