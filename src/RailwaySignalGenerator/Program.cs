using RailwaySignalGenerator.Models;
using RailwaySignalGenerator.Services;

namespace RailwaySignalGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(" RAILWAY SIGNALING DATA GENERATOR");
            Console.WriteLine("=================================");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: RailwaySignalGenerator <excel-file>");
                return;
            }

            string excelPath = args[0];

            if (!File.Exists(excelPath))
            {
                Console.WriteLine($"ERROR: File not found: {excelPath}");
                return;
            }

            ExcelReader reader = new ExcelReader();

            List<RailwaySignal> signals = reader.ReadSignals(excelPath);

            SignalValidator validator = new SignalValidator();

            foreach (RailwaySignal signal in signals)
            {
                bool isPositionValid = validator.IsValidPosition(signal.Position);
                bool isStateValid = validator.IsValidSignalState(signal.State);

                bool isValid = isPositionValid && isStateValid;

                Console.WriteLine("-------------------------------");
                Console.WriteLine($"Signal ID : {signal.SignalId}");
                Console.WriteLine($"Track     : {signal.TrackId}");
                Console.WriteLine($"Position  : {signal.Position}");
                Console.WriteLine($"Valid     : {isValid}");
                if (!isPositionValid)
                {
                    Console.WriteLine("ERROR: Invalid signal position");
                }

                if (!isStateValid)
                {
                    Console.WriteLine("ERROR: Invalid signal state");
                }

            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Total signals: {signals.Count}");
        
            XmlGenerator generator = new XmlGenerator();

            generator.GenerateXml(
                signals,
                "data/output/RailwaySignals.xml"
            );

            Console.WriteLine($"XML file generated: {generator.OutputPath}");       
        }
    }
}