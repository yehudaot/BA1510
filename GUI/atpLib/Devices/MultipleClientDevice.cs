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

        ConcurrentDictionary<Guid, IMessage> messageDictionary = new ConcurrentDictionary<Guid, IMessage>();
        ConcurrentDictionary<Guid, IResponse> responseDictionary = new ConcurrentDictionary<Guid, IResponse>();
        
        Thread messageProcessingThread;

        public MultipleClientDevice(T device)
        {
            this.device = device;
            this.SupportsAsyncOperations = true;
            messageProcessingThread = new Thread(new ThreadStart(messageProcessingFunction));
        }

        public override bool connect()
        {
            if (messageProcessingThread != null && !messageProcessingThread.IsAlive)
            {
                messageProcessingThread.Start();
            }
            return device.connect();
        }

        public override void disconnect()
        {
            if (messageProcessingThread != null && messageProcessingThread.IsAlive)
            {
                messageProcessingThread.Abort();
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

        public override IResponse receiveAsyncAnswer(AsyncMessageToken messageToken)
        {
            Task<IResponse> t = Task<IResponse>.Run(() => findResponseInList(messageToken.Value)).TimeoutAfter(RECIEVE_TIMEOUT_MS);
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

        public override AsyncMessageToken sendAsyncMsg(IMessage message)
        {
            Guid guid = Guid.NewGuid();
            messageDictionary.TryAdd(guid, message);
            return new AsyncMessageToken(guid);
        }

        public override IResponse sendRecieve(IMessage message)
        {
            throw new NotImplementedException();
        }

        private IResponse findResponseInList(Guid guid)
        {
            while (true)
            {
                foreach (KeyValuePair<Guid, IResponse> kvp in responseDictionary)
                {
                    if (guid.Equals(kvp.Key))
                    {
                        return kvp.Value;
                    }
                }
            }
        }

        private void messageProcessingFunction()
        {
            try
            {
                while (true)
                {
                    if (device.isConnected() && messageDictionary.Count > 0)
                    {
                        KeyValuePair<Guid, IMessage> kvp = messageDictionary.First();
                        IMessage t;
                        messageDictionary.TryRemove(kvp.Key, out t);
                        device.sendMsg(kvp.Value);
                        IResponse response = device.receiveAnswer();
                        responseDictionary.TryAdd(kvp.Key, response);
                    }
                }
            } catch (Exception ex)
            {
                if (ex is ThreadAbortException || ex is SocketException)
                {
                    /* this is ok */
                } else
                {
                    throw;
                }
            }

        }
    }
}
