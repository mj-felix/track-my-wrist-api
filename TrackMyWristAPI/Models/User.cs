using System.Collections.Generic;

namespace TrackMyWristAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Watch> Watches { get; set; }
    }
}