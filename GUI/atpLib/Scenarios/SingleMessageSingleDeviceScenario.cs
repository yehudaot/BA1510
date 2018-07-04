using System;
using System.Collections.Generic;
using atpLib.Messages;
using atpLib.Devices;
using System.Threading;
using static atpLib.Devices.Device;
using atpLib.Messagess;

namespace atpLib.Scenarios
{
    public class SingleMessageSingleDeviceScenario : SingleDeviceScenario
    {
        IMessage message { get; set; }
        bool receiveAnswer { get; set; }
        bool temporaryConnection { get; set; }

        public SingleMessageSingleDeviceScenario(string name, IMessage message, bool receiveAnswer, bool temporaryConnection, Device device)
            : base(name, device)
        {
            this.message = message;
            this.receiveAnswer = receiveAnswer;
            this.temporaryConnection = temporaryConnection;
        }

        protected override Scenario.ScenarioResult internalRun(CancellationToken ct)
        {
            log.Info("sending a message of type: " + message.GetType().Name);
            IResponse resp = null;
            AsyncMessageToken token = null;

            if (device == null)
            {
                throw new DeviceNotInitException("The device object was not initilized correctly!");
            }
            if (!device.isConnected())
            {
                throw new DeviceNotConnectedException();
            }
            try
            {
                if(temporaryConnection)
                {
                    device.connect();
                }

                if (device.SupportsAsyncOperations)
                {
                    token = device.sendAsyncMsg(message, ct);
                }
                else
                {
                    device.sendMsg(message);
                }

                if (receiveAnswer)
                {
                    if (device.SupportsAsyncOperations)
                    {
                        resp = device.receiveAsyncAnswer(token, ct);
                    }
                    else
                    {
                        resp = device.receiveAnswer(ct);
                    }
                    if (resp == null && !ct.IsCancellationRequested) { 
                        throw new UnknownOPException("response arrived as NULL");
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                if(temporaryConnection)
                {
                    device.disconnect();
                }
            }

            ScenarioResult.RunResult runResult = ct.IsCancellationRequested ? ScenarioResult.RunResult.TimedOut : ScenarioResult.RunResult.Pass;
            return new ScenarioResult(runResult, resp);
        }
    }
}

