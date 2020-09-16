using System;
using System.IO;

namespace Project_Intrion
{
    class Program
    {
        /*
        README
        Deze console applicatie simuleert iets gelijkaardig zoals in de opdracht bij het testje. De id van de scanner die iets scant (in het begin 
        zijn er enkel printers met id = 0 (werkt met het UDP protocol) en id = 1 (werkt met het TCP protocol). Hierna wordt gevraagd om de gescande code
        in te geven. Als alles goed gaat zal in het log bestand (log.txt) te zien zijn welke scanner welke code gescand heeft en welk protocol in werking
        getreden is. Als een id ingegeven wordt dat niet bestaad wordt gevraagd of er een extra printer toegevoegd moet worden, zo ja moet men een id en
        het gebruikte protocol (UDP of TCP) invullen en kan de printer vervolgens gebruikt worden. 

        De logger werkt via het singleton patroon, de protocols via het factory patroon. Hier is het dus ook gemakkelijk om in de code een extra protocol
        toe te voegen. De ScannerController bevat gewoon een lijst van actieve scanners. De protocols en de scanner objecten bevatten een logger object.

        Pad "log.txt" file --> bin/Debug/netcoreapp3.1/log.txt

         */
        static void Main(string[] args)
        {
            /*Klasse1 klasse1 = new Klasse1();
            klasse1.Log();

            ProtocolFactory protocolFactory = new ProtocolFactory();
            Protocol protocolUDP = protocolFactory.getProtocol("UDP");
            Protocol protocolTCP = protocolFactory.getProtocol("TCP");

            protocolUDP.GetInformation();
            protocolTCP.GetInformation();*/

            ScannerController scannerController = new ScannerController();

            //at the end of each scan the program asks if this is the last scan
            string allScansCompleted = "n";
            while (!allScansCompleted.ToUpper().Equals("Y"))
            {
                Scanner usedScanner = null;
                Console.WriteLine("Give the scanner id:");
                string input = Console.ReadLine();
                int id;

                //this loop continues until there is a correct integer input
                while (!Int32.TryParse(input, out id))
                {
                    Console.WriteLine("Give an integer number");
                    input = Console.ReadLine();
                }

                foreach(Scanner scanner in scannerController.getScanners())
                {
                    if(scanner.getId() == id)
                    {
                        usedScanner = scanner;
                    }
                }

                //when the id matches with an active scanner, the code is correctly scanned 
                if(usedScanner != null)
                {
                    Console.WriteLine("Give the scanned code:");
                    string scannedCode = Console.ReadLine();

                    scannerController.scanCode(usedScanner.getId(), scannedCode);
                    Console.WriteLine("Scans completed? (y/n)");
                    allScansCompleted = Console.ReadLine();
                }

                //otherwise there is asked if a new scanner has to be added to the scanners list
                else
                {
                    Console.WriteLine("Scanner id not found. Add scanner? (y/n)");
                    string addScanner = Console.ReadLine();

                    if (addScanner.ToUpper().Equals("Y"))
                    {
                        int inputId;

                        Console.WriteLine("Give scanner id of new scanner:");
                        string inputNewScanner = Console.ReadLine();

                        while (!Int32.TryParse(inputNewScanner, out inputId))
                        {
                            Console.WriteLine("Give an integer number");
                            inputNewScanner = Console.ReadLine();
                        }

                        Console.WriteLine("Give the protocol used by the scanner:");
                        string protocolNewScanner = Console.ReadLine();

                        scannerController.addScanner(inputId, protocolNewScanner);
                    } 
                }
            }
        }
    }

    public class Logger
    {
        //singleton object
        private static Logger singleton_logger = new Logger();
        private Logger() { }

        public static Logger GetLogger()
        {
            return singleton_logger;
        }

        public void LogMessage(string message)
        {
            //Console.WriteLine("LOGGER: " + message);

            //this code logs the message to the file "log.txt"
            using (StreamWriter writer = File.AppendText("log.txt"))
            {
                writer.Write("\r\nLog:");
                writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                writer.WriteLine(message);
            }
        }
    }

    //test class
    /*public class Klasse1
    {
        private Logger logger;
        private string berichtKlasse;

        public Klasse1()
        {
            berichtKlasse = "klasse 1";
            logger = Logger.GetLogger();
        }

        public void Log()
        {
            logger.LogMessage(berichtKlasse);
        }
    }*/
}
