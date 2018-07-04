using System;
using atpLib.Devices;
using atpLib.Messages.zUp;
using System.Threading;

namespace atpLib.Scenarios.zUp
{
    public class zUpSetVoltageScenario : SingleDeviceScenario
    {
        float voltage { get; set; }
        bool receiveAnswer { get; set; }

        public zUpSetVoltageScenario(string name, float voltage, Device device)
            : base(name, device)
        {
            this.voltage = voltage;
            this.receiveAnswer = receiveAnswer;
        }

        protected override Scenario.ScenarioResult internalRun(CancellationToken ct)
        {
            ScenarioResult.RunResult sResult = ScenarioResult.RunResult.Pass;
            try
            {
                device.connect();
                if (!device.isAlive())
                {
                    throw new ScenarioException("zUP device is not alive!");
                }
                //device.sendMsg(new zUPSetAddressMessage());
                /* disable output */
                device.sendMsg(new zUPOutputMessage(true));
                /* set voltage */
                device.sendMsg(new zUPSetVoltagetMessage(voltage));
                /* read back assigned voltage */
                device.sendMsg(new zUpGetVoltageMessage());
                string readVoltage = ((zUpGetVoltageResponse)device.receiveAnswer()).voltage;
                log.Info("Read voltage of: " + readVoltage);
                float actualVoltage = 0;
                if (!float.TryParse(readVoltage, out actualVoltage))
                {
                    log.Error("Error reading voltage from zUp");
                    sResult = ScenarioResult.RunResult.Fail;
                }
                else
                {
                    if (voltage * 1.05 < actualVoltage || voltage * 0.95 > actualVoltage)
                    {
                        log.Error("Voltage reading is not in range (+- 5%), Stopping output!");
                        device.sendMsg(new zUPOutputMessage(false));
                        sResult = ScenarioResult.RunResult.Fail;
                    }
                }

               // if (false)
               // {
               //     /* disable output */
               //     device.sendMsg(new zUPOutputMessage(false));
               // }
            }
            catch (Exception ex)
            {
                log.Error("Error in scenario", ex);
            }
            finally
            {
                device.disconnect();
            }
            return new ScenarioResult(sResult, null);
        }
    }
}
