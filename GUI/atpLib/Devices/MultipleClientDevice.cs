using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using atpLib.Infra;
using atpLib.Messages;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace atpLib.Devices
{
    /// <summary>
    /// An implementation of the Async operations on a Device
    /// </summary>
    /// <typeparam name="T">Any device that implements send & receieve</typeparam>
    public class MultipleClientDevice<T> : Device where T : Device
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const int RECIEVE_TIMEOUT_MS = 60*1000; /* 60 seconds */
        public T device { get; protected set; }

        ConcurrentDictionary<Guid, Tuple<IMessage, CancellationToken>> messageDictionary = new ConcurrentDictionary<Guid, Tuple<IMessage, CancellationToken>>();
        ConcurrentDictionary<Guid, IResponse> responseDictionary = new ConcurrentDictionary<Guid, IResponse>();
        
        Thread messageProcessingThread;
        CancellationTokenSource messageProcessingCt;

        public MultipleClientDevice(T device)
        {
            this.device = device;
            this.SupportsAsyncOperations = true;
            messageProcessingCt = new CancellationTokenSource();
            messageProcessingThread = new Thread(new ParameterizedThreadStart(messageProcessingFunction));
        }

        public override bool connect()
        {
            if (messageProcessingThread != null && !messageProcessingThread.IsAlive)
            {
                messageProcessingThread.Start(messageProcessingCt.Token);
            }
            return device.connect();
        }

        public override void disconnect()
        {
            if (messageProcessingThread != null && messageProcessingThread.IsAlive)
            {
                messageProcessingCt.Cancel();
            }
            device.disconnect();
        }
        public override bool isAlive()
        {
            return device.isAlive();
        }

        public override bool isConnected()
        {
            return device.isConnected();
        }

        public override IResponse receiveAnswer()
        {
            return device.receiveAnswer();
        }

        public override IResponse receiveAnswer(CancellationToken ct)
        {
            return device.receiveAnswer(ct);
        }

        public override IResponse receiveAsyncAnswer(AsyncMessageToken messageToken, CancellationToken ct)
        {
            Task<IResponse> t = Task<IResponse>.Run(() => findResponseInList(messageToken.Value, ct)).TimeoutAfter(RECIEVE_TIMEOUT_MS);
            while (!t.IsCompleted)
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1);
            }
            IResponse r = t.Result;
            t.Dispose();
            return r;
        }

        public override void sendMsg(IMessage message)
        {
            device.sendMsg(message);
        }

        public override AsyncMessageToken sendAsyncMsg(IMessage message, CancellationToken ct)
        {
            Tuple<IMessage, CancellationToken> tuple = new Tuple<IMessage, CancellationToken>(message, ct);
            Guid guid = Guid.NewGuid();
            messageDictionary.TryAdd(guid, tuple);
            return new AsyncMessageToken(guid);
        }

        public override IResponse sendRecieve(IMessage message)
        {
            throw new NotImplementedException();
        }

        private IResponse findResponseInList(Guid guid, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                foreach (KeyValuePair<Guid, IResponse> kvp in responseDictionary)
                {
                    if (guid.Equals(kvp.Key))
                    {
                        return kvp.Value;
                    }
                }
                Thread.Sleep(1);
            }
            return null;
        }

        private void messageProcessingFunction(object obj)
        {
            CancellationToken threadCt = (CancellationToken)obj;
            try
            {
                while (!threadCt.IsCancellationRequested)
                {
                    threadCt.ThrowIfCancellationRequested();
                    if (device != null && device.isConnected() && messageDictionary.Count > 0)
                    {
                        KeyValuePair<Guid, Tuple <IMessage, CancellationToken>> kvp = messageDictionary.First();
                        Tuple<IMessage, CancellationToken> t;
                        messageDictionary.TryRemove(kvp.Key, out t);
                        device.sendMsg(kvp.Value.Item1);
                        try
                        {
                            IResponse response = device.receiveAnswer(kvp.Value.Item2);
                            responseDictionary.TryAdd(kvp.Key, response);
                        } catch (OperationCanceledException)
                        {
                            /* cancelation token was thrown */
                        }
                    }
                    Thread.Sleep(1);
                }
            } catch (Exception ex)
            {
                if (ex is ThreadAbortException || ex is SocketException)
                {
                    /* this is ok */
                }
                else if (ex is OperationCanceledException || ex is DeviceNotConnectedException)
                {
                    /* the thread was canceled */
                }
                else
                {
                    throw;
                }
            }

        }
    }
}
