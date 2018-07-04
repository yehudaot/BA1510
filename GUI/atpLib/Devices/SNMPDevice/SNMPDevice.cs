using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;

using atpLib.Messages;
using Lextm.SharpSnmpLib.Messaging;
using System.Net;
using atpLib.Messages.SNMP;

namespace atpLib.Devices
{
    public class SNMPDevice : IPDevice
    {
        IPEndPoint agent;
        public VersionCode SnmpVersion
        {
            get { return VersionCode.V2; }
        }

        public String SNMPComunnity
        {
            get { return "testing"; }
        }

        public int SNMPTimeOut
        {
            get;
            set;
        }

        Task<IList<Variable>> asyncTask;

        public SNMPDevice(string agentAddress) : base(agentAddress)
        {
            SNMPTimeOut = 2000;
        }

        public override bool connect()
        {
            /* try to ping the agent address and see if it is alive */


            agent = new IPEndPoint(IPAddress.Parse(remoteAddress), 161);

            return true;

        }

        public override void disconnect()
        {
            throw new NotImplementedException();
        }

        public override bool isAlive()
        {
            throw new NotImplementedException();
        }

        public override bool isConnected()
        {
            throw new NotImplementedException();
        }

        public override IResponse receiveAnswer()
        {
            int to = SNMPTimeOut;
            while(!asyncTask.IsCompleted && to > 0)
            {
                System.Threading.Thread.Sleep(10);
                to-=10;
            }

            if (to == 0)
            {
                throw new System.TimeoutException("SNMP response did not arrive on time!");
            }

            if(asyncTask.IsFaulted == true || asyncTask.IsCanceled == true)
            {
                log.Error(asyncTask.Exception.GetBaseException().ToString());
                return null;
            }

            return new SNMPResponse(asyncTask.Result);
        }

        public override void sendMsg(IMessage message)
        {
            SNMPMessage m = (SNMPMessage)message;

            if (m.opcode == SNMPMessage.OP.GET_REQUEST)
            {
                asyncTask = Messenger.GetAsync(SnmpVersion,
                               agent,
                               new OctetString(SNMPComunnity),
                               new List<Variable> { new Variable(new ObjectIdentifier(m.OID)) });

                //GetRequestMessage request = new GetRequestMessage(Messenger.NextRequestId, SnmpVersion, SNMPComunnity, new List<Variable> { new Variable(new ObjectIdentifier(m.OID)));
                //Task rep = request.SendAsync(agent);
                //ISnmpMessage reply = request.GetResponse(timeout, receiver);
            } else if (m.opcode == SNMPMessage.OP.SET_REQUEST)
            {
                
            }
        }
    }
}
