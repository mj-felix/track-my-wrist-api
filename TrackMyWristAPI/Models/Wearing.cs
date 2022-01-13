using System;
namespace TrackMyWristAPI.Models
{
    public class Wearing
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Watch Watch { get; set; }
        public bool WorkDay { get; set; }
        public bool WorkFromHomeDay { get; set; }
        public string Comment { get; set; }
    }
}