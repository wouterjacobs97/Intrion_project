using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Intrion
{
    //ScannerController has two scanner objects
    class ScannerController
    {
        List<Scanner> scanners;
        ProtocolFactory protocolFactory;
        Logger logger;

        public ScannerController()
        {
            logger = Logger.GetLogger();
            protocolFactory = new ProtocolFactory();
            scanners = new List<Scanner>();

            //the first scanner (id = 0) works with UDP, second scanner (id = 1) with TCP
            scanners.Add(new Scanner(0, protocolFactory.getProtocol("UDP")));
            scanners.Add(new Scanner(1, protocolFactory.getProtocol("TCP")));
        }

        public List<Scanner> getScanners()
        {
            return scanners;
        }

        //this method adds a scanner to the scanners list
        public void addScanner(int id, string protocol)
        {
            while(!protocol.Equals("UDP") && !protocol.Equals("TCP"))
            {
                Console.WriteLine("Protocol can not be used, chose UDP or TCP");
                protocol = Console.ReadLine();
            }
            scanners.Add(new Scanner(id, protocolFactory.getProtocol(protocol)));
            logger.LogMessage("Scanner added: id = " + id + ", protocol = " + protocol);
        }

        //to simulate that a scanner has scanned a code
        public void scanCode(int idScanner, string code)
        {
            Scanner usedScanner = null;

            foreach(Scanner scanner in this.scanners)
            {
                if(idScanner == scanner.getId())
                {
                    usedScanner = scanner;
                }
            }
            
            if(usedScanner != null)
            {
                usedScanner.logScan(code);
            }
            else
            {
                logger.LogMessage("SCANNER ID DOES NOT EXIST FOR SCAN OF CODE: "+code);
            }
        }
    }

    //the scanner object contains a logger and a protocol object
    class Scanner
    {
        int id;
        Protocol protocol;
        Logger logger;

        public Scanner(int id, Protocol protocol)
        {
            logger = Logger.GetLogger();
            this.id = id;
            this.protocol = protocol;
        }

        public int getId()
        {
            return id;
        }

        //this method logs a message to the "log.txt" file
        public void logScan(string code)
        {
            logger.LogMessage("Scanner " + this.id +" that works with the "+ this.protocol.getName() +" protocol has scanned code: " + code);
            this.protocol.logInfo();
        }
    }

}
