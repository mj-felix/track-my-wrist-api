using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackMyWristAPI.Models
{
    public class AuthServiceResponse
    {
        public string Token { get; set; } = null;
        public string Message { get; set; } = null;
    }
}