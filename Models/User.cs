using Castle.Components.DictionaryAdapter;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CA_Proj.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Nickname { get; set; }
    }
}
