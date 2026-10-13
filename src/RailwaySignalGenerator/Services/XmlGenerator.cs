using System.Xml.Linq;
using RailwaySignalGenerator.Models;

namespace RailwaySignalGenerator.Services
{
    public class XmlGenerator
    {
        public string OutputPath { get; private set; } = ""; // les propriétés publiques suivent généralement la convention PascalCase.

        public void GenerateXml(
            List<RailwaySignal> signals,
            string outputPath)
        {
      
            XElement root = new XElement("RailwaySignals");
            foreach (RailwaySignal signal in signals)
            {
                // TODO:
                // Create an XML element for each signal.
                XElement signalElement = new XElement("Signal",
                    new XElement("SignalId", signal.SignalId),
                    new XElement("Type", signal.Type),
                    new XElement("TrackId", signal.TrackId),
                    new XElement("Position", signal.Position),
                    new XElement("Direction", signal.Direction),
                    new XElement("State", signal.State)
                );
                root.Add(signalElement);
            }

            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                root
            );

            string? directory = Path.GetDirectoryName(outputPath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }            

            document.Save(outputPath);
            this.OutputPath = outputPath; // Les paramètres et variables locales utilisent généralement camelCase
        }
    }
}