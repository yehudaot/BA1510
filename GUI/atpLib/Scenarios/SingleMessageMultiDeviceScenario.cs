using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using atpLib;
using atpLib.Messages;
using atpLib.Scenarios;
using atpLib.Devices;
using System.Threading.Tasks;
using System.Threading;

namespace atpLib.Scenarios
{
    public class SingleMessageMultiDeviceScenario : MultipleDeviceScenario
    {
        IMessage message { get; set; }
        bool receiveAnswer { get; set; }
        bool temporaryConnection { get; set; }

        public SingleMessageMultiDeviceScenario(string name, IMessage message, bool receiveAnswer, bool temporaryConnection, params Device[] devices)
            : base(name, devices)
        {
            this.message = message;
            this.receiveAnswer = receiveAnswer;
            this.temporaryConnection = temporaryConnection;
        }

        protected override Scenario.ScenarioResult internalRun(CancellationToken ct)
        {
            log.Info("sending a message of type: " + message.GetType().Name);
            Dictionary<Device, IResponse> resp = null;
            try
            {
                if (temporaryConnection)
                {
                    devices.ConnectAll();
                }
                devices.SendMsgToAll(message);
                if (receiveAnswer)
                {
                    resp = devices.ReceiveAnswerFromAll();
                }
            }
            catch (Exception ex)
            {
                if (ex is NullDeviceException)
                {
                    log.Error("null device in device list!", ex);
                }
            }
            finally
            {
                if (temporaryConnection)
                {
                    devices.DisconnectAll();
                }
            }

            ScenarioResult.RunResult runResult = ScenarioResult.RunResult.Pass;
            if(resp == null || resp.ContainsValue(null))
            {
                runResult = ScenarioResult.RunResult.Fail;
            }

            return new ScenarioResult(runResult, resp);
        }
    }
}
