using System;

namespace meteoApp
{
    public class Entry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double CurrentTemperature { get; set; }
    }

}