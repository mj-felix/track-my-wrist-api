using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyWristAPI.Models
{
    public class Wearing
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Watch Watch { get; set; }
        public string Comment { get; set; }
    }
}