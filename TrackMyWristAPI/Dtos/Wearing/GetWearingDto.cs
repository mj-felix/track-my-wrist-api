using System;

namespace TrackMyWristAPI.Dtos.Wearing
{
    public class GetWearingDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool WorkDay { get; set; }
        public bool WorkFromHomeDay { get; set; }
        public string Comment { get; set; }
    }
}