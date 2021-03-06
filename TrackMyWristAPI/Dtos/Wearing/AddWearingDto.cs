using System;
using System.ComponentModel.DataAnnotations;

namespace TrackMyWristAPI.Dtos.Wearing
{
    public class AddWearingDto
    {
        public DateTime Date { get; set; }
        public bool WorkDay { get; set; }
        public bool WorkFromHomeDay { get; set; }
        public string Comment { get; set; }
    }
}