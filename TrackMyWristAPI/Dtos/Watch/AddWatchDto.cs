using System;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Dtos.Watch
{
    public class AddWatchDto
    {
        public string Manufacturer { get; set; }
        public string ModelName { get; set; }
        public string ModelNumber { get; set; }
        public string Mechanism { get; set; }
        public int Diameter { get; set; }
        public int LugToLug { get; set; }
        public int LugWidth { get; set; }
        public int LiftAngle { get; set; }
        public WatchType Type { get; set; }
    }
}