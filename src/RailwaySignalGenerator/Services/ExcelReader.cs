using ClosedXML.Excel;
using RailwaySignalGenerator.Models;

namespace RailwaySignalGenerator.Services
{
    public class ExcelReader
    {
        public List<RailwaySignal> ReadSignals(string filePath)
        {
            List<RailwaySignal> signals = new List<RailwaySignal>();

            using var workbook = new XLWorkbook(filePath);

            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RangeUsed()?.RowsUsed();

            if (rows == null)
            {
                return signals;
            }

            foreach (var row in rows.Skip(1))
            {
                RailwaySignal signal = new RailwaySignal
                {
                    SignalId = row.Cell(1).GetString(),

                    Type = row.Cell(2).GetString(),

                    TrackId = row.Cell(3).GetString(),

                    Position = row.Cell(4).GetDouble(),

                    Direction = row.Cell(5).GetString(),

                    State = row.Cell(6).GetString()
                };

                signals.Add(signal);
            }

            return signals;
        }
    }
}