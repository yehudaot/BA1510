using atpLib.Scenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atpLib.Devices;
using mfe_gui.Messages;
using System.IO;
using System.Threading;
using static atpLib.Devices.Device;

namespace mfe_gui.Scenarios
{
    public class SoftwareUpdateScenario : SingleDeviceScenario
    {
        public class SoftwareUpdateException : Exception
        {

        }

        public enum Operation
        {
            Program,
            Verify,
        }

        public delegate void SoftwareUpdateDelegate(long total, long current, string status);
        public delegate void OperationFinishedDelegate(Operation operation);

        SoftwareUpdateDelegate progressFunc;
        OperationFinishedDelegate finishedFunc;
        Operation operation;
        string filePath;

        public SoftwareUpdateScenario(string name, Device device, string filePath, Operation operation, SoftwareUpdateDelegate progressFunc, OperationFinishedDelegate finishedFunc) : base(name, device)
        {
            this.progressFunc = progressFunc;
            this.operation = operation;
            this.filePath = filePath;
            this.finishedFunc = finishedFunc;
       }

        protected override ScenarioResult internalRun(CancellationToken ct)
        {
            try
            {
                /* start the conversion */
                int blockCount = 0;
                int curBlock = 0;

                HexConverter h = new HexConverter(filePath);
                h.Process();
                blockCount = h.lineCount();

                progressFunc.Invoke(blockCount, 0, "Staring the " + operation.ToString() + " Operation...");
                bool operationDone = false;
                while (!operationDone && !ct.IsCancellationRequested)
                {
                    /* send a line*/
                    KeyValuePair<UInt32, byte[]> line;
                    if (h.getNextLine(false, out line))
                    {
                        if (operation == Operation.Program)
                        {
                            log.Info("Programming address: 0x" + line.Key.ToString("X"));
                            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Send Line Scenario", new SetBootDataMessage(line.Key, line.Value), true, false, device).run();
                            if (r == null || r.result != Scenario.ScenarioResult.RunResult.Pass)
                            {
                                throw new SoftwareUpdateException();
                            }
                            SetBootDataResponse resp = (SetBootDataResponse)r.resultObj;
                            if (resp.address != line.Key)
                            {
                                log.Error("set data at address: 0x" + line.Key.ToString("X") + " but got a response of: 0x" + resp.address.ToString("X"));
                                throw new InvalidDataException();
                            }
                            if(resp.status != SetBootDataResponse.Status.OK)
                            {
                                log.Error("set data err at address: 0x" + line.Key.ToString("X"));
                                throw new InvalidDataException();
                            }
                        } else if(operation == Operation.Verify)
                        {
                            log.Info("Verifying address: 0x" + line.Key.ToString("X"));
                            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Get Line Scenario", new GetBootDataMessage(line.Key), true, false, device).run();
                            if (r == null || r.result != Scenario.ScenarioResult.RunResult.Pass)
                            {
                                throw new SoftwareUpdateException();
                            }
                            GetBootDataResponse resp = (GetBootDataResponse)r.resultObj;
                            /* compare */
                            if(line.Key != resp.address)
                            {
                                log.Error("requested address: 0x" + line.Key.ToString("X") + " but got a response of: " + resp.address.ToString());
                                throw new InvalidDataException();
                            }

                            if(line.Value.Length != resp.lineData.Length)
                            {
                                log.Error("somehow the secotr size is not equal in the hex and device!");
                                throw new InvalidDataException();
                            }

                            for(int i=0;i<line.Value.Length; i++)
                            {
                                if(line.Value[i] != resp.lineData[i])
                                {
                                    log.Error("verify failed at address: 0x" + (line.Key + i).ToString("X") + " - 0x" + resp.lineData[i].ToString("X") + " instead of: 0x" + line.Value[i].ToString("X"));
                                    throw new InvalidDataException();
                                }
                            }
                        }
                        progressFunc.Invoke(blockCount, curBlock++, operation.ToString() + " operation in progress");
                    } else
                    {
                        Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Finish Update Scenario", new FinishUpdateMessage(), true, false, device).run();
                        if (r == null || r.result != Scenario.ScenarioResult.RunResult.Pass)
                        {
                            throw new SoftwareUpdateException();
                        }
                        operationDone = true;
                    }
                }

                if(ct.IsCancellationRequested)
                {
                    progressFunc.Invoke(100, 0, operation.ToString() + " operation stopped");
                } else
                {
                    progressFunc.Invoke(100, 100, "Done.");
                }
                
                return new ScenarioResult(ScenarioResult.RunResult.Pass, null);
            } catch (Exception ex)
            {
                log.Error("error in \"Software Update Scenario\"", ex);
                progressFunc.Invoke(100, 0, "Error...");
                return new ScenarioResult(ScenarioResult.RunResult.Fail, null);
            }
            finally
            {
                finishedFunc.Invoke(operation);
            }

        }
    }
}
