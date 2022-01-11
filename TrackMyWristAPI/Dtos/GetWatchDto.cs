using System;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Dtos
{
    public class GetWatchDto
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string ModelName { get; set; }
        public string ModelNumber { get; set; }
        public string Mechanism { get; set; }
        public int Diameter { get; set; }
        public int LugToLug { get; set; }
        public int LugWidth { get; set; }
        public int LiftAngle { get; set; }
        public DateTime PurchasedDate { get; set; }
        public DateTime SoldDate { get; set; }
        public WatchType Type { get; set; }
    }
}