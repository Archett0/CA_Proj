using System;

namespace CA_Proj.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Nickname { get; set; }
    }
}
