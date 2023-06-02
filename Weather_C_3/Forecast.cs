using System;

namespace Weather_C_3
{
    public class Forecast
    {
        public DateTime Date { get; set; }
        
        public double Temp { get; set; }
        
        public double FeelsLike { get; set; }
        
        public double Pressure { get; set; }
        
        public double Humidity { get; set; }
    }
}