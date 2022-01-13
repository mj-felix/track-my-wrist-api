using System;
using System.ComponentModel.DataAnnotations;

namespace TrackMyWristAPI.Dtos.Wearing
{
    public class AddWearingDto
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public bool WorkDay { get; set; } = true;
        public bool WorkFromHomeDay { get; set; } = false;
        public string Comment { get; set; }
    }
}