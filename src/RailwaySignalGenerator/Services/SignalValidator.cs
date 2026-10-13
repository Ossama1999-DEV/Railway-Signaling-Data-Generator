namespace RailwaySignalGenerator.Services
{
    public class SignalValidator
    {
        public bool IsValidPosition(double position)
        {
            return double.IsFinite(position)
                && position >= 0
                && position <= 5000;
        }

        public bool IsValidSignalState(string state){
            return state == "RED" || state == "GREEN" || state == "YELLOW";

        }
    }
}