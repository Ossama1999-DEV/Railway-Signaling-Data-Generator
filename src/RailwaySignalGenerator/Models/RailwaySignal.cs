namespace RailwaySignalGenerator.Models
{
    public class RailwaySignal
    {
        public string SignalId { get; set; } = "";

        public string Type { get; set; } = "";

        public string TrackId { get; set; } = "";

        public double Position { get; set; }

        public string Direction { get; set; } = "";

        public string State { get; set; } = "";
    }
}