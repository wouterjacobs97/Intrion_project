using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Intrion
{
    //UDP or TCP (upper class)
     abstract class Protocol
    {
        protected Boolean Reliable;
        protected string Information;
        protected string Name;
        Logger logger;


        //GetProtocolSpecifics() is called in the beginning, here the attributes are set to the correct values
        public Protocol() {
            logger = Logger.GetLogger();
            GetProtocolSpecifics();
        }

        public string getName()
        {
            return Name;
        }

        //gives some information to the console about the specific protocol
        public void GetInformation()
        {
            string IsReliable = "";
            if (Reliable)
            {
                IsReliable = "is 100% reliable";
            }
            else
            {
                IsReliable = "is not 100% reliable";
            }
            Console.WriteLine("The " + Name + " protocol "+IsReliable+". Further information: "+Information);
        }

        //this method logs a message to the "log.txt" file
        public void logInfo()
        {
            logger.LogMessage(Name + " protocol has been used.");
        }

        public abstract void GetProtocolSpecifics();
    }

    //UDP_protocol and TCP_protocol inherit from the Protocol class
     class UDP_protocol : Protocol
    {
        //here the specifics per protocol are set
        public override void GetProtocolSpecifics()
        {
            Name = "UDP";
            Reliable = false;
            Information = "This protocol does not ensure the safe arrival of all packets";
        }
    }

    class TCP_protocol : Protocol
    {
        //here the specifics per protocol are set
        public override void GetProtocolSpecifics()
        {
            Name = "TCP";
            Reliable = true;
            Information = "This protocol ensures the safe arrival of all packets";
        }
    }

    class ProtocolFactory
    {
        public Protocol getProtocol(string Name)
        {
            if (Name.Equals("UDP"))
            {
                return new UDP_protocol();
            }
            else if(Name.Equals("TCP"))
            {
                return new TCP_protocol();
            }
            else
            {
                return null;
            }
        }
    }
}
